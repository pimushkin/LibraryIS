using System;
using LibraryIS.Core.Enums;
using LibraryIS.SharedKernel;

namespace LibraryIS.Core.Entities
{
    public class CopyRequest : BaseEntity<Guid>
    {
        public Book Book { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public string PagesRange { get; set; }
    }
}
