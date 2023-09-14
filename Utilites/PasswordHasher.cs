using UserService.Interfaces.Authentication;

namespace UserService.Utilites;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
    }

    public bool Verify(string providedPassword, string hashedPassword)
    {
        return  BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
    }
}