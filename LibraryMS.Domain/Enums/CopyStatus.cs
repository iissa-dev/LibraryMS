namespace LibraryMS.Domain.Enums;

public enum CopyStatus
{
    Available = 1,
    Borrowed,
    Reserved,
    InMaintenance,
    Lost,
    Damaged,
    Restricted,
    Archived
}