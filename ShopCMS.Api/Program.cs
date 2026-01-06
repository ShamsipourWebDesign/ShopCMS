using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopCMS.Application.Contracts;
using ShopCMS.Application.RulesEngine;
using ShopCMS.Application.Services;
using ShopCMS.Domain.SaleEligibility;
using System.Threading;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// (Optional) اگر بعداً Controller داشتی:
builder.Services.AddControllers();

// register core rule engine
builder.Services.AddScoped(typeof(IRuleEngine<,>), typeof(RuleEngine<,>));

// register eligibility service
builder.Services.AddScoped<IEligibilityService, EligibilityService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

// Minimal API endpoint for sale eligibility check
app.MapPost("/eligibility/check", async (
    EligibilityContext context,
    IEligibilityService eligibilityService,
    CancellationToken cancellationToken) =>
{
    // use domain service to evaluate rules
    var result = await eligibilityService.CheckAsync(context, cancellationToken);

    // return result as JSON
    return Results.Ok(result);
});

app.Run();
