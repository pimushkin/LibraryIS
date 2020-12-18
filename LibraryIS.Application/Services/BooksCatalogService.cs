using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<BookPreviewDto> GetRecentBooks(int page, int pageSize)
        {
            var books = _unitOfWork.GetRepository<Book>().Query().Skip((page - 1) * pageSize).Take(pageSize)
                .ProjectTo<BookPreviewDto>(_mapper.ConfigurationProvider).ToList();
            if (books == null || books.Count == 0)
            {
                throw new NotFoundException("No books were found for the specified page.");
            }
            var orderRatings = _evaluationService.GetBooksRatings();
            foreach (var (bookId, rating) in orderRatings)
            {
                books.Single(x => x.Id == bookId).Rating = rating;
            }

            return books!;
        }

        public IEnumerable<BookPreviewDto>? GetTopBooks()
        {
            var orderRatings = new Dictionary<Guid, double>(_evaluationService.GetBooksRatings()
                .OrderByDescending(x => x.Value).Take(10).ToList());
            var books = _unitOfWork.GetRepository<Book>()
                .FindBy(x => orderRatings.Select(valuePair => valuePair.Key).Contains(x.Id))
                .ProjectTo<BookPreviewDto>(_mapper.ConfigurationProvider)
                .Take(10).ToList();
            if (books.Count == 0)
            {   
                _logger.Warning("No books were found with ratings for getting the top books.");
                return null;
            }

            foreach (var (bookId, rating) in orderRatings)
            {
                books.Single(x => x.Id == bookId).Rating = rating;
            }

            return books;
        }

        public IEnumerable<BookPreviewDto>? SearchByTitle(string query)
        {
            var books = _unitOfWork.GetRepository<Book>().FindBy(x => x.Name.Contains(query))
                .ProjectTo<BookPreviewDto>(_mapper.ConfigurationProvider).ToList();
            if (books.Count != 0)
            {
                var orderRatings = _evaluationService.GetBooksRatings();
                foreach (var (bookId, rating) in orderRatings)
                {
                    books.Single(x => x.Id == bookId).Rating = rating;
                }
                return books;
            }
            _logger.Warning($"Books containing {query} in their title were not found.");
            return null;

        }

        public IEnumerable<BookPreviewDto>? SearchByFilter(BookSearchFilterDto filter)
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

            var resultBooks = books.ProjectTo<BookPreviewDto>(_mapper.ConfigurationProvider).ToList();
            
            if (resultBooks.Count != 0)
            {
                var orderRatings = _evaluationService.GetBooksRatings();
                foreach (var (bookId, rating) in orderRatings)
                {
                    resultBooks.Single(x => x.Id == bookId).Rating = rating;
                }
                return resultBooks;
            }
            _logger.Warning("No matches were found in the filter search.");
            return null;

        }
    }
}