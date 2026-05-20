using LibraryMS.Application.Features.Client.Commands.RegisterClient;
using LibraryMS.Domain.Entities;

namespace LibraryMS.Application.Mapper;

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
}