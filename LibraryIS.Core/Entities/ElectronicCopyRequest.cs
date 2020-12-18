using System;
using LibraryIS.Core.Enums;
using LibraryIS.SharedKernel;

namespace LibraryIS.Core.Entities
{
    public class ElectronicCopyRequest : BaseEntity<Guid>
    {
        public ReaderProfile ReaderProfile { get; set; }
        public Book Book { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public string PagesRange { get; set; }
    }
}
