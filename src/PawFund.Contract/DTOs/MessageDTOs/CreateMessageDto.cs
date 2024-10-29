namespace PawFund.Contract.DTOs.MessageDTOs;

public class CreateMessageDto
{
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public string Content { get; set; }
}
