namespace WebApi.Services;

using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Exceptions;

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
        User user = new User()
        {
            login = credentials.login,
            isAdmin = false,
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
        var user = _userRepository.FindById(id);
        if (user == null) throw new NotFoundException();
        return user;
    }

    public void Edit(User user)
    {
        User persistedUser = GetById(user.id);
        persistedUser.isAdmin = user.isAdmin;
        persistedUser.isBlocked = user.isBlocked;
        persistedUser.passwordCriteria = user.passwordCriteria;
        persistedUser.login = user.login;
        persistedUser.password = UserPasswordHelper.hashPassword(user.password);
        _userRepository.Update(persistedUser);
    }

    public void Delete(int id)
    {
        _userRepository.Delete(GetById(id));
    }
}
