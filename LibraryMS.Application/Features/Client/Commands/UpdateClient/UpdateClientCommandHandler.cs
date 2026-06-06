namespace LibraryMS.Application.Features.Client.Commands.UpdateClient;

public class UpdateClientCommandHandler(IAppDbContext context) : IRequestHandler<UpdateClientCommand, Result>
{
    public async Task<Result> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        // Update if there is any updated logic in the future
        var client = await context.Clients
            .SingleOrDefaultAsync(c => c.Id == request.ClientId, cancellationToken);

        if (client is null)
        {
            return Result.Failure("Client does not exists");
        }

        client.LibraryCardNumber = request.LibraryCardNumber;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success;

    }
}