namespace LibraryMS.Domain.Entities;

public abstract class SoftDeleteableEntity : BaseEntity, ISoftDeleteable
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOn { get; set; }
    public virtual void Delete() { IsDeleted = true; DeletedOn = DateTime.UtcNow; }
    public virtual void UnDelete() { IsDeleted = false; DeletedOn = null; }
}