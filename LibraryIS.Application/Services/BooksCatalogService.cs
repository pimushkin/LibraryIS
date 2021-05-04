using AutoMapper;
using LibraryIS.Application.DTOs;
using LibraryIS.Application.Interfaces;
using LibraryIS.CrossCutting.Exceptions;
using LibraryIS.Domain.Entities;
using LibraryIS.Domain.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryIS.Application.Repositories;

namespace LibraryIS.Application.Services
{
    public class BooksCatalogService : IBooksCatalogService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public BooksCatalogService(IBookRepository bookRepository, IMapper mapper, ILogger logger)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<BookPreviewDto>> GetRecentBooksAsync(int page, int pageSize)
        {
            var books = await _bookRepository.GetBooksForSelectedPage(page, pageSize);
            if (books == null || books.Count == 0)
            {
                const string message = "No books were found for the specified page.";
                _logger.Error(message);
                throw new NotFoundException(message);
            }

            return books!;
        }

        public async Task<IEnumerable<BookPreviewDto>> GetTopBooksAsync()
        {
            var books = await _bookRepository.GetBooksWithHighestRating();
            if (books.Count == 0)
            {
                const string message = "No books were found with ratings for getting the top books.";
                _logger.Warning(message);
                throw new NotFoundException(message);
            }

            return books;
        }

        public async Task<IEnumerable<BookPreviewDto>> SearchByTitleAsync(string query)
        {
            //var books = await _unitOfWork.GetRepository<Book>().FindAllAsync(x => x.Name.Contains(query));
            //var booksList = books.ToList();
            //var booksPreviews = _mapper.Map<List<Book>, List<BookPreviewDto>>(booksList);
            //if (books.Count != 0)
            //{
            //    return booksPreviews;
            //}
            //var message = $"Books containing {query} in their title were not found.";
            //_logger.Warning(message);
            throw new NotFoundException();

        }

        public async Task<IEnumerable<BookPreviewDto>> SearchByFilterAsync(BookSearchFilterDto filter)
        {
            var books = await _bookRepository.SearchBooksByFilter(filter);
            if (books.Count > 0)
            {
                return books;
            }

            var message = "No matches were found in the filter search.";
            _logger.Warning(message);
            throw new NotFoundException(message);
        }
    }
}