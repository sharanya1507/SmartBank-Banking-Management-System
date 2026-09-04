using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SmartBank.DataLayer.Models;

public partial class Customer
{
    [Key]
    public int CustomerId { get; set; }

    public Guid? CustomerCode { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public decimal MonthlyIncome { get; set; }

    public int CreditScore { get; set; }

    public bool IsActive { get; set; }

    public bool? IsKycVerified { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D8C362B47B");

        builder.ToTable("Customer");

        builder.HasIndex(e => e.Email, "UQ__Customer__A9D1053416D28C8D").IsUnique();

        builder.Property(e => e.CreatedDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.CustomerCode).HasDefaultValueSql("(newid())");
        builder.Property(e => e.Email)
            .HasMaxLength(100)
            .IsUnicode(false);
        builder.Property(e => e.FullName).HasMaxLength(100);
        builder.Property(e => e.Gender)
            .HasMaxLength(1)
            .IsUnicode(false)
            .IsFixedLength();
        builder.Property(e => e.IsActive).HasDefaultValue(true);
        builder.Property(e => e.IsKycVerified).HasDefaultValue(false);
        builder.Property(e => e.MonthlyIncome).HasColumnType("decimal(12, 2)");
        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(15)
            .IsUnicode(false);
    }
}
