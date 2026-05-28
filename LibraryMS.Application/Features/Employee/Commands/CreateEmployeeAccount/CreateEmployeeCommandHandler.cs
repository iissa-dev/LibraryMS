using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Application.Common.Mapper;
using LibraryMS.Application.Common.Results;
using LibraryMS.Domain.Enums;
using MediatR;

namespace LibraryMS.Application.Features.Employee.Commands.CreateEmployeeAccount;

public sealed class CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, IIdentityUser identityUser)
    : IRequestHandler<CreateEmployeeCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
             if (await unitOfWork.Employees.ExistsAsync(c => c.EmployeeCode == request.EmployeeCode))
            return Result<int>.Failure("This Employee Number is already assigned to another employee.");

            var person = request.ToEntity();
            unitOfWork.Repository<Domain.Entities.Person>().Add(person);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var userResult = await identityUser.CreateUserAsync(request.Email, request.Password, request.UserName,
                person.Id,
                request.PhoneNumber);

            if (userResult.IsFailure)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result<int>.Failure(userResult.Error);
            }

            var employee = new Domain.Entities.Employee
            {
                UserId = userResult.Data,
                EmployeeCode = request.EmployeeCode,
            };

            unitOfWork.Employees.Add(employee);
            
            Result<int>? roleResult;
            if(request.RoleId == (short)Roles.Admin)
            {
                roleResult = await identityUser.AddToRolesAsync(request.UserName, [Roles.Admin.ToString(), Roles.Employee.ToString()]);
            }
            else
            {
                roleResult = await identityUser.AddUserToRoleAsync(request.UserName, Roles.Employee);
            }
            

            if (roleResult.IsFailure)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result<int>.Failure(roleResult.Error);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return Result<int>.Success(employee.Id);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}