namespace MvcStartApp.Models;

public interface IUserRepository
{
    Task AddUser(User user);
    Task<User[]> GetUsers();
}