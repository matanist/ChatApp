
namespace ChatApp.Data;

public class UserRepository : IUserRepository
{
    private readonly IGenericRepository<User> _genericRepository;

    public UserRepository(IGenericRepository<User> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<User> AddAsync(User entity)
    {
        return await _genericRepository.AddAsync(entity);
    }

    public async Task<User> DeleteAsync(User entity)
    {
        return await _genericRepository.DeleteAsync(entity);
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _genericRepository.GetByIdAsync(id);
    }

    public async Task<IReadOnlyList<User>> ListAllAsync(PaginationModel paginationModel)
    {
        return await _genericRepository.ListAllAsync(paginationModel);
    }

    public async Task<User> UpdateAsync(User entity)
    {
        return await _genericRepository.UpdateAsync(entity);
    }
}
