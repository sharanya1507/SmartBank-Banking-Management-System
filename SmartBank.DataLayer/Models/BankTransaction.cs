using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SmartBank.DataLayer.Models;

public partial class BankTransaction
{
    [Key]
    public int TransactionId { get; set; }

    public int AccountId { get; set; }

    public string TransactionType { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }

    public string? Description { get; set; }

    public bool IsSuccessful { get; set; }

    public bool IsFraudSuspected { get; set; }

    public virtual Account Account { get; set; } = null!;
}


public class BankTransactionConfiguration : IEntityTypeConfiguration<BankTransaction>
{
    public void Configure(EntityTypeBuilder<BankTransaction> builder)
    {
        builder.HasKey(e => e.TransactionId).HasName("PK__BankTran__55433A6B60011A42");

        builder.ToTable("BankTransaction");

        builder.Property(e => e.Amount).HasColumnType("decimal(15, 2)");
        builder.Property(e => e.Description).HasMaxLength(200);
        builder.Property(e => e.IsSuccessful).HasDefaultValue(true);
        builder.Property(e => e.TransactionDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.TransactionType)
            .HasMaxLength(20)
            .IsUnicode(false);

        builder.HasOne(d => d.Account).WithMany(p => p.BankTransactions)
            .HasForeignKey(d => d.AccountId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Transaction_Account");
    }
}
