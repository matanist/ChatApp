namespace ChatApp.Data;

public interface IUserRepository : IGenericRepository<User>
{
    public Task<User> GetByUsernameAndPasswordAsync(string username, string password);
    public Task<List<User>> GetUsersByNameAsync(string name);
    Task<User> GetUserByUsernameAsync(string username);
}
