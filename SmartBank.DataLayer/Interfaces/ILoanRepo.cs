using SmartBank.Shared.DTO;

namespace SmartBank.DataLayer.Interfaces;

public interface ILoanRepo
{
    Task<LoanResponseDto> CreateLoanAsync(LoanCreateDto loanDto);

    Task<List<LoanResponseDto>> GetAllLoansAsync();
} 