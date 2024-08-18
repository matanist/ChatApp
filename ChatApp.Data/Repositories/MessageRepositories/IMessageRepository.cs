using ChatApp.Data.Models;

namespace ChatApp.Data;

public interface IMessageRepository : IGenericRepository<Message>
{
    public Task<List<Message>> GetMessageHistory(MessageHistoryModel messageHistoryModel);
}
