namespace LibraryMS.Domain.Entities;

public class Employee : BaseEntity
{
    public int PersonId { get; set; }
    public string EmployeeCode { get; set; } =  string.Empty;
    
    public Person Person { get; set; } = null!;

}