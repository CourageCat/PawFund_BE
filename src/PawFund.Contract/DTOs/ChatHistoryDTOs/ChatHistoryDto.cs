using PawFund.Contract.DTOs.Account;

namespace PawFund.Contract.DTOs.ChatHistoryDTOs;

public class ChatHistoryDto
{
    public Guid UserId { get; set; }
    public AccountDto User { get; set; }
    public bool Read { get; set; }
    public string Content { get; set; }
}