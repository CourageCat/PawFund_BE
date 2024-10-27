namespace PawFund.Contract.Services.Messages;

public class CreateMessageDto
{
    public Guid ConnectionUser { get; set; }
    public Guid ConnectionStaff { get; set; }
}
