using ChatApp.Data;
using ChatApp.Data.Models;


namespace ChatApp.Services;

public interface IMessageService: IGenericService<Message>
{
    Task<List<Message>> GetMessageHistory(MessageHistoryModel messageHistoryModel);
}
