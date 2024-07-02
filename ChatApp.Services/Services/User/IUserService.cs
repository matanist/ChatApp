using ChatApp.Data;

namespace ChatApp.Services;

public interface IUserService : IGenericService<User>
{
    public Task<User> GetByUsernameAndPasswordAsync(string username, string password);
}
