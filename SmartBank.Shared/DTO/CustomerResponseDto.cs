using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBank.Shared.DTO
{
    public class CustomerResponseDto
    {
        public int CustomerId { get; set; }

        public Guid? CustomerCode { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public decimal MonthlyIncome { get; set; }

        public int CreditScore { get; set; }

        public bool IsActive { get; set; }

        public bool IsKycVerified { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
