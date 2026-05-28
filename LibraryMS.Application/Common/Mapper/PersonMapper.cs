using LibraryMS.Application.Features.Client.Commands.RegisterClient;
using LibraryMS.Application.Features.Employee.Commands.CreateEmployeeAccount;
using LibraryMS.Domain.Entities;

namespace LibraryMS.Application.Common.Mapper;

public static class PersonMapper
{
    public static Person ToEntity(this RegisterClientCommand request)
    {
        return new Person
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Address = request.Address,
            DateOfBirth = request.BirthDate,
            CountryId = request.CountryId,
            ImageUrl = request.ImageUrl,
        };
    }
    public static Person ToEntity(this CreateEmployeeCommand request)
    {
        return new Person
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Address = request.Address,
            DateOfBirth = request.BirthDate,
            CountryId = request.CountryId,
            ImageUrl = request.ImageUrl,
        };
    }
}