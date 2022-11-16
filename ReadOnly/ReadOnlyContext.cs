using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ReadOnly;

public partial class ReadOnlyContext : DbContext
{
    public ReadOnlyContext(DbContextOptions<ReadOnlyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bookmark> Bookmarks { get; set; }


    public virtual DbSet<CalculationBookmark> CalculationBookmarks { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bookmark>(entity =>
        {
            entity.ToTable("bookmarks", "calculations");

            entity.HasIndex(e => e.UserId, "IX_bookmarks_UserId").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
        });


        modelBuilder.Entity<CalculationBookmark>(entity =>
        {
            entity.ToTable("calculationBookmarks", "calculations");

            entity.HasIndex(e => e.BookmarksId, "IX_calculationBookmarks_BookmarksId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CalculationId).HasMaxLength(11);

            entity.HasOne(d => d.Bookmarks).WithMany(p => p.CalculationBookmarks).HasForeignKey(d => d.BookmarksId);
        });
       
        modelBuilder.HasSequence("bookmarksseq", "calculations").IncrementsBy(10);
        modelBuilder.HasSequence("calculationbookmarksseq", "calculations").IncrementsBy(10);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
