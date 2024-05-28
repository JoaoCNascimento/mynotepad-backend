namespace MyNotepad.Domain.Exceptions
{
    public class UnauthorizedOperationException(string message) : Exception(message);
}
