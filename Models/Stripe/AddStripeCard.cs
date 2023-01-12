namespace StripeWebApi.Models.Stripe
{
  public record AddStripeCard
        (
            string Name, //immutable property
            string CardNumber,
            string ExpirationYear,
            string ExpirationMonth,
            string Cvc

      );
    
}
