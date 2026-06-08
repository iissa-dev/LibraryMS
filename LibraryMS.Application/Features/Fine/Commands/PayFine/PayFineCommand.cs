namespace LibraryMS.Application.Features.Fine.Commands.PayFine;

public sealed record PayFineCommand(int FineId) : IRequest<Result>;
