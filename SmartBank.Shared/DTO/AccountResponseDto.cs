namespace SmartBank.Shared.DTO
{
    public class AccountResponseDto
    {
        public int AccountId { get; set; }

        public int CustomerId { get; set; }

        public string AccountNumber { get; set; } = null!;

        public string AccountType { get; set; } = null!;

        public decimal CurrentBalance { get; set; }

        public decimal InterestRate { get; set; }

        public bool IsActive { get; set; }

        public bool IsBlocked { get; set; }
    }
}