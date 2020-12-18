using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryIS.Application.DTOs;
using LibraryIS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryIS.Api.Controllers
{
    public class BooksCatalogController : ApiControllerBase
    {
        private readonly IBooksCatalogService _booksCatalogService;

        public BooksCatalogController(IBooksCatalogService booksCatalogService)
        {
            _booksCatalogService = booksCatalogService;
        }

        /// <summary>
        /// Get the top 10 books with the highest ratings.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/top-books")]
        public ActionResult<IEnumerable<BookPreviewDto>> GetTopBooks()
        {
            return Ok(_booksCatalogService.GetTopBooks());
        }

        /// <summary>
        /// Get books for a specific page.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("/recent-books")]
        public ActionResult<IEnumerable<BookPreviewDto>> GetRecentBooks(int page, int pageSize)
        {
            return Ok(_booksCatalogService.GetRecentBooks(page, pageSize));
        }

        /// <summary>
        /// Find the book that contains the title you entered.
        /// </summary>
        /// <param name="bookTitle"></param>
        /// <returns></returns>
        [HttpPost("/search-by-title")]
        public ActionResult GetBooksByTitle([FromBody] BookTitleDto bookTitle)
        {
            return Ok(_booksCatalogService.SearchByTitle(bookTitle.Title));
        }

        /// <summary>
        /// Find books by filter.
        /// </summary>
        /// <param name="bookSearchFilter"></param>
        /// <returns></returns>
        [HttpPost("/search-by-filter")]
        public ActionResult GetBooksByFilter([FromBody] BookSearchFilterDto bookSearchFilter)
        {
            return Ok(_booksCatalogService.SearchByFilter(bookSearchFilter));
        }
    }
}
