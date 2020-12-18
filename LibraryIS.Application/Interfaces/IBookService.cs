using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryIS.Application.DTOs;

namespace LibraryIS.Application.Interfaces
{
    public interface IBookService
    {
        public Task SubmitBookRating(EvaluationDto evaluation);
        public Task GetBookInformation(Guid bookId);
        public Task ReserveBook(Guid bookId);
        public Task OrderBookFragment(ElectronicCopyRequestDto electronicCopyRequest);

    }
}
