namespace WebApi.Services;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Exceptions;

public interface IUserService
{
    LoggedUser SignIn(string login, string password);
    void ResetPassword(string login, string password);
    void EditPassword(int userId, string oldPassword, string newPassword);
}

public class UserService : IUserService
{
    private readonly AppSettings _appSettings;
    private readonly IUserRepository _userRepository;

    public UserService(IOptions<AppSettings> appSettings, IUserRepository userRepository)
    {
        _appSettings = appSettings.Value;
        _userRepository = userRepository;
    }

    public LoggedUser SignIn(string login, string password)
    {
        var user = _userRepository.FindByCredentials(login, password);
        if (user == null || user.isBlocked) throw new IdentityException();
        Logger.LogAction(user.id, "login - success");
        return new LoggedUser(generateJwtToken(user.id.ToString()));
    }

    public void ResetPassword(string login, string password)
    {
        var user = _userRepository.FindByLogin(login);
        if (user == null || !user.hasToResetPassword) throw new IdentityException();
        if (user.passwordCriteria && !UserPasswordHelper.validatePassword(password)) throw new ValidationException();
        user.hasToResetPassword = false;
        user.password = UserPasswordHelper.hashPassword(password);
        Logger.LogAction(user.id, "reset password - success");
        _userRepository.Update(user);
    }

    public void EditPassword(int userId, string oldPassword, string newPassword)
    {
        var user = _userRepository.FindById(userId);
        if (user == null || !UserPasswordHelper.verifyPassword(oldPassword, user.password)) throw new IdentityException();
        if (user.passwordCriteria && !UserPasswordHelper.validatePassword(newPassword)) throw new ValidationException();
        user.password = UserPasswordHelper.hashPassword(newPassword);
        Logger.LogAction(user.id, "change password - success");
        _userRepository.Update(user);
    }

    private string generateJwtToken(string userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", userId) }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
