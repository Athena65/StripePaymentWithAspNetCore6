using Microsoft.AspNetCore.Mvc;
using StripeWebApi.Models.Stripe;
using StripeWebApi.Services;
using StripeWebApi.Services.Contracts;

namespace StripeWebApi.Controllers
{
    [ApiController]
    [Route("[action]")]
    public class StripeController : ControllerBase
    {
        private readonly IStripeAppService _stripeService;

        public StripeController(IStripeAppService stripeService)
        {
            _stripeService = stripeService;
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomer(AddStripeCustomer customer, CancellationToken ct)
        {
            try
            {
                return Ok(await _stripeService.AddStripeCustomer(customer, ct));
            }
            catch (Exception ex)
            {
                var response = new ServiceResponse();
                response.Success = false;
                response.Message= ex.Message;
                return BadRequest(response);    
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddPayment(AddStripePayment payment, CancellationToken ct)
        {
            try
            {
                return Ok(await _stripeService.AddStripePayment(payment, ct));
            }
            catch (Exception ex)
            {
                var response = new ServiceResponse();
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

    }
}
