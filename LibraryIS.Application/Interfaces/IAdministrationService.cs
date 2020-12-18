using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryIS.Application.DTOs;
using LibraryIS.Core.Entities;
using LibraryIS.Core.Enums;

namespace LibraryIS.Application.Interfaces
{
    public interface IAdministrationService
    {
        public IEnumerable<ElectronicCopyRequestDto> GetBooksCopiesRequests();
        public IEnumerable<TakenBook> GetBooksDebtInformation(Guid profileId);
        public IEnumerable<Book> SearchBookFromAPI(BookSearchFilterDto bookSearchFilter);
        public IEnumerable<Book> SearchBookFromAPI(string query);
        public Book AddBookInformation(Book book);
        public IEnumerable<User> SearchUsers(string name);
        public void CreateUser(Guid id);
        public void DeleteUser(Guid id);
        public void EditUser(User user);
        public void ChangeBlock(Guid id, bool block);
        public void AddTakenBook(Guid profileId, Guid bookId);
        public void AddReturnedBook(Guid profileId, Guid bookId);
        public void ResolveCopyRequest(Guid requestId, RequestStatus requestStatus);
        public IEnumerable<ReservedBook> GetReservedBooks(Guid profileId);
        public void CancelBookReservation(Guid requestId);
    }
}
