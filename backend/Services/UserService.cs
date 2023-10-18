namespace WebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;
using WebApi;

public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    IEnumerable<User> GetAll();
    User GetById(int id);
    User Create(AuthenticateRequest model);
    void Delete(int id);
    void Edit(User user);
    void EditPassword(int id, string oldPassword, string newPassword);
}

public class UserService : IUserService
{

    private readonly AppSettings _appSettings;
    private readonly DbSet<User> _users;
    private readonly ApplicationDbContext _context;
    public UserService(IOptions<AppSettings> appSettings, ApplicationDbContext applicationDbContext)
    {
        _appSettings = appSettings.Value;
        _context = applicationDbContext;
        _users = applicationDbContext.Users;
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _users.SingleOrDefault(x => x.login == model.login && x.password == model.password);

        // return null if user not found
        if (user == null) return null;

        // authentication successful so generate jwt token
        var token = generateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }

    public IEnumerable<User> GetAll()
    {
        return _users;
    }

    public User GetById(int id)
    {
        return _users.FirstOrDefault(x => x.id == id);
    }

    public User Create(AuthenticateRequest model)
    {
        var user = new User()
        {
            login = model.login,
            role = true,
            resetDate = DateTime.Now,
            blocked = false,
            criteria = true,
            password = UserPasswordHelper.hashPassword(model.password)
        };

        _users.Add(user);
        _context.SaveChanges();

        return user;
    }

    public void EditPassword(int id, string oldPassword, string newPassword)
    {
        var user = _users.SingleOrDefault(x => x.id == id);
        if (user != null)
        {
            if (UserPasswordHelper.verifyPassword(oldPassword, user.password))
                user.password = UserPasswordHelper.hashPassword(newPassword);
        };
        _users.Update(user);
        _context.SaveChanges();
    }

    public void Edit(User model)
    {
      Console.WriteLine("start");
        var user = _users.SingleOrDefault(x => x.id == model.id);
        if (user != null)
        {
            user.login = model.login;
            user.role = model.role;
            user.resetDate = DateTime.Now;
            user.blocked = false;
            user.criteria = true;
            user.password = UserPasswordHelper.hashPassword(model.password);
        };
        _users.Update(user);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var user = _users.SingleOrDefault(x => x.id == id);
        if (user != null)
        {
            _users.Remove(user);
            _context.SaveChanges();
        }
    }

    // helper methods

    private string generateJwtToken(User user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
