//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using AutoMapper.QueryableExtensions;
//using LibraryIS.Application.DTOs;
//using LibraryIS.Application.Interfaces;
//using LibraryIS.Application.Services;
//using LibraryIS.Domain.Entities;
//using LibraryIS.Persistence;
//using Microsoft.EntityFrameworkCore;
//using MockQueryable.Moq;
//using Moq;
//using NUnit.Framework;
//using Serilog;
//using Serilog.Core;

//namespace LibraryIS.UnitTests
//{
//    public class Tests
//    {
//        private IBooksCatalogService _booksCatalogService;
//        private IEvaluationService _evaluationService;
//        private static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
//        {
//            var elementsAsQueryable = elements.AsQueryable();
//            var dbSetMock = new Mock<DbSet<T>>();

//            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
//            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
//            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
//            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

//            return dbSetMock;
//        }
//        private List<PublishingHouse> PublishingHouses { get; set; }
//        private List<Genre> Genres { get; set; }
//        private List<Language> Languages { get; set; }
//        private List<Author> Authors { get; set; }
//        private List<BookPreviewDto> BookPreviewDtos { get; set; }

//        [SetUp]
//        public void Setup()
//        {
//            var phoenix = new PublishingHouse
//            {
//                Id = Guid.NewGuid(),
//                Name = "Феникс"
//            };
//            var flamingos = new PublishingHouse
//            {
//                Id = Guid.NewGuid(),
//                Name = "Фламинго"
//            };
//            var juventus = new PublishingHouse
//            {
//                Id = Guid.NewGuid(),
//                Name = "Ювента"
//            };
//            PublishingHouses = new List<PublishingHouse> {phoenix, juventus, flamingos};

//            var fantastic = new Genre
//            {
//                Id = Guid.NewGuid(),
//                Name = "Фантастика"
//            };
//            var drama = new Genre
//            {
//                Id = Guid.NewGuid(),
//                Name = "Драма"
//            };
//            var horror = new Genre
//            {
//                Id = Guid.NewGuid(),
//                Name = "Ужасы"
//            };
//            Genres = new List<Genre> {fantastic, drama, horror};

//            var author1 = new Author
//            {
//                Id = Guid.NewGuid(),
//                Name = "Пушкин"
//            };
//            var author2 = new Author
//            {
//                Id = Guid.NewGuid(),
//                Name = "Толстой"
//            };
//            var author3 = new Author
//            {
//                Id = Guid.NewGuid(),
//                Name = "Лермпантов"
//            };
//            Authors = new List<Author> {author1, author2, author3};

//            var russian = new Language
//            {
//                Id = Guid.NewGuid(),
//                Name = "Русский"
//            };
//            var english = new Language
//            {
//                Id = Guid.NewGuid(),
//                Name = "Английский"
//            };
//            Languages = new List<Language> {russian, english};
//            var book = new Book
//            {
//                Id = Guid.NewGuid(),
//                Name = "Книга1",
//                PublishingHouses = new List<PublishingHouse> {juventus},
//                Genres = new List<Genre> {fantastic},
//                Authors = new List<Author> {author1, author2},
//                Description = "Описание",
//                Language = russian,
//                PageCount = 438,
//                PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
//            };
//            var books = new List<Book>
//            {
//                book,
//                new Book
//                {
//                    Id = Guid.NewGuid(), Name = "Книга2",
//                    PublishingHouses = new List<PublishingHouse> {juventus},
//                    Genres = new List<Genre> { horror, fantastic }, Authors = new List<Author> {author2, author1},
//                    Description = "Описание", Language = english, PageCount = 624,
//                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
//                },
//                new Book
//                {
//                    Id = Guid.NewGuid(), Name = "Книга3",
//                    PublishingHouses = new List<PublishingHouse> {juventus},
//                    Genres = new List<Genre> {horror}, Authors = new List<Author> { author3 },
//                    Description = "Описание", Language = russian, PageCount = 528,
//                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
//                },
//                new Book
//                {
//                    Id = Guid.NewGuid(), Name = "Книга4",
//                    PublishingHouses = new List<PublishingHouse> {juventus},
//                    Genres = new List<Genre> {horror, fantastic, drama}, Authors = new List<Author> { author1 },
//                    Description = "Описание", Language = english, PageCount = 457,
//                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
//                },
//                new Book
//                {
//                    Id = Guid.NewGuid(), Name = "Книга5",
//                    PublishingHouses = new List<PublishingHouse> {juventus},
//                    Genres = new List<Genre> {drama, fantastic}, Authors = new List<Author> { author1, author3 },
//                    Description = "Описание", Language = russian, PageCount = 125,
//                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
//                },
//                new Book
//                {
//                    Id = Guid.NewGuid(), Name = "Книга6",
//                    PublishingHouses = new List<PublishingHouse> {juventus},
//                    Genres = new List<Genre> {drama}, Authors = new List<Author> {author1},
//                    Description = "Описание", Language = english, PageCount = 724,
//                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
//                },
//                new Book
//                {
//                    Id = Guid.NewGuid(), Name = "Книга7",
//                    PublishingHouses = new List<PublishingHouse> {juventus},
//                    Genres = new List<Genre> {drama, fantastic}, Authors = new List<Author> {author2},
//                    Description = "Описание", Language = english, PageCount = 457,
//                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
//                },
//                new Book
//                {
//                    Id = Guid.NewGuid(), Name = "Книга8",
//                    PublishingHouses = new List<PublishingHouse> {juventus},
//                    Genres = new List<Genre> {drama, fantastic}, Authors = new List<Author> {author3},
//                    Description = "Описание", Language = russian, PageCount = 765,
//                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
//                },
//                new Book
//                {
//                    Id = Guid.NewGuid(), Name = "Книга9",
//                    PublishingHouses = new List<PublishingHouse> {juventus},
//                    Genres = new List<Genre> {horror}, Authors = new List<Author> {author1, author2, author3},
//                    Description = "Описание", Language = english, PageCount = 346,
//                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
//                },
//                new Book
//                {
//                    Id = Guid.NewGuid(), Name = "Книга10",
//                    PublishingHouses = new List<PublishingHouse> {juventus},
//                    Genres = new List<Genre> {fantastic}, Authors = new List<Author> {author2, author3},
//                    Description = "Описание", Language = russian, PageCount = 124,
//                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
//                },
//                new Book
//                {
//                    Id = Guid.NewGuid(), Name = "Книга11",
//                    PublishingHouses = new List<PublishingHouse> {juventus},
//                    Genres = new List<Genre> {drama}, Authors = new List<Author> {author1, author2},
//                    Description = "Описание", Language = english, PageCount = 457,
//                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
//                },
//                new Book
//                {
//                    Id = Guid.NewGuid(), Name = "Книга12",
//                    PublishingHouses = new List<PublishingHouse> {juventus},
//                    Genres = new List<Genre> {drama}, Authors = new List<Author> {author1},
//                    Description = "Описание", Language = russian, PageCount = 346,
//                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
//                },
//                new Book
//                {
//                    Id = Guid.NewGuid(), Name = "Книга13",
//                    PublishingHouses = new List<PublishingHouse> {juventus},
//                    Genres = new List<Genre> {horror, fantastic}, Authors = new List<Author> {author2},
//                    Description = "Описание", Language = english, PageCount = 235,
//                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
//                },
//                new Book
//                {
//                    Id = Guid.NewGuid(), Name = "Книга14",
//                    PublishingHouses = new List<PublishingHouse> {juventus},
//                    Genres = new List<Genre> {horror, drama}, Authors = new List<Author> {author1},
//                    Description = "Описание", Language = russian, PageCount = 150,
//                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
//                },
//                new Book
//                {
//                    Id = Guid.NewGuid(), Name = "Книга15",
//                    PublishingHouses = new List<PublishingHouse> {juventus},
//                    Genres = new List<Genre> {drama, fantastic}, Authors = new List<Author> {author1, author2, author3},
//                    Description = "Описание", Language = english, PageCount = 100,
//                    PublicationDate = DateTime.Now.AddDays(new Random().Next(0, 100))
//                },
//            }.AsQueryable();
//            var evaluationsMock = new List<Evaluation>().AsQueryable().BuildMockDbSet();
//            var config = new MapperConfiguration(cfg =>
//                cfg.CreateMap<Book, BookPreviewDto>()
//                    .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors.Select(x => x.Name)))
//                    .ForMember(dest => dest.PublishingHouses, opt => opt.MapFrom(src => src.PublishingHouses.Select(x => x.Name)))
//                    .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(x => x.Name)))
//            );
//            IMapper mapper = new Mapper(config);
//            BookPreviewDtos = books.ProjectTo<BookPreviewDto>(mapper.ConfigurationProvider).ToList();
//            var booksMock = books.BuildMockDbSet();
//            var options = new DbContextOptions<ApplicationDbContext>();
//            var applicationDbContextMock = new Mock<ApplicationDbContext>(options);
//            applicationDbContextMock.Setup(x => x.Set<Book>()).Returns(booksMock.Object);
//            applicationDbContextMock.Setup(x => x.Set<Evaluation>()).Returns(evaluationsMock.Object);
//            var unitOfWork = new UnitOfWork(applicationDbContextMock.Object);
//            var log = new LoggerConfiguration()
//                .WriteTo.Console()
//                .CreateLogger();
//            _evaluationService = new EvaluationService(unitOfWork);
//            _booksCatalogService = new BooksCatalogService(unitOfWork, mapper, log, _evaluationService );
//        }

//        [Test]
//        public async Task Can_Paginate_Books()
//        {
//            // Act
//            var result = await _booksCatalogService.GetRecentBooksAsync(1, 5);

//            // Assert
//            var resultBooks = result.ToList();
//            Assert.IsTrue(resultBooks.Count <= 5);
//            Assert.AreEqual(BookPreviewDtos.Where(x => x.Rating != null).OrderBy(x => x.Rating).Take(1).Select(x => x.Name), resultBooks.Where(x => x.Rating != null).Select(x => x.Name));
//        }
//    }
//}