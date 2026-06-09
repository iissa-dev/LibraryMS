using LibraryMS.Domain.Entities;
namespace LibraryMS.Domain.Common.Specifications;

public sealed class HasUnpaidFinesSpecification : BaseSpecification<Fine>
{
    public HasUnpaidFinesSpecification(int ClientId)
    {
        Query = f => f.ClientId == ClientId && f.PaymentStatus == PaymentStatus.Unpaid;
    }
}
