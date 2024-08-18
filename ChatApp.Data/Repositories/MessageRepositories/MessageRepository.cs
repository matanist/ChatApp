using System.Linq;
using ChatApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data;

public class MessageRepository : IMessageRepository
{
    private readonly IGenericRepository<Message> _genericRepository;
    private readonly ChatAppDbContext _context;

    public MessageRepository(IGenericRepository<Message> genericRepository, ChatAppDbContext context)
    {
        _genericRepository = genericRepository;
        _context = context;
    }

    public async Task<Message> AddAsync(Message entity)
    {
        return await _genericRepository.AddAsync(entity);
    }

    public async Task<int> CountAsync()
    {
        return await _genericRepository.CountAsync();
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
    public async Task<List<Message>> GetMessageHistory(MessageHistoryModel messageHistoryModel)
    {
        var senderUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == messageHistoryModel.SenderUsername);
        var receiverUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == messageHistoryModel.ReceiverUsername);

        var query = _context.Messages.AsNoTracking();

        if (messageHistoryModel.MessageForPrivateChat)
        {
            query = query.Where(m => m.UserId == senderUser.Id && m.ReceiverUserId == receiverUser.Id);
        }
        else
        {
            var receiverGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Name == messageHistoryModel.GroupName);
            query = query.Where(m => m.UserId == senderUser.Id && m.ReceiverGroupId == receiverGroup.Id);
        }
        var queryResult = await query.OrderByDescending(q=>q.CreatedAt).Take(20).ToListAsync();
        return queryResult;


    }
}

