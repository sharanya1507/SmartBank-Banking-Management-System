using Microsoft.AspNetCore.Mvc;
using SmartBank.DataLayer.Interfaces;
using SmartBank.Shared.DTO;

namespace SmartBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanRepo _loanRepo;

        public LoanController(ILoanRepo loanRepo)
        {
            _loanRepo = loanRepo; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _loanRepo.GetAllLoansAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(LoanCreateDto loanDto)
        {
            return Ok(
                await _loanRepo.CreateLoanAsync(loanDto));
        }
    }
}