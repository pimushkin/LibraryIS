using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryIS.Application.DTOs;
using LibraryIS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryIS.Api.Controllers
{
    [Authorize]
    public class BookController : ApiControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Add a rating to the book that the reader took.
        /// </summary>
        /// <param name="evaluation"></param>
        /// <returns></returns>
        [HttpPost("/rate-book")]
        public async Task<ActionResult> AddRatingToBook(EvaluationDto evaluation)
        {
            await _bookService.SubmitBookRating(evaluation);
            return Ok();
        }
    }
}
