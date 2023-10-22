namespace WebApi.Helpers;

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

    public static bool validatePassword(String password)
    {
        bool containsLowerCaseLetter = password.Any(char.IsLower);
        bool containsSpecialCharacter = password.Any(ch => !char.IsLetterOrDigit(ch));
        return containsLowerCaseLetter && containsSpecialCharacter;
    }
}
