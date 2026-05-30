namespace LibraryMS.Domain.Entities;

public class Employee : BaseEntity, ISoftDeleteable
{
    public string EmployeeCode { get; set; } = string.Empty;
    public int UserId { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime? DeletedOn { get; set; }

    public void Delete()
    {
        IsDeleted = true;
        DeletedOn = DateTime.UtcNow;
    }

    public void UnDelete()
    {
        IsDeleted = false;
        DeletedOn = null;
    }
}