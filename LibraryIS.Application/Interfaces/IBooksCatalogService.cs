using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryIS.Application.DTOs;
using LibraryIS.Core.Entities;

namespace LibraryIS.Application.Interfaces
{
    public interface IBooksCatalogService
    {
        public IEnumerable<BookPreviewDto> GetRecentBooks(int page, int pageSize);
        public IEnumerable<BookPreviewDto>? GetTopBooks();
        public IEnumerable<BookPreviewDto>? SearchByTitle(string query);
        public IEnumerable<BookPreviewDto>? SearchByFilter(BookSearchFilterDto filter);
    }
}
