namespace SmartBank.Shared.DTO
{
    public class LoanResponseDto
    {
        public int LoanId { get; set; }

        public int CustomerId { get; set; }

        public string LoanType { get; set; } = null!;

        public decimal PrincipalAmount { get; set; }

        public decimal InterestRate { get; set; }

        public int LoanTenureMonths { get; set; }

        public decimal EMIAmount { get; set; }

        public decimal TotalPayment { get; set; }

        public decimal TotalInterest { get; set; }

        public bool IsApproved { get; set; }
    }
}