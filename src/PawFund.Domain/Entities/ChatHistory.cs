using PawFund.Contract.DTOs.ChatHistoryDTOs;
using PawFund.Domain.Abstractions.Entities;

namespace PawFund.Domain.Entities;

public class ChatHistory : DomainEntity<Guid>
{
    public Guid UserId { get; set; }
    public Guid ChatPartnerId { get; set; }
    public virtual Account User { get; set; }
    public virtual Account ChatPartner { get; set; }
    public bool Read { get; set; }
    public string Content { get; set; }

    public ChatHistory() { }

    public ChatHistory(Guid id, Guid userId, Guid chatPartnerId, bool read, string content, DateTime createDate, DateTime modifieDate)
    {
        Id = id;
        UserId = userId;
        ChatPartnerId = chatPartnerId;
        Read = read;
        Content = content;
        CreatedDate = createDate;
        ModifiedDate = modifieDate;
        IsDeleted = false;
    }

    public static ChatHistory CreateChatHistory(Guid id, Guid userId, Guid chatPartnerId, bool read, string content)
    {
        return new ChatHistory(id, userId, chatPartnerId, read, content, DateTime.Now, DateTime.Now);
    }

    public static ChatHistory UpdateChatHistory(Guid id, Guid userId, Guid chatPartnerId, bool read, string content, DateTime createdDate)
    {
        return new ChatHistory(id, userId, chatPartnerId, read, content, createdDate, DateTime.Now);
    }
}