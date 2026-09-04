using Microsoft.EntityFrameworkCore;
using SmartBank.DataLayer.Interfaces;
using SmartBank.DataLayer.Models;
using SmartBank.Shared.DTO;

namespace SmartBank.DataLayer.Repo
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly SmartBankDBContext _context;

        public CustomerRepo(SmartBankDBContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerResponseDto>> GetAllCustomersAsync()
        {
            return await _context.Customers
                .Select(c => new CustomerResponseDto
                {
                    CustomerId = c.CustomerId,
                    CustomerCode = c.CustomerCode,
                    FullName = c.FullName,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    DateOfBirth = c.DateOfBirth,
                    Gender = c.Gender,
                    MonthlyIncome = c.MonthlyIncome,
                    CreditScore = c.CreditScore,
                    IsActive = c.IsActive,
                    IsKycVerified = c.IsKycVerified ?? false,
                    CreatedDate = c.CreatedDate
                })
                .ToListAsync();
        }

        public async Task<CustomerResponseDto?> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers
                .Where(c => c.CustomerId == id)
                .Select(c => new CustomerResponseDto
                {
                    CustomerId = c.CustomerId,
                    CustomerCode = c.CustomerCode,
                    FullName = c.FullName,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    DateOfBirth = c.DateOfBirth,
                    Gender = c.Gender,
                    MonthlyIncome = c.MonthlyIncome,
                    CreditScore = c.CreditScore,
                    IsActive = c.IsActive,
                    IsKycVerified = c.IsKycVerified ?? false,
                    CreatedDate = c.CreatedDate
                })
                .FirstOrDefaultAsync();
        }

        public async Task<string> CreateCustomerAsync(CustomerCreateDto customerDto)
        {
            var customer = new Customer
            {
                CustomerCode = Guid.NewGuid(),
                FullName = customerDto.FullName,
                Email = customerDto.Email,
                PhoneNumber = customerDto.PhoneNumber,
                DateOfBirth = customerDto.DateOfBirth,
                Gender = customerDto.Gender,
                MonthlyIncome = customerDto.MonthlyIncome,
                CreditScore = customerDto.CreditScore,
                IsActive = true,
                IsKycVerified = false,
                CreatedDate = DateTime.Now
            };

            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();

            return "Customer created successfully with ID: " + customer.CustomerId;
        }

        public async Task<CustomerResponseDto?> UpdateCustomerAsync(int id,CustomerUpdateDto customerDto)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
                return null;

            customer.FullName = customerDto.FullName;
            customer.PhoneNumber = customerDto.PhoneNumber;
            customer.MonthlyIncome = customerDto.MonthlyIncome;
            customer.CreditScore = customerDto.CreditScore;
            customer.IsActive = customerDto.IsActive;
            customer.IsKycVerified = customerDto.IsKycVerified;

            await _context.SaveChangesAsync();

            return new CustomerResponseDto
            {
                CustomerId = customer.CustomerId,
                CustomerCode = customer.CustomerCode,
                FullName = customer.FullName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                DateOfBirth = customer.DateOfBirth,
                Gender = customer.Gender,
                MonthlyIncome = customer.MonthlyIncome,
                CreditScore = customer.CreditScore,
                IsActive = customer.IsActive,
                IsKycVerified = customer.IsKycVerified ?? false,
                CreatedDate = customer.CreatedDate
            };
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
                return false;

            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();

            return true;
        }



        public async Task<object?> GetFinancialSummaryAsync(int customerId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (customer == null)
                return null;

            var accountIds = await _context.Accounts
                .Where(a => a.CustomerId == customerId)
                .Select(a => a.AccountId)
                .ToListAsync();

            var totalBalance = await _context.Accounts
                .Where(a => a.CustomerId == customerId)
                .SumAsync(a => a.CurrentBalance);

            var totalDeposits = await _context.BankTransactions
                .Where(t => accountIds.Contains(t.AccountId) && t.TransactionType == "Deposit")
                .SumAsync(t => t.Amount);

            var totalWithdrawals = await _context.BankTransactions
                .Where(t => accountIds.Contains(t.AccountId) && t.TransactionType == "Withdrawal")
                .SumAsync(t => t.Amount);

            var totalLoanAmount = await _context.Loans
                .Where(l => l.CustomerId == customerId)
                .SumAsync(l => l.PrincipalAmount);

            var totalAccounts = accountIds.Count;

            var totalLoans = await _context.Loans
                .CountAsync(l => l.CustomerId == customerId);

            return new
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.FullName,

                TotalAccounts = totalAccounts,

                TotalBalance = totalBalance,

                TotalDeposits = totalDeposits,

                TotalWithdrawals = totalWithdrawals,

                NetSavings =
                    totalDeposits - totalWithdrawals,

                TotalLoans = totalLoans,

                TotalLoanAmount = totalLoanAmount
            };
        }
    }
}