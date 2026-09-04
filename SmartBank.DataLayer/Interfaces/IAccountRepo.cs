using SmartBank.Shared.DTO;

namespace SmartBank.DataLayer.Interfaces
{
    public interface IAccountRepo
    {
        Task<List<AccountResponseDto>> GetAllAccountsAsync();

        Task<AccountResponseDto?> GetAccountByIdAsync(int id);

        Task<AccountResponseDto> CreateAccountAsync(AccountCreateDto accountDto);

        Task<bool> BlockUnblockAccountAsync(int id);
    }
}