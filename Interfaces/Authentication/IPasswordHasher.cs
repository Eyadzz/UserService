namespace UserService.Interfaces.Authentication;

public interface IPasswordHasher
{
    /// <summary>
    /// Hashes the given password using the BCrypt algorithm 
    /// </summary>
    /// <param name="password">The password to hash</param>
    /// <returns>String hashed password</returns>
    string Hash(string password);
    
    /// <summary>
    /// Verifies the given password against the given hashed password
    /// </summary>
    /// <param name="providedPassword">the provided password by the user</param>
    /// <param name="hashedPassword">the existing hashed password to validate against</param>
    /// <returns>True if the password matches and false if not</returns>
    bool Verify(string providedPassword, string hashedPassword);
}