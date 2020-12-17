using System;
using LibraryIS.SharedKernel;

namespace LibraryIS.Core.Entities
{
    public class CopyBook : BaseEntity<Guid>
    {
        public Book Book { get; set; }
    }
}
