namespace LibraryMS.Application.Common.Interfaces;

public interface INotificationService
{
    Task SendNotificationToClientAsync(int clientId, string title, string message, int reservationId, CancellationToken cancellationToken);
}