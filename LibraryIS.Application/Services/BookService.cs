using LibraryIS.Application.DTOs;
using LibraryIS.Application.Interfaces;
using LibraryIS.CrossCutting.Exceptions;
using LibraryIS.Domain.Entities;
using LibraryIS.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LibraryIS.Application.Repositories;

namespace LibraryIS.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;

        public BookService(IBookRepository bookRepository, IHttpContextAccessor httpContextAccessor, ILogger logger)
        {
            _bookRepository = bookRepository;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task SubmitBookRating(EvaluationDto evaluation)
        {
            if (!_httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated)
            {
                _logger.Fatal($"Method {MethodBase.GetCurrentMethod()!.Name} was called without checking the user's authorization.");
                throw new ForbiddenAccessException();
            }

            var book = await _bookRepository.GetByIdAsync(evaluation.BookId);
            if (book == null)
            {
                throw new NotFoundException($"The book with id {evaluation.BookId} was not found.");
            }

            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var profiles = await _unitOfWork.GetRepository<ReaderProfile>()
                .FilterAsync(x => x.User.Email == userName);
            var profile = profiles.First();

            if (profile.TakenBooks == null || profile.TakenBooks.Count == 0 || !profile.TakenBooks.Select(x => x.Book.Id).Contains(book.Id))
            {
                throw new Exception("There is no book among the books taken.");
            }

            if (profile.Evaluations != null && profile.Evaluations.Select(x => x.Book.Id).Contains(evaluation.BookId))
            {
                throw new Exception("The reader has already rated the book.");
            }

            profile.AddEvaluationToBook(new Evaluation(evaluation.Rating, book, profile));
            _unitOfWork.GetRepository<ReaderProfile>().Update(profile);
            await _unitOfWork.Commit();
        }

        public Task GetBookInformation(Guid bookId)
        {
            throw new NotImplementedException();
        }

        public Task ReserveBook(Guid bookId)
        {
            throw new NotImplementedException();
        }

        public Task OrderBookFragment(ElectronicCopyRequestDto electronicCopyRequest)
        {
            throw new NotImplementedException();
        }
    }
}
