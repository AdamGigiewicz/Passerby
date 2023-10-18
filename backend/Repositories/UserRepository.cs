namespace WebApi.Repositories;

using Microsoft.EntityFrameworkCore;
using WebApi;
using WebApi.Entities;
using WebApi.Helpers;

public interface IUserRepository
{
    void Save(User user);
    IEnumerable<User> FindAll();
    User FindById(int id);
    User FindByCredentials(string login, string password);
    void Update(User user);
    void Delete(int id);
}

public class UserRepository : IUserRepository
{
    private readonly DbSet<User> _users;
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
        _users = applicationDbContext.Users;
    }

    public void Save(User user)
    {
        _users.Add(user);
        _context.SaveChanges();
    }

    public IEnumerable<User> FindAll()
    {
        return _users;
    }

    public User FindById(int id)
    {
        return _users.SingleOrDefault(user => user.id == id);
    }

    public User FindByCredentials(string login, string password)
    {
        return _users.SingleOrDefault(user => user.login == login && UserPasswordHelper.verifyPassword(password, user.password));
    }

    public void Update(User user)
    {
        var persistedUser = FindById(user.id);
        if (persistedUser != null)
        {
            persistedUser.login = user.login;
            persistedUser.isAdmin = user.isAdmin;
            persistedUser.passwordResetDate = user.passwordResetDate;
            persistedUser.isBlocked = user.isBlocked;
            persistedUser.passwordCriteria = user.passwordCriteria;
            persistedUser.password = user.password;
            _users.Update(user);
            _context.SaveChanges();
        }
    }

    public void Delete(int id)
    {
        var persistedUser = FindById(id);
        if (persistedUser != null)
        {
            _users.Remove(persistedUser);
            _context.SaveChanges();
        }
    }
}
