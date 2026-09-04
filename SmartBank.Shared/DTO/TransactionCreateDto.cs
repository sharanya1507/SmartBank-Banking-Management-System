namespace SmartBank.Shared.DTO
{
    public class TransactionCreateDto
    {
        public int AccountId { get; set; }

        public string TransactionType { get; set; } = null!;

        public decimal Amount { get; set; }

        public string? Description { get; set; }
    }
}