using System;
using AutoMapper;
using LibraryIS.Application.DTOs;
using LibraryIS.Application.Repositories;
using LibraryIS.Domain.Entities;
using LibraryIS.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using LibraryIS.CrossCutting.Exceptions;

namespace LibraryIS.Persistence.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        private readonly IMapper _mapper;
        public BookRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<BookPreviewDto>> GetBooksForSelectedPage(int page, int pageSize)
        {
            return await Context.Set<Book>().Skip((page - 1) * pageSize).Take(pageSize)
                .ProjectTo<BookPreviewDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IReadOnlyList<BookPreviewDto>> GetBooksWithHighestRating()
        {
            return await Context.Set<Evaluation>()
                .GroupBy(evaluation => evaluation.Book.Id,
                    (guid, enumerable) => new {guid, Average = enumerable.Average(evaluation => evaluation.Rating)})
                .OrderBy(arg => arg.Average).Take(10)
                .Join(Context.Set<Book>(), arg => arg.guid, book => book.Id,
                    (arg, book) => _mapper.Map<BookPreviewDto>(book)).ToListAsync();
        }

        public async Task<IReadOnlyList<BookPreviewDto>> SearchBooksByFilter(BookSearchFilterDto dto)
        {
            var bookRepository = Context.Set<Book>().AsQueryable();
            if (dto.Authors != null)
            {
                bookRepository = bookRepository.Where(book =>
                    book.Authors.Select(author => author.Name)
                        .All(x => dto.Authors != null && dto.Authors.Contains(x)));
            }

            if (dto.BeginningPublicationDate != DateTime.MinValue)
            {
                bookRepository = bookRepository.Where(book =>
                    dto.BeginningPublicationDate <= book.PublicationDate);
            }

            if (dto.EndPublicationDate != DateTime.MinValue)
            {
                bookRepository = bookRepository.Where(book =>
                    dto.EndPublicationDate >= book.PublicationDate);
            }

            if (dto.Genre != null)
            {
                bookRepository = bookRepository.Where(book => book.Genres.Select(x => x.Name).Contains(dto.Genre));
            }

            if (dto.Language != null)
            {
                bookRepository = bookRepository.Where(book => book.BookLanguage.Name.Contains(dto.Language));
            }

            if (dto.Title != null)
            {
                bookRepository = bookRepository.Where(book => EF.Functions.FreeText(book.Name, dto.Title));
            }

            var resultBooks = bookRepository.ToList();
            var booksPreviews = _mapper.Map<List<BookPreviewDto>>(resultBooks);

            return booksPreviews;
        }
    }
}