using System.Security.Cryptography;

namespace API.Domain.ValueObjects;
public class Password
{
    public byte[] PasswordHash { get; private set; }

    public byte[] PasswordSalt { get; private set; }

    public Password(string password)
    {
        try
        {
            createPasswordHash(password);
        }
        catch (ArgumentNullException)
        {
            throw new Exception("Problem creating password hash");
        }
    }

    private void createPasswordHash(string password)
    {
        using (var hmac = new HMACSHA512())
        {
            this.PasswordSalt = hmac.Key;
            this.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}