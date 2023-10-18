namespace WebApi.Services;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Repositories;

public interface IUserService
{
    LoggedUser Login(UserCredentials form);
    void EditPassword(UserChangePassword form);
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

    public LoggedUser Login(UserCredentials form)
    {
        LoggedUser loggedUser = null;
        var user = _userRepository.FindByCredentials(form.login, form.password);
        if (user != null)
        {
            string userToken = generateJwtToken(user.id.ToString());
            loggedUser = new LoggedUser(user.id, userToken);
        }
        return loggedUser;
    }

    public void EditPassword(UserChangePassword form)
    {
        var user = _userRepository.FindById(form.id);
        if (user != null && UserPasswordHelper.verifyPassword(form.oldPassword, user.password) && UserPasswordHelper.validatePassword(form.newPassword))
        {
            user.password = UserPasswordHelper.hashPassword(form.newPassword);
            _userRepository.Update(user);
        };
    }

    private string generateJwtToken(string userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", userId) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
