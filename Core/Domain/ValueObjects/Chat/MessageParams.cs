using Domain.Common;

namespace Domain.ValueObjects.Chat;

public class MessageParams : PaginationParams
{
    public string UserName { get; set; } = string.Empty; //current loged in user name 

    public string Container { get; set; } = "Unread";
}