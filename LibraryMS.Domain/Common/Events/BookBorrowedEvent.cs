namespace LibraryMS.Domain.Common.Events;

public class BookBorrowedEvent : IDomainEvent
{
    public int CopyId { get; }
    public int ClientId { get; }
    public DateTime DueDate { get; }

    public BookBorrowedEvent(int copyId, int clientId, DateTime dueDate)
    {
        CopyId = copyId;
        ClientId = clientId;
        DueDate = dueDate;
    }
}