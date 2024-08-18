
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data;

public class UserRepository : IUserRepository
{
    private readonly IGenericRepository<User> _genericRepository;
    private readonly ChatAppDbContext _context;
    public UserRepository(IGenericRepository<User> genericRepository, ChatAppDbContext context)
    {
        _genericRepository = genericRepository;
        _context = context;
    }

    public async Task<User> AddAsync(User entity)
    {
        return await _genericRepository.AddAsync(entity);
    }

    public async Task<int> CountAsync()
    {
        return await _genericRepository.CountAsync();
    }

    public async Task<User> DeleteAsync(User entity)
    {
        return await _genericRepository.DeleteAsync(entity);
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _genericRepository.GetByIdAsync(id);
    }

    public async Task<User> GetByUsernameAndPasswordAsync(string username, string password)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
    }

    

    public async Task<List<User>> GetUsersByNameAsync(string name)
    {
        return await _context.Users.Where(x => x.FullName.Contains(name)).Take(10).ToListAsync();
    }

    public async Task<IReadOnlyList<User>> ListAllAsync(PaginationModel paginationModel)
    {
        return await _genericRepository.ListAllAsync(paginationModel);
    }

    public async Task<User> UpdateAsync(User entity)
    {
        return await _genericRepository.UpdateAsync(entity);
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
       return await _context.Users.FirstOrDefaultAsync(u=>u.Username==username);
    }
}
