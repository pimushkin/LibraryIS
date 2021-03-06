﻿// <auto-generated />
using System;
using LibraryIS.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LibraryIS.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201218062547_AddUserToProfile")]
    partial class AddUserToProfile
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("AuthorBook", b =>
                {
                    b.Property<Guid>("AuthorsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BooksId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AuthorsId", "BooksId");

                    b.HasIndex("BooksId");

                    b.ToTable("AuthorBook");
                });

            modelBuilder.Entity("BookGenre", b =>
                {
                    b.Property<Guid>("BooksId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GenresId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BooksId", "GenresId");

                    b.HasIndex("GenresId");

                    b.ToTable("BookGenre");
                });

            modelBuilder.Entity("BookPublishingHouse", b =>
                {
                    b.Property<Guid>("BooksId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PublishingHousesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BooksId", "PublishingHousesId");

                    b.HasIndex("PublishingHousesId");

                    b.ToTable("BookPublishingHouse");
                });

            modelBuilder.Entity("GenreReaderProfile", b =>
                {
                    b.Property<Guid>("ReaderProfilesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TopGenresId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ReaderProfilesId", "TopGenresId");

                    b.HasIndex("TopGenresId");

                    b.ToTable("GenreReaderProfile");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

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

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.CopyBook", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("CopyBook");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.ElectronicCopyRequest", b =>
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

                    b.ToTable("ElectronicCopyRequest");
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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PublishingHouse");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.ReaderProfile", b =>
                {
                    b.Property<Guid>("Id")
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

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("AuthorBook", b =>
                {
                    b.HasOne("LibraryIS.Core.Entities.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryIS.Core.Entities.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookGenre", b =>
                {
                    b.HasOne("LibraryIS.Core.Entities.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryIS.Core.Entities.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookPublishingHouse", b =>
                {
                    b.HasOne("LibraryIS.Core.Entities.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryIS.Core.Entities.PublishingHouse", null)
                        .WithMany()
                        .HasForeignKey("PublishingHousesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GenreReaderProfile", b =>
                {
                    b.HasOne("LibraryIS.Core.Entities.ReaderProfile", null)
                        .WithMany()
                        .HasForeignKey("ReaderProfilesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryIS.Core.Entities.Genre", null)
                        .WithMany()
                        .HasForeignKey("TopGenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.Book", b =>
                {
                    b.HasOne("LibraryIS.Core.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.CopyBook", b =>
                {
                    b.HasOne("LibraryIS.Core.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.ElectronicCopyRequest", b =>
                {
                    b.HasOne("LibraryIS.Core.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId");

                    b.HasOne("LibraryIS.Core.Entities.ReaderProfile", null)
                        .WithMany("ElectronicCopyRequests")
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

            modelBuilder.Entity("LibraryIS.Core.Entities.ReaderProfile", b =>
                {
                    b.HasOne("LibraryIS.Core.Entities.User", "User")
                        .WithOne("ReaderProfile")
                        .HasForeignKey("LibraryIS.Core.Entities.ReaderProfile", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
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

            modelBuilder.Entity("LibraryIS.Core.Entities.ReaderProfile", b =>
                {
                    b.Navigation("ElectronicCopyRequests");

                    b.Navigation("Evaluations");

                    b.Navigation("ReservedBooks");

                    b.Navigation("TakenBooks");
                });

            modelBuilder.Entity("LibraryIS.Core.Entities.User", b =>
                {
                    b.Navigation("ReaderProfile");
                });
#pragma warning restore 612, 618
        }
    }
}
