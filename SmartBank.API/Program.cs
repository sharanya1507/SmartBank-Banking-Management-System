using Microsoft.EntityFrameworkCore;
using SmartBank.DataLayer;
using SmartBank.DataLayer.Interfaces;
using SmartBank.DataLayer.Models;
using SmartBank.DataLayer.Repo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SmartBankDBContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString(
                "DefaultConnection")));

builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();

builder.Services.AddScoped<IAccountRepo, AccountRepo>();

builder.Services.AddScoped< IBankTransactionRepo, BankTransactionRepo>();

builder.Services.AddScoped<ILoanRepo, LoanRepo>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();