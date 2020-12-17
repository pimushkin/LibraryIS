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

        public BooksCatalogService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<BookPreviewDto> GetRecentBooks(int page, int pageSize)
        {
            var books = _unitOfWork.GetRepository<Book>().Query().Skip((page - 1) * pageSize).Take(pageSize)
                .ProjectTo<BookPreviewDto>(_mapper.ConfigurationProvider).ToList();
            if (books == null || books.Count == 0)
            {
                throw new NotFoundException("No books were found for the specified page.");
            }
            return books!;
        }

        public IEnumerable<BookPreviewDto>? GetTopBooks()
        {
            var books = _unitOfWork.GetRepository<Book>().FindBy(x => x.Rating != null).OrderByDescending(x => x.Rating)
                .ProjectTo<BookPreviewDto>(_mapper.ConfigurationProvider)
                .Take(10).ToList();
            if (books.Count == 0)
            {
                _logger.Warning("No books were found with ratings for getting the top books.");
                return null;
            }

            return books;
        }

        public IEnumerable<BookPreviewDto>? SearchByTitle(string query)
        {
            var books = _unitOfWork.GetRepository<Book>().FindBy(x => x.Name.Contains(query))
                .ProjectTo<BookPreviewDto>(_mapper.ConfigurationProvider).ToList();
            if (books.Count == 0)
            {
                _logger.Warning($"Books containing {query} in their title were not found.");
                return null;
            }

            return books;
        }

        public IEnumerable<BookPreviewDto>? SearchByFilter(BookSearchFilterDto filter)
        {
            var books = _unitOfWork.GetRepository<Book>().FindBy(book =>
                filter.Rating >= book.Rating &&
                book.Authors.Select(author => author.Name)
                    .All(x => filter.Authors != null && filter.Authors.Contains(x)) &&
                filter.Language == book.Language.Name && filter.BeginningPublicationDate >= book.PublicationDate &&
                filter.EndPublicationDate <= book.PublicationDate &&
                filter.Title == book.Name &&
                book.Genres.Select(genre => genre.Name).Contains(filter.Genre))
                .ProjectTo<BookPreviewDto>(_mapper.ConfigurationProvider).ToList(); ;
            if (books.Count == 0)
            {
                _logger.Warning("No matches were found in the filter search.");
                return null;
            }

            return books;
        }
    }
}