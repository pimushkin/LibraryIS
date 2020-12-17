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

        [HttpGet("/top-books")]
        public ActionResult<IEnumerable<BookPreviewDto>> GetTopBooks()
        {
            return Ok(_booksCatalogService.GetTopBooks());
        }

        [HttpGet("/recent-books")]
        public ActionResult<IEnumerable<BookPreviewDto>> GetRecentBooks(int page, int pageSize)
        {
            return Ok(_booksCatalogService.GetRecentBooks(page, pageSize));
        }

        [HttpPost("/search-by-title")]
        public ActionResult GetBooksByTitle([FromBody] BookTitleDto bookTitle)
        {
            return Ok(_booksCatalogService.SearchByTitle(bookTitle.Title));
        }

        [HttpPost("/search-by-filter")]
        public ActionResult GetBooksByFilter([FromBody] BookSearchFilterDto bookSearchFilter)
        {
            return Ok(_booksCatalogService.SearchByFilter(bookSearchFilter));
        }
    }
}
