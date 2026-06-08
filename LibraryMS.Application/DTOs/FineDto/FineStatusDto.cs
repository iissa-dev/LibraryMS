namespace LibraryMS.Application.DTOs.FineDto;

public class FineStatusDto
{
    public int FineId { get; init; }
    public decimal FineAmount { get; init; }
    public bool IsPaid { get; init; } = false;

    public int LateDays { get; init; } = 0;

    public string Status { get; init; } = PaymentStatus.Unpaid.ToString();
}