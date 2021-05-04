using LibraryIS.Domain.Common;
using System;

namespace LibraryIS.Domain.Entities
{
    public class TakenBook : Entity
    {
        public virtual ReaderProfile Reader { get; }
        public virtual Book Book { get; }
        public DateTime? ReturnDate { get; }
        public DateTime ReceiveDate { get; }

        protected TakenBook()
        {
        }
    }
}