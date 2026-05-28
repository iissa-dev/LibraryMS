using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Application.Common.Results;
using MediatR;

namespace LibraryMS.Application.Features.Person.Commands.UpdatePersonInfo;

public class UpdatePersonInfoCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdatePersonInfoCommand, Result>
{
    public async Task<Result> Handle(UpdatePersonInfoCommand request, CancellationToken cancellationToken)
    {
        var person = await unitOfWork.Persons.GetPersonByUserIdAsync(request.UserId);
        if (person is null) return Result.Failure("Person info not found");

        person.Update(
            request.FirstName,
            request.LastName,
            request.Address,
            request.ImageUrl,
            request.BirthDate
        );

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}