using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain;

[Table("Users", Schema = "User")]
public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
}