using System.Text;
using APBD_Projekt;
using APBD_Projekt.Context;
using APBD_Projekt.Controllers;
using APBD_Projekt.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<CustomerController>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<CompanyController>();
builder.Services.AddScoped<CompanyService>();
builder.Services.AddScoped<KontraktController>();
builder.Services.AddScoped<KontraktService>();
builder.Services.AddScoped<PlatnoscController>();
builder.Services.AddScoped<PlatnoscService>();
builder.Services.AddScoped<PrzychodController>();
builder.Services.AddScoped<PrzychodService>();
builder.Services.AddScoped<ExchangeRateService>();

builder.Services.AddDbContext<CustomerDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,   
        ValidateAudience = true, 
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(2),
        ValidIssuer = "https://localhost:5001", 
        ValidAudience = "https://localhost:5001", 
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("8hTfGvUWfZXNz7Dk5JH7fF3sDq8fJ9x2"))
    };

    opt.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-expired", "true");
            }
            return Task.CompletedTask;
        }
    };
}).AddJwtBearer("IgnoreTokenExpirationScheme",opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,   //by who
        ValidateAudience = true, //for whom
        ValidateLifetime = false,
        ClockSkew = TimeSpan.FromMinutes(2),
        ValidIssuer = "https://localhost:5001", //should come from configuration
        ValidAudience = "https://localhost:5001", //should come from configuration
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("8hTfGvUWfZXNz7Dk5JH7fF3sDq8fJ9x2"))
    };
});    




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

