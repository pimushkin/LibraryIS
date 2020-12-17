using System;
using LibraryIS.SharedKernel;

namespace LibraryIS.Core.Entities
{
    public class TakenBook : BaseEntity<Guid>
    {
        public Book Book { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime ReceiveDate { get; set; }
    }
}
