namespace WebApi.Services;

using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Repositories;

public interface IAdminService
{
    void Create(UserCredentials model);
    IEnumerable<User> GetAll();
    User GetById(int id);
    void Delete(int id);
    void Edit(User user);
}

public class AdminService : IAdminService
{
    private readonly IUserRepository _userRepository;

    public AdminService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void Create(UserCredentials credentials)
    {
        var user = new User()
        {
            login = credentials.login,
            isAdmin = false,
            passwordResetDate = DateTime.Now,
            isBlocked = false,
            passwordCriteria = true,
            password = UserPasswordHelper.hashPassword(credentials.password)
        };
        _userRepository.Save(user);
    }

    public IEnumerable<User> GetAll()
    {
        return _userRepository.FindAll();
    }

    public User GetById(int id)
    {
        return _userRepository.FindById(id);
    }

    public void Edit(User user)
    {
        user.password = UserPasswordHelper.hashPassword(user.password);
        _userRepository.Update(user);
    }

    public void Delete(int id)
    {
        _userRepository.Delete(id);
    }
}
