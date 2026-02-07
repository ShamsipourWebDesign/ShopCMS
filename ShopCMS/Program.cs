using Microsoft.EntityFrameworkCore;
using Polly;
using ShopCMS.Application.Contracts;
using ShopCMS.Application.Services;
using ShopCMS.Application.Services.Providers;
using ShopCMS.Domain.Entities.PriceLockApi;
using ShopCMS.Domain.Interfaces;
using ShopCMS.Infrastructure.External;
using ShopCMS.Infrastructure.Persistence.Context;
using ShopCMS.Infrastructure.PriceLock;
using System;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<CurrencyService>();
// register volatility service
builder.Services.AddScoped<IVolatilityService, VolatilityService>();
builder.Services.AddScoped<IEligibilityService, EligibilityService>();
builder.Services.AddSingleton<FakeVolatilityProvider>();
builder.Services.AddScoped<IPricingService, PricingService>();


builder.Services.AddScoped<IPriceLockApiRepository, PriceLockApiRepository>();

builder.Services.AddScoped<PriceLockService>(); 
builder.Services.AddScoped<IPriceLockApiRepository, PriceLockApiRepository>();



builder.Services.AddHttpClient<ShopCMS.Domain.Interfaces.ICurrencyRateProvider, CurrencyRateProvider>(client =>
{
    client.BaseAddress = new Uri("https://api.exchangerate.com/");
    client.Timeout = TimeSpan.FromSeconds(3);
})
.AddTransientHttpErrorPolicy(policy =>
    policy.WaitAndRetryAsync(3, retry =>
        TimeSpan.FromMilliseconds(200 * retry)));



builder.Services.AddHttpClient<IExchangeRateClient, ExternalExchangeRateClient>(client =>
{
    client.BaseAddress = new Uri("https://api.exchangerate-api.com/v4/");
    client.Timeout = TimeSpan.FromSeconds(3);
})
.AddTransientHttpErrorPolicy(policy =>
    policy.WaitAndRetryAsync(3, retry =>
        TimeSpan.FromMilliseconds(200 * retry)));





var app = builder.Build();

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.StatusCode = 503;
        await context.Response.WriteAsJsonAsync(new
        {
            error = "External service unavailable"
        });
    });
});


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
