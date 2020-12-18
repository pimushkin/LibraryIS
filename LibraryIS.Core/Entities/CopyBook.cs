using System;
using LibraryIS.SharedKernel;

namespace LibraryIS.Core.Entities
{
    public class CopyBook : BaseEntity<Guid>
    {
        public ReaderProfile ReaderProfile { get; set; }
        public Book Book { get; set; }
    }
}
