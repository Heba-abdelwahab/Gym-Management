using Domain.Common;
using Domain.Entities.Chat;
using Domain.ValueObjects.Chat;

namespace Domain.Contracts;

public interface IMessageRepository
{
    void AddMessage(Message message);
    void DeleteMessage(Message message);

    Task<Message?> GetMessage(int id);


    Task<PagedList<MessageDto>> GetMessagesForUserAsync(MessageParams messageParams);
    Task<IReadOnlyList<MessageDto>> GetMessagesThreadAsync(string currentUserName, string recipientUserName);

    Task<bool> SaveAllAsync();


    void AddGroup(Group group);
    void RemoveConnection(Connection connection);

    Task<Connection?> GetConnectionAsync(string ConnectionId);

    Task<Group?> GetMessageGroupAsync(string groupName);
    Task<Group?> GetGroupForConnectionAsync(string ConnectionId);


}
