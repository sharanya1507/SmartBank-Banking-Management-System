namespace SmartBank.Shared.DTO;

public class AccountCreateDto
{
    public int CustomerId { get; set; }

    public string AccountNumber { get; set; } = null!;

    public string AccountType { get; set; } = null!;

    public decimal CurrentBalance { get; set; }

    public decimal InterestRate { get; set; }
}