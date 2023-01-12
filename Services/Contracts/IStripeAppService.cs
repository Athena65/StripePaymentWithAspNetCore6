using StripeWebApi.Models.Stripe;

namespace StripeWebApi.Services.Contracts
{
    public interface IStripeAppService
    {
        Task<StripeCustomer> AddStripeCustomer(AddStripeCustomer customer, CancellationToken ct);
        Task<StripePayment> AddStripePayment(AddStripePayment payment, CancellationToken ct);
    }
}
