namespace ChatApp.Data;

public class MessageRepository : IMessageRepository
{
    private readonly IGenericRepository<Message> _genericRepository;

    public MessageRepository(IGenericRepository<Message> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<Message> AddAsync(Message entity)
    {
        return await _genericRepository.AddAsync(entity);
    }

    public async Task<Message> DeleteAsync(Message entity)
    {
        return await _genericRepository.DeleteAsync(entity);
    }

    public async Task<Message> GetByIdAsync(int id)
    {
        return await _genericRepository.GetByIdAsync(id);
    }

    public async Task<IReadOnlyList<Message>> ListAllAsync(PaginationModel paginationModel)
    {
        return await _genericRepository.ListAllAsync(paginationModel);
    }

    public async Task<Message> UpdateAsync(Message entity)
    {
        return await _genericRepository.UpdateAsync(entity);
    }
}

