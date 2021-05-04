using LibraryIS.Application.DTOs;
using LibraryIS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryIS.Application.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        public Task<IReadOnlyList<BookPreviewDto>> GetBooksForSelectedPage(int page, int pageSize);
        public Task<IReadOnlyList<BookPreviewDto>> GetBooksWithHighestRating();
        public Task<IReadOnlyList<BookPreviewDto>> SearchBooksByFilter(BookSearchFilterDto dto);
    }
}
