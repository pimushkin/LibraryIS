using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryIS.Application.DTOs;
using LibraryIS.Application.Interfaces;
using LibraryIS.Core.Entities;
using LibraryIS.Core.Enums;

namespace LibraryIS.Application.Services
{
    public class AdministrationService : IAdministrationService
    {
        public IEnumerable<ElectronicCopyRequestDto> GetBooksCopiesRequests()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TakenBook> GetBooksDebtInformation(Guid profileId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> SearchBookFromAPI(BookSearchFilterDto bookSearchFilter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> SearchBookFromAPI(string query)
        {
            throw new NotImplementedException();
        }

        public Book AddBookInformation(Book book)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> SearchUsers(string name)
        {
            throw new NotImplementedException();
        }

        public void CreateUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public void EditUser(User user)
        {
            throw new NotImplementedException();
        }

        public void ChangeBlock(Guid id, bool block)
        {
            throw new NotImplementedException();
        }

        public void AddTakenBook(Guid profileId, Guid bookId)
        {
            throw new NotImplementedException();
        }

        public void AddReturnedBook(Guid profileId, Guid bookId)
        {
            throw new NotImplementedException();
        }

        public void ResolveCopyRequest(Guid requestId, RequestStatus requestStatus)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReservedBook> GetReservedBooks(Guid profileId)
        {
            throw new NotImplementedException();
        }

        public void CancelBookReservation(Guid requestId)
        {
            throw new NotImplementedException();
        }
    }
}
