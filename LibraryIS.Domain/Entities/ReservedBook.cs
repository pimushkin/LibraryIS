using LibraryIS.Domain.Common;
using System;

namespace LibraryIS.Domain.Entities
{
    public class ReservedBook : Entity
    {
        public virtual ReaderProfile Reader { get; }
        public virtual Book BookedBook { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        protected ReservedBook()
        {
        }
    }
}
