using System;
using LibraryIS.SharedKernel;

namespace LibraryIS.Core.Entities
{
    public class Genre : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}
