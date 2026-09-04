using Microsoft.EntityFrameworkCore;
using SmartBank.DataLayer.Interfaces;
using SmartBank.DataLayer.Models;
using SmartBank.Shared.DTO;

namespace SmartBank.DataLayer.Repo;

public class BankTransactionRepo : IBankTransactionRepo
{
    private readonly SmartBankDBContext _context;

    public BankTransactionRepo(SmartBankDBContext context)
    {
        _context = context;
    }

    public async Task<TransactionResponseDto>CreateTransactionAsync(TransactionCreateDto transactionDto)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == transactionDto.AccountId);

        if (account == null)
            throw new Exception("Account not found");

        if (account.IsBlocked == true)
            throw new Exception(
                "Transaction failed because account is blocked");

        bool isFraud = transactionDto.Amount > 100000;

        if (transactionDto.TransactionType == "Withdrawal")
        {
            if (account.CurrentBalance < transactionDto.Amount)
            {
                throw new Exception("Insufficient balance");
            }
            else
            {
                account.CurrentBalance -= transactionDto.Amount;
            }
        }

        else if (transactionDto.TransactionType == "Deposit")
        {
            account.CurrentBalance += transactionDto.Amount;
        }

        var transaction = new BankTransaction
        {
            AccountId = transactionDto.AccountId,
            TransactionType = transactionDto.TransactionType,
            Amount = transactionDto.Amount,
            Description = transactionDto.Description,
            TransactionDate = DateTime.Now,
            IsSuccessful = true,
            IsFraudSuspected = isFraud
        };

        _context.BankTransactions.Add(transaction);

        await _context.SaveChangesAsync();

        return new TransactionResponseDto
        {
            TransactionId = transaction.TransactionId,
            AccountId = transaction.AccountId,
            TransactionType = transaction.TransactionType,
            Amount = transaction.Amount,
            TransactionDate =transaction.TransactionDate,
            IsSuccessful = transaction.IsSuccessful,
            IsFraudSuspected = transaction.IsFraudSuspected
        };
    }

    public async Task<List<TransactionResponseDto>>GetAllTransactionsAsync()
    {
        return await _context.BankTransactions
            .Select(t => new TransactionResponseDto
            {
                TransactionId = t.TransactionId,
                AccountId = t.AccountId,
                TransactionType = t.TransactionType,
                Amount = t.Amount,
                TransactionDate = t.TransactionDate,
                IsSuccessful = t.IsSuccessful,
                IsFraudSuspected = t.IsFraudSuspected
            })
            .ToListAsync();
    }

    public async Task<object> GetTransactionAnalyticsAsync()
    {
        var transactions =_context.BankTransactions;

        var totalDeposits =await transactions
                .Where(t => t.TransactionType == "Deposit")
                .SumAsync(t => t.Amount);

        var totalWithdrawals =await transactions
                .Where(t => t.TransactionType == "Withdrawal")
                .SumAsync(t => t.Amount);

        var averageTransaction = await transactions
                .AverageAsync(t => t.Amount);

        var highestTransaction =await transactions
                .MaxAsync(t => t.Amount);

        return new
        {
            TotalDeposits = totalDeposits,
            TotalWithdrawals = totalWithdrawals,
            NetSavings = totalDeposits - totalWithdrawals,
            AverageTransaction =Math.Round(averageTransaction, 2),
            HighestTransaction = highestTransaction
        };
    }
}