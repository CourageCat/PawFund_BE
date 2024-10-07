namespace PawFund.Contract.MessagesList;

public enum MessagesList
{
    [Message("This email has not been registered, please try again", "auth01")]
    AuthEmailNotFoundException,
}