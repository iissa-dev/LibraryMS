using LibraryMS.Application.DTOs.EmployeeDto;

namespace LibraryMS.Application.Features.Employee.Queries.GetAllEmployee;

public class GetAllEmployeeQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllEmployeeQuery, Result<PagedResult<EmployeeResponseDto>>>
{
    public async Task<Result<PagedResult<EmployeeResponseDto>>> Handle(GetAllEmployeeQuery request,
        CancellationToken cancellationToken)
    {
        var (items, totalCount) =
            await unitOfWork.Employees.GetEmployeesWithUsersPagedAsync(request.PageNumber, request.PageSize,
                cancellationToken);
        var pagedResult = new PagedResult<EmployeeResponseDto>
        {
            Items = items,
            TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize),
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };

        return Result<PagedResult<EmployeeResponseDto>>.Success(pagedResult);
    }
}