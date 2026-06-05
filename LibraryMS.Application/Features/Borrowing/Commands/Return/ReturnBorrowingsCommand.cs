namespace LibraryMS.Application.Features.Borrowing.Commands.Return;

public sealed record ReturnBorrowingsCommand(
    int BorrowingId,
    int CopyId
) : IRequest<Result>;
