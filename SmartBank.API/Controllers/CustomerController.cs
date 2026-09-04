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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerRepo.GetCustomerByIdAsync(id);

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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id,CustomerUpdateDto customerDto)
        {
            var customer = await _customerRepo.UpdateCustomerAsync(id, customerDto);

            if (customer == null)
                return NotFound("Customer not found");

            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result =
                await _customerRepo.DeleteCustomerAsync(id);

            if (!result)
                return NotFound("Customer not found");

            return Ok("Customer deleted successfully");
        }



        [HttpGet("{id}/financial-summary")]
        public async Task<IActionResult> GetFinancialSummary(int id)
        {
            var summary =
                await _customerRepo.GetFinancialSummaryAsync(id);

            if (summary == null)
                return NotFound("Customer not found");

            return Ok(summary);
        }
    }
}