using Stripe;
using StripeWebApi.Services.Application;
using StripeWebApi.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);
IConfiguration config = builder.Configuration;

// Add services to the container.

builder.Services.AddScoped<IStripeAppService, StripeAppService>()
                .AddScoped<ChargeService>()
                .AddScoped<CustomerService>()
                .AddScoped<TokenService>();
StripeConfiguration.ApiKey = config.GetValue<string>("StripeSettings:SecretKey");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
