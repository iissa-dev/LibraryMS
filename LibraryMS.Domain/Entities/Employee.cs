namespace LibraryMS.Domain.Entities;

public class Employee : SoftDeleteableEntity
{
    public string EmployeeCode { get; set; } = string.Empty;
    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;
}