namespace SmartBank.Shared.DTO
{
    public class CustomerCreateDto
    {
        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public decimal MonthlyIncome { get; set; }

        public int CreditScore { get; set; }
    }
}
