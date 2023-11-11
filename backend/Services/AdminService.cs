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
            hasToResetPassword = true,
            password = UserPasswordHelper.hashPassword(credentials.password)
        };
        _userRepository.Save(user);
        Logger.LogAction("add user - success");
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
        persistedUser.hasToResetPassword = user.hasToResetPassword;
        persistedUser.login = user.login;
        persistedUser.password = UserPasswordHelper.hashPassword(user.password);
        _userRepository.Update(persistedUser);
        Logger.LogAction("edit user - success");
    }

    public void Delete(int id)
    {
        _userRepository.Delete(GetById(id));
        Logger.LogAction("remove user - success");
    }
}
