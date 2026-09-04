using Microsoft.EntityFrameworkCore;
using SmartBank.DataLayer.Interfaces;
using SmartBank.DataLayer.Models;
using SmartBank.Shared.DTO;

namespace SmartBank.DataLayer.Repo
{
    public class AccountRepo : IAccountRepo
    {
        private readonly SmartBankDBContext _context;

        public AccountRepo(SmartBankDBContext context)
        {
            _context = context;
        }

        public async Task<List<AccountResponseDto>> GetAllAccountsAsync()
        {
            return await _context.Accounts
                .Select(a => new AccountResponseDto
                {
                    AccountId = a.AccountId,
                    CustomerId = a.CustomerId,
                    AccountNumber = a.AccountNumber,
                    AccountType = a.AccountType,
                    CurrentBalance = a.CurrentBalance,
                    InterestRate = a.InterestRate,
                    IsActive = a.IsActive,
                    IsBlocked = a.IsBlocked
                })
                .ToListAsync();
        }

        public async Task<AccountResponseDto?> GetAccountByIdAsync(int id)
        {
            return await _context.Accounts
                .Where(a => a.AccountId == id)
                .Select(a => new AccountResponseDto
                {
                    AccountId = a.AccountId,
                    CustomerId = a.CustomerId,
                    AccountNumber = a.AccountNumber,
                    AccountType = a.AccountType,
                    CurrentBalance = a.CurrentBalance,
                    InterestRate = a.InterestRate,
                    IsActive = a.IsActive,
                    IsBlocked = a.IsBlocked
                })
                .FirstOrDefaultAsync();
        }

        public async Task<AccountResponseDto> CreateAccountAsync(AccountCreateDto accountDto)
        {
            var customerExists = await _context.Customers.AnyAsync(c => c.CustomerId == accountDto.CustomerId);

            if (!customerExists)
                throw new Exception("Customer does not exist");

            var account = new Account
            {
                CustomerId = accountDto.CustomerId,
                AccountNumber = accountDto.AccountNumber,
                AccountType = accountDto.AccountType,
                CurrentBalance = accountDto.CurrentBalance,
                InterestRate = accountDto.InterestRate,
                IsActive = true,
                IsBlocked = false,
                OpenedDate = DateTime.Now
            };

            _context.Accounts.Add(account);

            await _context.SaveChangesAsync();

            return new AccountResponseDto
            {
                AccountId = account.AccountId,
                CustomerId = account.CustomerId,
                AccountNumber = account.AccountNumber,
                AccountType = account.AccountType,
                CurrentBalance = account.CurrentBalance,
                InterestRate = account.InterestRate,
                IsActive = account.IsActive,
                IsBlocked = account.IsBlocked
            };
        }

        public async Task<bool> BlockUnblockAccountAsync(int id)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == id);

            if (account == null)
                return false;

            account.IsBlocked = !(account.IsBlocked);

            await _context.SaveChangesAsync();

            return true;
        } 
    }
}