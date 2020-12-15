using System;
using System.Collections.Generic;
using System.Text;
using LibraryIS.SharedKernel;

namespace LibraryIS.Core.Entities
{
    public class ReaderProfile : BaseEntity<Guid>
    {
        public string LibraryCard { get; set; }
        public string PassportSeriesAndNumber { get; set; }
        public DateTime BornYear { get; set; }
        public List<TakenBook> TakenBooks { get; set; }
        public List<ReservedBook> ReservedBooks { get; set; }
        public List<CopyRequest> CopyRequests { get; set; }
        public List<Genre> TopGenres { get; set; }
        public List<Evaluation> Evaluations { get; set; }
    }
}
