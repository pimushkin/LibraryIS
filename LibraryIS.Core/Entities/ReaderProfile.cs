using System;
using System.Collections.Generic;
using System.Text;
using LibraryIS.SharedKernel;

namespace LibraryIS.Core.Entities
{
    public class ReaderProfile : BaseEntity<Guid>
    {
        public User User { get; set; }
        public string LibraryCard { get; set; }
        public string PassportSeriesAndNumber { get; set; }
        public DateTime BornYear { get; set; }
        public List<TakenBook>? TakenBooks { get; set; }
        public List<ReservedBook>? ReservedBooks { get; set; }
        public List<ElectronicCopyRequest>? ElectronicCopyRequests { get; set; }
        public List<Genre>? TopGenres { get; set; }
        public List<Evaluation>? Evaluations { get; set; }
    }
}
