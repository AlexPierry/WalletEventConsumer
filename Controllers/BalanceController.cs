using WalletEventConsumer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Text.Json;

namespace WalletEventConsumer.Controllers
{
    [ApiController]
    [Route("balances")]
    public class BalanceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BalanceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetBalance(string accountId)
        {
            Console.WriteLine(JsonSerializer.Serialize("AccountID received: " + accountId));

            var balance = await _context.BalanceEvents.Include(m => m.Payload).OrderByDescending(b => b.Id)
                .FirstOrDefaultAsync(e => e.Payload.AccountIdFrom == accountId);

            Console.WriteLine("Balance From: " + JsonSerializer.Serialize(balance));

            if (balance != null)
            {
                return Ok(balance.Payload.BalanceAccountIdFrom);
            }

            balance = await _context.BalanceEvents.Include(m => m.Payload).OrderByDescending(b => b.Id)
                .FirstOrDefaultAsync(e => e.Payload.AccountIdTo == accountId);

            Console.WriteLine("Balance To: " + JsonSerializer.Serialize(balance));

            if (balance != null)
            {
                return Ok(balance.Payload.BalanceAccountIdTo);
            }

            return NotFound();
        }
    }
}
