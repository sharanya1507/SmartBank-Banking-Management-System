using Microsoft.AspNetCore.Mvc;
using SmartBank.DataLayer.Interfaces;
using SmartBank.Shared.DTO;

namespace SmartBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepo _accountRepo;

        public AccountController(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _accountRepo.GetAllAccountsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var account = await _accountRepo.GetAccountByIdAsync(id);

            if (account == null)
                return NotFound();

            return Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> Create( AccountCreateDto accountDto)
        {
            var account = await _accountRepo.CreateAccountAsync(accountDto);

            return Ok(account);
        }

        [HttpPut("{id}/block-unblock")]
        public async Task<IActionResult> BlockUnblock(int id)
        {
            var result =
                await _accountRepo.BlockUnblockAccountAsync(id);

            if (!result)
                return NotFound();

            return Ok("Account status changed successfully");
        }
    }
}