namespace UserService.Utilites;

public static class JwtSettings
{
    public static readonly string SecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? "MK75iPxNo4XNUZMYyE8nomhiDTmiz5ON";
}