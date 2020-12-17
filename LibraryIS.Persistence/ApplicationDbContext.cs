using System;
using System.Collections.Generic;
using System.Text;
using LibraryIS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryIS.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>();
            modelBuilder.Entity<Book>();
            modelBuilder.Entity<CopyBook>();
            modelBuilder.Entity<ElectronicCopyRequest>();
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
