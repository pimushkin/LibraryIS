using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryIS.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201217003309_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("LibraryIS.Core.Entities.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Author");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("LanguageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PageCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublicationDate")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Rating")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.CopyRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PagesRange")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ReaderProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RequestStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("ReaderProfileId");

                    b.ToTable("CopyRequest");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.Evaluation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Evaluation");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ReaderProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("ReaderProfileId");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Language");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.PublishingHouse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("PublishingHouse");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.ReaderProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BornYear")
                        .HasColumnType("datetime2");

                    b.Property<string>("LibraryCard")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassportSeriesAndNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ReaderProfile");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.ReservedBook", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ReaderProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("ReaderProfileId");

                    b.ToTable("ReservedBook");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.TakenBook", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ReaderProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ReceiveDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("ReaderProfileId");

                    b.ToTable("TakenBook");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ReaderProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ReaderProfileId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.Author", b =>
                {
                    b.HasOne("LibraryIS.Core.Entities.Book", null)
                        .WithMany("Authors")
                        .HasForeignKey("BookId");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.Book", b =>
                {
                    b.HasOne("LibraryIS.Core.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.CopyRequest", b =>
                {
                    b.HasOne("LibraryIS.Core.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId");

                    b.HasOne("LibraryIS.Core.Entities.ReaderProfile", null)
                        .WithMany("CopyRequests")
                        .HasForeignKey("ReaderProfileId");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.Evaluation", b =>
                {
                    b.HasOne("LibraryIS.Core.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId");

                    b.HasOne("LibraryIS.Core.Entities.ReaderProfile", "Profile")
                        .WithMany("Evaluations")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.Genre", b =>
                {
                    b.HasOne("LibraryIS.Core.Entities.Book", null)
                        .WithMany("Genres")
                        .HasForeignKey("BookId");

                    b.HasOne("LibraryIS.Core.Entities.ReaderProfile", null)
                        .WithMany("TopGenres")
                        .HasForeignKey("ReaderProfileId");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.PublishingHouse", b =>
                {
                    b.HasOne("LibraryIS.Core.Entities.Book", null)
                        .WithMany("PublishingHouses")
                        .HasForeignKey("BookId");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.ReservedBook", b =>
                {
                    b.HasOne("LibraryIS.Core.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId");

                    b.HasOne("LibraryIS.Core.Entities.ReaderProfile", null)
                        .WithMany("ReservedBooks")
                        .HasForeignKey("ReaderProfileId");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.TakenBook", b =>
                {
                    b.HasOne("LibraryIS.Core.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId");

                    b.HasOne("LibraryIS.Core.Entities.ReaderProfile", null)
                        .WithMany("TakenBooks")
                        .HasForeignKey("ReaderProfileId");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.User", b =>
                {
                    b.HasOne("LibraryIS.Core.Entities.ReaderProfile", "ReaderProfile")
                        .WithMany()
                        .HasForeignKey("ReaderProfileId");

                    b.Navigation("ReaderProfile");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.Book", b =>
                {
                    b.Navigation("Authors");

                    b.Navigation("Genres");

                    b.Navigation("PublishingHouses");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.ReaderProfile", b =>
                {
                    b.Navigation("CopyRequests");

                    b.Navigation("Evaluations");

                    b.Navigation("ReservedBooks");

                    b.Navigation("TakenBooks");

                    b.Navigation("TopGenres");
                });
#pragma warning restore 612, 618
        }
    }
}
