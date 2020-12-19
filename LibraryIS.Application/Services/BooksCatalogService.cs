using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryIS.Application.DTOs;
using LibraryIS.Application.Interfaces;
using LibraryIS.Core.Entities;
using LibraryIS.Core.Interfaces;
using LibraryIS.CrossCutting.Exceptions;
using Serilog;

namespace LibraryIS.Application.Services
{
    public class BooksCatalogService : IBooksCatalogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IEvaluationService _evaluationService;

        public BooksCatalogService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IEvaluationService evaluationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _evaluationService = evaluationService;
        }

        public async Task<IEnumerable<BookPreviewDto>> GetRecentBooksAsync(int page, int pageSize)
        {
            var books = await _unitOfWork.GetRepository<Book>().FilterAsync(includeProperties: "Authors,PublishingHouses,Genres", page: page, pageSize: pageSize);
            var booksList = books.ToList();
            var booksPreviews = _mapper.Map<List<Book>, List<BookPreviewDto>>(booksList);
            if (booksPreviews == null || booksPreviews.Count == 0)
            {
                const string message = "No books were found for the specified page.";
                _logger.Error(message);
                throw new NotFoundException(message);
            }
            var orderRatings = await _evaluationService.GetBooksRatingsAsync();
            foreach (var (bookId, rating) in orderRatings)
            {
                booksPreviews.Single(x => x.Id == bookId).Rating = rating;
            }

            return booksPreviews!;
        }

        public async Task<IEnumerable<BookPreviewDto>> GetTopBooksAsync()
        {
            var ratings = new Dictionary<Guid, double>(await _evaluationService.GetBooksRatingsAsync());
            var orderedRatings = ratings.OrderByDescending(x => x.Value).Take(10);
            var books = await _unitOfWork.GetRepository<Book>()
                .FindAllAsync(x => orderedRatings.Select(valuePair => valuePair.Key).Contains(x.Id));
            var topBooks = books.Take(10).ToList();
            var topBooksPreviews = _mapper.Map<List<Book>, List<BookPreviewDto>>(topBooks);
            if (topBooksPreviews.Count == 0)
            {
                const string message = "No books were found with ratings for getting the top books.";
                _logger.Warning(message);
                throw new NotFoundException(message);
            }

            foreach (var (bookId, rating) in orderedRatings)
            {
                topBooksPreviews.Single(x => x.Id == bookId).Rating = rating;
            }

            return topBooksPreviews;
        }

        public async Task<IEnumerable<BookPreviewDto>> SearchByTitleAsync(string query)
        {
            var books = await _unitOfWork.GetRepository<Book>().FindAllAsync(x => x.Name.Contains(query));
            var booksList = books.ToList();
            var booksPreviews = _mapper.Map<List<Book>, List<BookPreviewDto>>(booksList);
            if (books.Count != 0)
            {
                var orderRatings = await _evaluationService.GetBooksRatingsAsync();
                foreach (var (bookId, rating) in orderRatings)
                {
                    booksPreviews.Single(x => x.Id == bookId).Rating = rating;
                }
                return booksPreviews;
            }
            var message = $"Books containing {query} in their title were not found.";
            _logger.Warning(message);
            throw new NotFoundException(message);

        }

        public async Task<IEnumerable<BookPreviewDto>> SearchByFilterAsync(BookSearchFilterDto filter)
        {
            var bookRepository = _unitOfWork.GetRepository<Book>();
            IQueryable<Book> books = bookRepository.Query();
            if (filter.Authors != null)
            {
                books = bookRepository.FindBy(book =>
                    book.Authors.Select(author => author.Name)
                        .All(x => filter.Authors != null && filter.Authors.Contains(x)));
            }

            if (filter.BeginningPublicationDate != null)
            {
                books = bookRepository.FindBy(book =>
                    filter.BeginningPublicationDate <= book.PublicationDate);
            }

            if (filter.EndPublicationDate != null)
            {
                books = bookRepository.FindBy(book =>
                    filter.EndPublicationDate >= book.PublicationDate);
            }

            if (filter.Genre != null)
            {
                books = bookRepository.FindBy(book => book.Genres.Select(x => x.Name).Contains(filter.Genre));
            }

            if (filter.Language != null)
            {
                books = bookRepository.FindBy(book => book.Language.Name.Contains(filter.Language));
            }

            if (filter.Title != null)
            {
                books = bookRepository.FindBy(book => book.Name.Contains(filter.Title));
            }

            var resultBooks = books.ToList();
            var booksPreviews = _mapper.Map<List<Book>, List<BookPreviewDto>>(resultBooks);
            
            if (booksPreviews.Count != 0)
            {
                var orderRatings = await _evaluationService.GetBooksRatingsAsync();
                foreach (var (bookId, rating) in orderRatings)
                {
                    booksPreviews.Single(x => x.Id == bookId).Rating = rating;
                }
                return booksPreviews;
            }

            var message = "No matches were found in the filter search.";
            _logger.Warning(message);
            throw new NotFoundException(message);
        }
    }
}