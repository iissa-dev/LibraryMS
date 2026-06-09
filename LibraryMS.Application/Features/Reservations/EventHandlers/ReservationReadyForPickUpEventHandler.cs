using LibraryMS.Domain.Common.Events;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace LibraryMS.Application.Features.Reservations.EventHandlers;

public class ReservationReadyForPickUpEventHandler(INotificationService notificationService, ILogger<ReservationReadyForPickUpEventHandler> logger)
    : INotificationHandler<ReservationReadyForPickUpEvent>
{
    public async Task Handle(ReservationReadyForPickUpEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("SignalR: Sending live notification to Client {ClientId}", notification.ClientId);

        await notificationService.SendNotificationToClientAsync(
            notification.ClientId,
            title: "Book ready to pick up.",
            message: $"Your Book \"{notification.BookTitle}\" ready to pick up.",
            notification.ReservationId,
            cancellationToken
        );
    }
}