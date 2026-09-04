using SmartBank.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBank.DataLayer.Interfaces
{
    public interface ICustomerRepo
    {
        Task<List<CustomerResponseDto>> GetAllCustomersAsync();

        Task<CustomerResponseDto?> GetCustomerByIdAsync(Guid customerCode);

        Task<string> CreateCustomerAsync(CustomerCreateDto customerDto);

        Task<CustomerResponseDto?> UpdateCustomerAsync(Guid customerCode,CustomerUpdateDto customerDto);

        Task<bool> DeleteCustomerAsync(Guid customerCode);



        Task<object?> GetFinancialSummaryAsync(Guid customerCode);
    }
}
