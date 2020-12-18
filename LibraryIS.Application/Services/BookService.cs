using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryIS.Application.DTOs;
using LibraryIS.Application.Interfaces;
using LibraryIS.Core.Entities;
using LibraryIS.Core.Interfaces;
using LibraryIS.CrossCutting.Exceptions;
using Microsoft.AspNetCore.Http;

namespace LibraryIS.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SubmitBookRating(EvaluationDto evaluation)
        {
            if (_httpContextAccessor.HttpContext?.User.Identity?.Name == null)
            {
                throw new ForbiddenAccessException();
            }
            var userName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
            var profile = _unitOfWork.GetRepository<ReaderProfile>().Filter(x => x.User.Email == userName, includeProperties: "TakenBooks,Evaluations").First();

            var book = await _unitOfWork.GetRepository<Book>().GetByUniqueIdAsync(evaluation.BookId);
            if (book == null)
            {
                throw new NotFoundException($"The book with id {evaluation.BookId} was not found.");
            }

            if (profile.TakenBooks == null || profile.TakenBooks.Count == 0 || !profile.TakenBooks.Select(x => x.Book.Id).Contains(book.Id))
            {
                throw new Exception("There is no book among the books taken.");
            }

            profile.Evaluations ??= new List<Evaluation>();
            profile.Evaluations.Add(new Evaluation
            {
                Book = book,
                Profile = profile,
                Rating = evaluation.Rating,
            });
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
