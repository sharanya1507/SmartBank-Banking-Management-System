using Microsoft.AspNetCore.Mvc;
using SmartBank.DataLayer.Interfaces;
using SmartBank.Shared.DTO;

namespace SmartBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankTransactionController : ControllerBase
    {
        private readonly IBankTransactionRepo _transactionRepo;

        public BankTransactionController(IBankTransactionRepo transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _transactionRepo.GetAllTransactionsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionCreateDto transactionDto)
        {
            try
            {
                var result =
                    await _transactionRepo.CreateTransactionAsync(transactionDto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("analytics")]
        public async Task<IActionResult> Analytics()
        {
            return Ok(await _transactionRepo.GetTransactionAnalyticsAsync());


        }
    }
}