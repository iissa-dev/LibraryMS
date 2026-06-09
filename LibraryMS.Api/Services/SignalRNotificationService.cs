using LibraryMS.Api.Hubs;
using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Application.DTOs.ReservationDto;
using Microsoft.AspNetCore.SignalR;

namespace LibraryMS.Api.Services;

public class SignalRNotificationService(IHubContext<NotificationHub> hubContext) : INotificationService
{
    public async Task SendNotificationToClientAsync(int clientId, string title, string message, int reservationId, CancellationToken cancellationToken)
    {
        await hubContext.Clients
            .User(clientId.ToString())
            .SendAsync(ApiConstant.ReceiveReservationNotificationKey, new ReservationNotificationDto
            {
                Title = title,
                Message = message,
                ReservationId = reservationId
            }, cancellationToken);
    }
}