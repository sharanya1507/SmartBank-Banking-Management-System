using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SmartBank.DataLayer.Models;

public partial class Account
{
    [Key]
    public int AccountId { get; set; }

    public int CustomerId { get; set; }

    public string AccountNumber { get; set; } = null!;

    public string AccountType { get; set; } = null!;

    public decimal CurrentBalance { get; set; }

    public decimal InterestRate { get; set; }

    public DateTime OpenedDate { get; set; }

    public bool IsActive { get; set; }

    public bool IsBlocked { get; set; }

    public virtual ICollection<BankTransaction> BankTransactions { get; set; } = new List<BankTransaction>();

    public virtual Customer Customer { get; set; } = null!;
}

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(e => e.AccountId).HasName("PK__Account__349DA5A6E4110C6F");

        builder.ToTable("Account");

        builder.HasIndex(e => e.AccountNumber, "UQ__Account__BE2ACD6F1A31BB9B").IsUnique();

        builder.Property(e => e.AccountNumber)
            .HasMaxLength(20)
            .IsUnicode(false);
        builder.Property(e => e.AccountType).HasMaxLength(30);
        builder.Property(e => e.CurrentBalance).HasColumnType("decimal(15, 2)");
        builder.Property(e => e.InterestRate).HasColumnType("decimal(5, 2)");
        builder.Property(e => e.IsActive).HasDefaultValue(true);
        builder.Property(e => e.OpenedDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        builder.HasOne(d => d.Customer).WithMany(p => p.Accounts)
            .HasForeignKey(d => d.CustomerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Account_Customer");
    }
}
