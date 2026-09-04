using SmartBank.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBank.DataLayer.Interfaces
{
    public interface ICustomerRepo
    {
        Task<List<CustomerResponseDto>> GetAllCustomersAsync();

        Task<CustomerResponseDto?> GetCustomerByIdAsync(int id);

        Task<string> CreateCustomerAsync(CustomerCreateDto customerDto);

        Task<CustomerResponseDto?> UpdateCustomerAsync(int id,CustomerUpdateDto customerDto);

        Task<bool> DeleteCustomerAsync(int id);



        Task<object?> GetFinancialSummaryAsync(int customerId);
    }
}
