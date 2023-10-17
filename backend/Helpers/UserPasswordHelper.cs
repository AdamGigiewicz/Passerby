namespace WebApi.Helpers;

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using WebApi.Entities;

public class UserPasswordHelper
{
    public static String hashPassword(String password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    public static bool verifyPassword(String password, String hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
    private static bool validatePasswort(String password)
    {
        bool containsLowerCaseLetter = password.Any(char.IsLower);
        bool containsSpecialCharacter = password.Any(ch => !char.IsLetterOrDigit(ch));
        return containsLowerCaseLetter && containsSpecialCharacter;
    }
}
