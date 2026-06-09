using LibraryMS.Domain.Common.Events;
using Microsoft.Extensions.Logging;

namespace LibraryMS.Application.Features.Reservations.EventHandlers;

public class ReservationReadyForPickUpEventHandler(ILogger<ReservationReadyForPickUpEventHandler> logger)
    : INotificationHandler<ReservationReadyForPickUpEvent>
{
    public async Task Handle(ReservationReadyForPickUpEvent notification, CancellationToken cancellationToken)
    {
        // add emial send logic

        logger.LogInformation(
            "NOTIFICATION SENT: Client {ClientId}, your reserved book '{BookTitle}' is ready for pick up! (Reservation ID: {ReservationId})",
            notification.ClientId, notification.BookTitle, notification.ReservationId);

        await Task.CompletedTask;
    }
}