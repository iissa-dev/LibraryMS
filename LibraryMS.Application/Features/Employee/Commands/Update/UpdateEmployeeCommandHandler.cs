using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Application.Common.Results;
using LibraryMS.Application.DTOs.UserDto;
using MediatR;

namespace LibraryMS.Application.Features.Employee.Commands.Update;

public class UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork, IIdentityUser identityUser) : IRequestHandler<UpdateEmployeeCommand, Result>
{
    public async Task<Result> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            // Update Employee
            var employee = await unitOfWork.Employees.GetEmployeeByUserIdAsync(request.UserId);
            if (employee is null)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result.Failure("Employee not found");
            }

            employee.EmployeeCode = request.EmployeeCode;

            // Update User
            var userData = new UpdateUserInfoDto
            {
                UserId = request.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                UserName = request.UserName,
                ImageUrl = request.ImageUrl,
                DateOfBirth = request.DateOfBirth,
                CountryId = request.CountryId
            };

            var result = await identityUser.UpdateUserInfoAsync(userData);
            if (result.IsFailure)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result.Failure(result.Error);
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return Result.Success;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}