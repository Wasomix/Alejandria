using System;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Devon4Net.WebAPI.Implementation.Domain.Database
{
    public partial class AlejandriaContext : DbContext
    {
        public AlejandriaContext()
        {
        }

        public AlejandriaContext(DbContextOptions<AlejandriaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<AuthorBook> AuthorBook { get; set; }
        public virtual DbSet<Book> Book { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<AuthorBook>(entity =>
            {
                entity.ToTable("Author_Book");

                entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

                entity.Property(e => e.AuthorId).HasColumnName("Author_id");

                entity.Property(e => e.BookId).HasColumnName("Book_id");

                entity.Property(e => e.PublishDate)
                    .HasColumnName("Publish_Date")
                    .HasColumnType("date");

                entity.Property(e => e.ValidityDate)
                    .HasColumnName("Validity_Date")
                    .HasColumnType("date");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.AuthorBook)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("author_book_fk");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.AuthorBook)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("author_book_fk_1");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

                entity.Property(e => e.Genere)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Summary)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
