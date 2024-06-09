namespace ChatApp.Data;

public interface IUserRepository : IGenericRepository<User>
{
    public Task<User> GetByUsernameAndPasswordAsync(string username, string password);
}
