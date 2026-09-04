using SmartBank.Shared.DTO;

namespace SmartBank.DataLayer.Interfaces
{
    public interface IBankTransactionRepo
    {
        Task<List<TransactionResponseDto>> GetAllTransactionsAsync();
        Task<TransactionResponseDto> CreateTransactionAsync(TransactionCreateDto transactionDto);

        Task<object> GetTransactionAnalyticsAsync();
    }
}