using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Controllers.Model;
using UserService.Domain;
using UserService.Interfaces.Authentication;
using UserService.Persistence;
using UserService.Utilites;

namespace UserService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public UserController(AppDbContext context, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }
    
    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp(SignUpRequest request)
    {
        var emailExists = await _context.Users.AnyAsync(x => x.Email == request.Email);
        if (emailExists)
            return BadRequest("Email already exists.");
        
        var phoneNumberExists = await _context.Users.AnyAsync(x => x.PhoneNumber == request.PhoneNumber);
        if (phoneNumberExists)
            return BadRequest("Phone number already exists.");
        
        var hashedPassword = _passwordHasher.Hash(request.Password);
        var user = new User
        {
            Email = request.Email,
            Password = hashedPassword,
            Name = request.Name,
            PhoneNumber = request.PhoneNumber
        };
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return Ok();
    }
    
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
        if (user == null)
            return BadRequest("Invalid email or password.");
        
        var isPasswordValid = _passwordHasher.Verify(request.Password, user.Password);
        if (!isPasswordValid)
            return BadRequest("Invalid email or password.");
        
        var token = _jwtProvider.Generate(user);
        return Ok(token);
    }
}