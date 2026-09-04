using Microsoft.AspNetCore.Mvc;
using SmartBank.DataLayer.Interfaces;
using SmartBank.Shared.DTO;

namespace SmartBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomerController(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerRepo.GetAllCustomersAsync();

            return Ok(customers);
        }

        [HttpGet("{customerCode}")]
        public async Task<IActionResult> GetCustomerById(Guid customerCode)
        {
            var customer = await _customerRepo.GetCustomerByIdAsync(customerCode);

            if (customer == null)
                return NotFound("Customer not found");

            return Ok(customer);
        }

        [HttpPost]
        public async Task<string> CreateCustomer(CustomerCreateDto customerDto)
        {
            var customer = await _customerRepo.CreateCustomerAsync(customerDto);

            return "Successfully Created the Customer";
        }

        [HttpPut("{customerCode}")]
        public async Task<IActionResult> UpdateCustomer(Guid customerCode,CustomerUpdateDto customerDto)
        {
            var customer = await _customerRepo.UpdateCustomerAsync(customerCode, customerDto);

            if (customer == null)
                return NotFound("Customer not found");

            return Ok(customer);
        }

        [HttpDelete("{customerCode}")]
        public async Task<IActionResult> DeleteCustomer(Guid customerCode)
        {
            var result = await _customerRepo.DeleteCustomerAsync(customerCode);

            if (!result)
                return NotFound("Customer not found");

            return Ok("Customer deleted successfully");
        }



        [HttpGet("{id}/financial-summary")]
        public async Task<IActionResult> GetFinancialSummary(Guid customercode)
        {
            var summary = await _customerRepo.GetFinancialSummaryAsync(customercode);

            if (summary == null)
                return NotFound("Customer not found");

            return Ok(summary);
        }
    }
}