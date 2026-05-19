namespace LibraryMS.Domain.Entities;

public class Setting
{
    public int Id { get; set; }
    public int DefaultBorrowDays { get; set; } = 7;
    public decimal DefaultFinePerDay { get; set; } = 1.1m;
}