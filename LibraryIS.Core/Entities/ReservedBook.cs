using System;
using LibraryIS.SharedKernel;

namespace LibraryIS.Core.Entities
{
    public class ReservedBook : BaseEntity<Guid>
    {
        public ReaderProfile ReaderProfile { get; set; }
        public Book Book { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
