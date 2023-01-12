using Stripe;
using StripeWebApi.Models.Stripe;
using StripeWebApi.Services.Contracts;

namespace StripeWebApi.Services.Application
{
    public class StripeAppService:IStripeAppService
    {
        private readonly ChargeService _chargeService;
        private readonly CustomerService _customerService;
        private readonly TokenService _tokenService;

        public StripeAppService( ChargeService chargeService,CustomerService customerService,TokenService tokenService )
        {
            _chargeService = chargeService;
            _customerService = customerService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Create Customer at Stripe
        /// </summary>
        /// <param name="customer">Stripe Customer</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>newly created Stripe Customer</returns>

        public async Task<StripeCustomer> AddStripeCustomer(AddStripeCustomer customer, CancellationToken ct)
        {
           var tokenOpt= new TokenCreateOptions
           {
               Card=new TokenCardOptions
               {
                   Name=customer.Name,
                   Number=customer.CreditCard.CardNumber,
                   ExpYear=customer.CreditCard.ExpirationYear,
                   ExpMonth=customer.CreditCard.ExpirationMonth,    
                   Cvc=customer.CreditCard.Cvc
               }
           };

            Token stripeToken=await _tokenService.CreateAsync(tokenOpt,null,ct);

            var customerOptions = new CustomerCreateOptions
            {
                Name = customer.Name,
                Email = customer.Email,
                Source = stripeToken.Id
            };

            Customer createdCustomer=await _customerService.CreateAsync(customerOptions,null,ct);

            return new StripeCustomer(createdCustomer.Name, createdCustomer.Email, createdCustomer.Id);
        }

        /// <summary>
        /// Adds a new payment at Stripe
        /// </summary>
        /// <param name="payment">Stripe payment</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<StripePayment> AddStripePayment(AddStripePayment payment, CancellationToken ct)
        {
            var paymentOpts = new ChargeCreateOptions
            {
                Customer = payment.CustomerId,
                ReceiptEmail = payment.ReceiptEmail,
                Description = payment.Description,
                Currency = payment.Currency,
                Amount = payment.Amount,
            };

            var createdPayment= await _chargeService.CreateAsync(paymentOpts,null,ct);

            return new StripePayment
                (
                createdPayment.CustomerId,
                createdPayment.ReceiptEmail,
                createdPayment.Description,
                createdPayment.Currency,
                createdPayment.Amount,
                createdPayment.Id
                );
        }
    }
}
