namespace SmartBank.Shared.DTO
{
    public class LoanCreateDto
    {
        public int CustomerId { get; set; }

        public string LoanType { get; set; } = null!;

        public decimal PrincipalAmount { get; set; }

        public decimal InterestRate { get; set; }

        public int LoanTenureMonths { get; set; }
    }
}