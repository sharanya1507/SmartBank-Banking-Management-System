using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartBank.DataLayer.Models;

namespace SmartBank.DataLayer;

public partial class SmartBankDBContext : DbContext
{
    public SmartBankDBContext(DbContextOptions<SmartBankDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<BankTransaction> BankTransactions { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmartBankDBContext).Assembly);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
