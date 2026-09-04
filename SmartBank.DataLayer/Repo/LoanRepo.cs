using Microsoft.EntityFrameworkCore;
using SmartBank.DataLayer.Interfaces;
using SmartBank.DataLayer.Models;
using SmartBank.Shared.DTO;

namespace SmartBank.DataLayer.Repo
{
    public class LoanRepo : ILoanRepo
    {
        private readonly SmartBankDBContext _context;

        public LoanRepo(SmartBankDBContext context)

        {
            _context = context;
        }

        public async Task<LoanResponseDto> CreateLoanAsync(LoanCreateDto loanDto)
        {
            decimal monthlyRate = loanDto.InterestRate / 12 / 100;

            int months = loanDto.LoanTenureMonths;
             
            decimal emi;

            if (monthlyRate == 0) 
            {
                emi = loanDto.PrincipalAmount / months;
            }
            else
            {
                double power = Math.Pow((double)(1 + monthlyRate),months);

                emi = loanDto.PrincipalAmount * monthlyRate * (decimal)power / ((decimal)power - 1);


            }

            emi = Math.Round(emi, 2);

            decimal totalPayment = emi * months;

            decimal totalInterest = totalPayment - loanDto.PrincipalAmount;

            var loan = new Loan
            {
                CustomerId = loanDto.CustomerId,
                LoanType = loanDto.LoanType,
                PrincipalAmount = loanDto.PrincipalAmount,
                InterestRate = loanDto.InterestRate,
                LoanTenureMonths = months,
                Emiamount = emi,
                RemainingAmount = loanDto.PrincipalAmount,
                LoanStartDate = DateOnly.FromDateTime( DateTime.Now),
                IsApproved = true,
                IsClosed = false
            };

            _context.Loans.Add(loan);

            await _context.SaveChangesAsync();

            return new LoanResponseDto
            {
                LoanId = loan.LoanId,
                CustomerId = loan.CustomerId,
                LoanType = loan.LoanType,
                PrincipalAmount = loan.PrincipalAmount,
                InterestRate = loan.InterestRate,
                LoanTenureMonths = loan.LoanTenureMonths,
                EMIAmount = emi,
                TotalPayment = totalPayment,
                TotalInterest = totalInterest,
                IsApproved = loan.IsApproved
            };
        }

        public async Task<List<LoanResponseDto>>GetAllLoansAsync()
        {
            return await _context.Loans
                .Select(l => new LoanResponseDto
                {
                    LoanId = l.LoanId,
                    CustomerId = l.CustomerId,
                    LoanType = l.LoanType,
                    PrincipalAmount = l.PrincipalAmount,
                    InterestRate = l.InterestRate,
                    LoanTenureMonths = l.LoanTenureMonths,
                    EMIAmount = l.Emiamount,

                    TotalPayment = l.Emiamount * l.LoanTenureMonths,

                    TotalInterest = (l.Emiamount * l.LoanTenureMonths) - l.PrincipalAmount,

                    IsApproved = l.IsApproved
                })
                .ToListAsync();
        }
    }
}