using LibraryMS.Domain.Common.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LibraryMS.Infrastructure.Services;

public class ReservationCheckJob(IServiceProvider provider, ILogger<ReservationCheckJob> logger)
    : BackgroundService
{
    private static readonly TimeSpan CheckInterval = TimeSpan.FromHours(1);
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Reservation Check Job is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await CheckReservationsAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while checking reservations.");
            }
            await Task.Delay(CheckInterval, stoppingToken);
        }

    }

    private async Task CheckReservationsAsync(CancellationToken cancellationToken)
    {
        using var scope = provider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
        var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

        var readyReservations = await context.Reservations
            .Include(r => r.Book)
            .Include(r => r.BookCopy)
            .Where(r => r.ReservationsStatus == ReservationsStatus.ReadyForPickup
                        && r.BookCopy.CopyStatus == CopyStatus.Reserved)
            .ToListAsync(cancellationToken);

        if (!readyReservations.Any()) return;

        logger.LogInformation("Found {Count} reservations ready for pick-up.", readyReservations.Count);

        foreach (var reservation in readyReservations)
        {
            reservation.ReservationsStatus = ReservationsStatus.Notified;

            var @event = new ReservationReadyForPickUpEvent(
                reservation.Id,
                reservation.ClientId,
                reservation.Book.Title
            );
            await publisher.Publish(@event, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}