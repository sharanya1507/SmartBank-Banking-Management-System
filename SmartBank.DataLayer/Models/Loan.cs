using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SmartBank.DataLayer.Models;

public partial class Loan
{
    [Key]
    public int LoanId { get; set; }

    public int CustomerId { get; set; }

    public string LoanType { get; set; } = null!;

    public decimal PrincipalAmount { get; set; }

    public decimal InterestRate { get; set; }

    public int LoanTenureMonths { get; set; }

    public decimal Emiamount { get; set; }

    public decimal RemainingAmount { get; set; }

    public DateOnly LoanStartDate { get; set; }

    public bool IsApproved { get; set; }

    public bool IsClosed { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}

public class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.HasKey(e => e.LoanId).HasName("PK__Loan__4F5AD457C8C21C70");

        builder.ToTable("Loan");

        builder.Property(e => e.Emiamount)
            .HasColumnType("decimal(15, 2)")
            .HasColumnName("EMIAmount");
        builder.Property(e => e.InterestRate).HasColumnType("decimal(5, 2)");
        builder.Property(e => e.LoanStartDate).HasDefaultValueSql("(getdate())");
        builder.Property(e => e.LoanType).HasMaxLength(50);
        builder.Property(e => e.PrincipalAmount).HasColumnType("decimal(15, 2)");
        builder.Property(e => e.RemainingAmount).HasColumnType("decimal(15, 2)");

        builder.HasOne(d => d.Customer).WithMany(p => p.Loans)
            .HasForeignKey(d => d.CustomerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Loan_Customer");
    }
}
