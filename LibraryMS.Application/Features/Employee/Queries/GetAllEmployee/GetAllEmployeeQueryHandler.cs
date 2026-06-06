using LibraryMS.Application.Common.Extensions;
using LibraryMS.Application.DTOs.EmployeeDto;

namespace LibraryMS.Application.Features.Employee.Queries.GetAllEmployee;

public class GetAllEmployeeQueryHandler(IAppDbContext context)
    : IRequestHandler<GetAllEmployeeQuery, Result<PagedResult<EmployeeResponseDto>>>
{
    public async Task<Result<PagedResult<EmployeeResponseDto>>> Handle(GetAllEmployeeQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.Employees
        .AsNoTracking()
        .OrderByDescending(e => e.CreatedOn);

        var pagedResult = await query
            .ToPagedResultAsync(
                request.PageNumber,
                request.PageSize,
                selector: x => new EmployeeResponseDto
                {
                    EmployeeId = x.Id,
                    EmployeeCode = x.EmployeeCode,
                    CreatedOn = x.CreatedOn,
                    Country = x.Person.Country != null ? x.Person.Country.Name : "Unknown",

                    FirstName = x.Person.FirstName,
                    LastName = x.Person.LastName,
                    Address = x.Person.Address
                },
                cancellationToken);

        return Result<PagedResult<EmployeeResponseDto>>.Success(pagedResult);
    }
}