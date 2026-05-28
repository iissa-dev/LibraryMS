namespace LibraryMS.Domain.Entities;

public class Employee : BaseEntity
{
    public string EmployeeCode { get; set; } =  string.Empty;
    public int UserId {get; set;}
}