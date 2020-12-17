using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryIS.Application.DTOs;
using LibraryIS.Application.Interfaces;
using LibraryIS.Application.Services;
using LibraryIS.Core.Entities;
using LibraryIS.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace LibraryIS.UnitTests
{
    public class Tests
    {
        private IBooksCatalogService _booksCatalogService;
        private static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

            return dbSetMock;
        }
        private List<PublishingHouse> PublishingHouses { get; set; }
        private List<Genre> Genres { get; set; }
        private List<Language> Languages { get; set; }
        private List<Author> Authors { get; set; }
        private List<BookPreviewDto> BookPreviewDtos { get; set; }

        [SetUp]
        public void Setup()
        {
            var phoenix = new PublishingHouse
            {
                Id = Guid.NewGuid(),
                Name = "������"
            };
            var flamingos = new PublishingHouse
            {
                Id = Guid.NewGuid(),
                Name = "��������"
            };
            var juventus = new PublishingHouse
            {
                Id = Guid.NewGuid(),
                Name = "������"
            };
            PublishingHouses = new List<PublishingHouse> {phoenix, juventus, flamingos};

            var fantastic = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "����������"
            };
            var drama = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "�����"
            };
            var horror = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "�����"
            };
            Genres = new List<Genre> {fantastic, drama, horror};

            var author1 = new Author
            {
                Id = Guid.NewGuid(),
                Name = "������"
            };
            var author2 = new Author
            {
                Id = Guid.NewGuid(),
                Name = "�������"
            };
            var author3 = new Author
            {
                Id = Guid.NewGuid(),
                Name = "����������"
            };
            Authors = new List<Author> {author1, author2, author3};

            var russian = new Language
            {
                Id = Guid.NewGuid(),
                Name = "�������"
            };
            var english = new Language
            {
                Id = Guid.NewGuid(),
                Name = "����������"
            };
            Languages = new List<Language> {russian, english};

            var books = new List<Book>
            {
                new Book
                {
                    Id = Guid.NewGuid(), Name = "�����1", Rating = 3,
                    PublishingHouses = new List<PublishingHouse> {juventus},
                    Genres = new List<Genre> { fantastic }, Authors = new List<Author> {author1, author2},
                    Description = "��������", Language = russian, PageCount = 438,
                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
                },
                new Book
                {
                    Id = Guid.NewGuid(), Name = "�����2", Rating = 4.5,
                    PublishingHouses = new List<PublishingHouse> {juventus},
                    Genres = new List<Genre> { horror, fantastic }, Authors = new List<Author> {author2, author1},
                    Description = "��������", Language = english, PageCount = 624,
                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
                },
                new Book
                {
                    Id = Guid.NewGuid(), Name = "�����3", Rating = 4,
                    PublishingHouses = new List<PublishingHouse> {juventus},
                    Genres = new List<Genre> {horror}, Authors = new List<Author> { author3 },
                    Description = "��������", Language = russian, PageCount = 528,
                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
                },
                new Book
                {
                    Id = Guid.NewGuid(), Name = "�����4", Rating = 5,
                    PublishingHouses = new List<PublishingHouse> {juventus},
                    Genres = new List<Genre> {horror, fantastic, drama}, Authors = new List<Author> { author1 },
                    Description = "��������", Language = english, PageCount = 457,
                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
                },
                new Book
                {
                    Id = Guid.NewGuid(), Name = "�����5", Rating = 2.6,
                    PublishingHouses = new List<PublishingHouse> {juventus},
                    Genres = new List<Genre> {drama, fantastic}, Authors = new List<Author> { author1, author3 },
                    Description = "��������", Language = russian, PageCount = 125,
                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
                },
                new Book
                {
                    Id = Guid.NewGuid(), Name = "�����6", Rating = 4.7,
                    PublishingHouses = new List<PublishingHouse> {juventus},
                    Genres = new List<Genre> {drama}, Authors = new List<Author> {author1},
                    Description = "��������", Language = english, PageCount = 724,
                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
                },
                new Book
                {
                    Id = Guid.NewGuid(), Name = "�����7", Rating = 3.2,
                    PublishingHouses = new List<PublishingHouse> {juventus},
                    Genres = new List<Genre> {drama, fantastic}, Authors = new List<Author> {author2},
                    Description = "��������", Language = english, PageCount = 457,
                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
                },
                new Book
                {
                    Id = Guid.NewGuid(), Name = "�����8", Rating = 3.6,
                    PublishingHouses = new List<PublishingHouse> {juventus},
                    Genres = new List<Genre> {drama, fantastic}, Authors = new List<Author> {author3},
                    Description = "��������", Language = russian, PageCount = 765,
                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
                },
                new Book
                {
                    Id = Guid.NewGuid(), Name = "�����9", Rating = 1,
                    PublishingHouses = new List<PublishingHouse> {juventus},
                    Genres = new List<Genre> {horror}, Authors = new List<Author> {author1, author2, author3},
                    Description = "��������", Language = english, PageCount = 346,
                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
                },
                new Book
                {
                    Id = Guid.NewGuid(), Name = "�����10", Rating = 2.8,
                    PublishingHouses = new List<PublishingHouse> {juventus},
                    Genres = new List<Genre> {fantastic}, Authors = new List<Author> {author2, author3},
                    Description = "��������", Language = russian, PageCount = 124,
                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
                },
                new Book
                {
                    Id = Guid.NewGuid(), Name = "�����11", Rating = 3.1,
                    PublishingHouses = new List<PublishingHouse> {juventus},
                    Genres = new List<Genre> {drama}, Authors = new List<Author> {author1, author2},
                    Description = "��������", Language = english, PageCount = 457,
                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
                },
                new Book
                {
                    Id = Guid.NewGuid(), Name = "�����12", Rating = 4.9,
                    PublishingHouses = new List<PublishingHouse> {juventus},
                    Genres = new List<Genre> {drama}, Authors = new List<Author> {author1},
                    Description = "��������", Language = russian, PageCount = 346,
                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
                },
                new Book
                {
                    Id = Guid.NewGuid(), Name = "�����13", Rating = 4.4,
                    PublishingHouses = new List<PublishingHouse> {juventus},
                    Genres = new List<Genre> {horror, fantastic}, Authors = new List<Author> {author2},
                    Description = "��������", Language = english, PageCount = 235,
                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
                },
                new Book
                {
                    Id = Guid.NewGuid(), Name = "�����14", Rating = 3.9,
                    PublishingHouses = new List<PublishingHouse> {juventus},
                    Genres = new List<Genre> {horror, drama}, Authors = new List<Author> {author1},
                    Description = "��������", Language = russian, PageCount = 150,
                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
                },
                new Book
                {
                    Id = Guid.NewGuid(), Name = "�����15", Rating = 4.5,
                    PublishingHouses = new List<PublishingHouse> {juventus},
                    Genres = new List<Genre> {drama, fantastic}, Authors = new List<Author> {author1, author2, author3},
                    Description = "��������", Language = english, PageCount = 100,
                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
                },
            }.AsQueryable();
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<Book, BookPreviewDto>()
                    .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors.Select(x => x.Name)))
                    .ForMember(dest => dest.PublishingHouses, opt => opt.MapFrom(src => src.PublishingHouses.Select(x => x.Name)))
                    .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(x => x.Name)))
            );
            IMapper mapper = new Mapper(config);
            BookPreviewDtos = books.ProjectTo<BookPreviewDto>(mapper.ConfigurationProvider).ToList();
            var booksMock = CreateDbSetMock(books.AsQueryable());
            var options = new DbContextOptions<ApplicationDbContext>();
            var applicationDbContextMock = new Mock<ApplicationDbContext>(options);
            applicationDbContextMock.Setup(x => x.Set<Book>()).Returns(booksMock.Object);
            var unitOfWork = new UnitOfWork(applicationDbContextMock.Object);
            _booksCatalogService = new BooksCatalogService(unitOfWork, mapper);
        }

        [Test]
        public async Task Can_Get_Top_Books()
        {
            // Act
            var result = _booksCatalogService.GetTopBooks();

            // Assert
            var resultBooks = result.ToList();
            Assert.IsTrue(resultBooks.Count <= 10);
            Assert.AreEqual(BookPreviewDtos.Where(x => x.Rating != null).OrderBy(x => x.Rating).Take(10).Select(x => x.Name), resultBooks.Select(x => x.Name));
        }
    }
}