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
    User FindByLogin(string login);
    User FindByCredentials(string login, string password);
    void Update(User user);
    void Delete(User user);
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

    public User FindByLogin(string login)
    {
        return _users.SingleOrDefault(user => user.login == login);
    }
    public User FindByCredentials(string login, string password)
    {
        return _users.SingleOrDefault(user => user.login == login && UserPasswordHelper.verifyPassword(password, user.password));
    }
    public void Update(User user)
    {
        _users.Update(user);
        _context.SaveChanges();
    }

    public void Delete(User user)
    {
        _users.Remove(user);
        _context.SaveChanges();
    }
}
