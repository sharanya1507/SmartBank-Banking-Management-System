namespace SmartBank.Shared.DTO
{
    public class TransactionResponseDto
    {
        public int TransactionId { get; set; }

        public int AccountId { get; set; }

        public string TransactionType { get; set; } = null!;

        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public bool IsSuccessful { get; set; }

        public bool IsFraudSuspected { get; set; }
    }
}