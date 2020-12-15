using System;
using System.Collections.Generic;
using System.Text;
using LibraryIS.SharedKernel;

namespace LibraryIS.Core.Entities
{
    public class Evaluation : BaseEntity<Guid>
    {
        public double Rating { get; set; }
        public Book Book { get; set; }
        public ReaderProfile Profile { get; set; }
    }
}
