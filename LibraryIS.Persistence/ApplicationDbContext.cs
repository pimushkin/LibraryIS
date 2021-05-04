using LibraryIS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryIS.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(x =>
            {
                x.ToTable("Author").HasKey(k => k.Id);
                x.Property(p => p.Name);
                x.HasMany(p => p.Books).WithMany(p => p.Authors);
            });
            modelBuilder.Entity<Book>(x =>
            {
                x.ToTable("Book").HasKey(k => k.Id);
                x.Property(p => p.Name);
                x.HasMany(p => p.Authors).WithMany(p => p.Books);
                x.Property(p => p.PublicationDate);
                x.Property(p => p.Description);
                x.Property(p => p.PageCount);
                x.Property(p => p.Rating);
                x.HasMany(p => p.PublishingHouses).WithMany(p => p.Books);
                x.HasMany(p => p.Genres).WithMany(p => p.Books);
                x.HasOne(p => p.BookLanguage).WithMany();
            });
            modelBuilder.Entity<ElectronicCopyRequest>(x =>
            {
                x.ToTable("ElectronicCopyRequest").HasKey(k => k.Id);
                x.HasOne(p => p.RequestingReader).WithMany();
                x.HasOne(p => p.Book).WithMany();
                x.Property(p => p.Status);
                x.Property(p => p.PagesRange);
            });
            modelBuilder.Entity<Evaluation>(x =>
            {
                x.ToTable("Evaluation").HasKey(k => k.Id);
                x.Property(p => p.Rating);
                x.HasOne(p => p.Book).WithMany();
                x.HasOne(p => p.ReaderProfile).WithMany(p => p.Evaluations);
            });
            modelBuilder.Entity<Genre>(x =>
            {
                x.ToTable("Genre").HasKey(k => k.Id);
                x.Property(p => p.Name);
                x.HasMany(p => p.Books).WithMany(p => p.Genres);
                x.HasMany(p => p.ReaderProfiles).WithMany(p => p.TopGenres);
            });
            modelBuilder.Entity<Language>(x =>
            {
                x.ToTable("Language").HasKey(k => k.Id);
                x.Property(p => p.Name);
            });
            modelBuilder.Entity<PublishingHouse>(x =>
            {
                x.ToTable("PublishingHouse").HasKey(k => k.Id);
                x.HasMany(p => p.Books).WithMany(p => p.PublishingHouses);
            });
            modelBuilder.Entity<ReaderProfile>(x =>
            {
                x.ToTable("ReaderProfile").HasKey(k => k.Id);
                x.HasOne(p => p.User).WithOne(p => p.ReaderProfile);
                x.Property(p => p.LibraryCard);
                x.Property(p => p.PassportSeriesAndNumber);
                x.Property(p => p.BornYear);
                x.HasMany(p => p.TakenBooks).WithOne(p => p.Reader);
                x.HasMany(p => p.ReservedBooks).WithOne(p => p.Reader);
                x.HasMany(p => p.ElectronicCopyRequests).WithOne(p => p.RequestingReader);
                x.HasMany(p => p.TopGenres).WithMany(p => p.ReaderProfiles);
                x.HasMany(p => p.Evaluations).WithOne(p => p.ReaderProfile);
            });
            modelBuilder.Entity<ReservedBook>(x =>
            {
                x.ToTable("ReservedBook").HasKey(k => k.Id);
                x.HasOne(p => p.BookedBook).WithMany();
                x.HasOne(p => p.Reader).WithMany(p => p.ReservedBooks);
                x.Property(p => p.StartDate);
                x.Property(p => p.EndDate);
            });
            modelBuilder.Entity<TakenBook>(x =>
            {
                x.ToTable("TakenBook").HasKey(k => k.Id);
                x.HasOne(p => p.Reader).WithMany(p => p.TakenBooks);
                x.HasOne(p => p.Book).WithMany();
                x.Property(p => p.ReturnDate);
                x.Property(p => p.ReceiveDate);
            });
            modelBuilder.Entity<User>(x =>
            {
                x.ToTable("User").HasKey(k => k.Id);
                x.Property(p => p.FirstName);
                x.Property(p => p.LastName);
                x.Property(p => p.Email);
                x.Property(p => p.Password);
                x.Property(p => p.IsApproved);
                x.Property(p => p.CreationDate);
                x.Property(p => p.CreationDate);
                x.Property(p => p.IsAdmin);
                x.HasOne(p => p.ReaderProfile).WithOne(p => p.User).HasForeignKey<ReaderProfile>(p => p.Id);
            });
        }
    }
}