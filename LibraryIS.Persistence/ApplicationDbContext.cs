using System;
using System.Collections.Generic;
using System.Text;
using LibraryIS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryIS.Persistence
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>();
            modelBuilder.Entity<Book>();
            modelBuilder.Entity<CopyRequest>();
            modelBuilder.Entity<Evaluation>();
            modelBuilder.Entity<Genre>();
            modelBuilder.Entity<Language>();
            modelBuilder.Entity<PublishingHouse>();
            modelBuilder.Entity<ReaderProfile>();
            modelBuilder.Entity<ReservedBook>();
            modelBuilder.Entity<TakenBook>();
            modelBuilder.Entity<User>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
