using ShopCMS.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using ShopCMS.Application.Contracts;
using ShopCMS.Application.Services;
using ShopCMS.Application.Services.Providers;

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
