namespace LibraryMS.Domain.Common;

public interface ISoftDeleteable
{
    bool IsDeleted { get; set; }
    DateTime? DeletedOn { get; set; }

    void Delete();
    void UnDelete();
}