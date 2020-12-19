using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryIS.Application.DTOs;
using LibraryIS.Core.Entities;

namespace LibraryIS.Application.Interfaces
{
    public interface IBooksCatalogService
    {
        public Task<IEnumerable<BookPreviewDto>> GetRecentBooksAsync(int page, int pageSize);
        public Task<IEnumerable<BookPreviewDto>> GetTopBooksAsync();
        public Task<IEnumerable<BookPreviewDto>> SearchByTitleAsync(string query);
        public Task<IEnumerable<BookPreviewDto>> SearchByFilterAsync(BookSearchFilterDto filter);
    }
}
