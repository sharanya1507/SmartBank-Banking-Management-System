
        namespace SmartBank.Shared.DTO
    {
        public class CustomerUpdateDto
        {
            public string FullName { get; set; } = null!;

            public string PhoneNumber { get; set; } = null!;

            public decimal MonthlyIncome { get; set; }

            public int CreditScore { get; set; }

            public bool IsActive { get; set; }

            public bool IsKycVerified { get; set; }
        }
    }

