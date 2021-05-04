using LibraryIS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryIS.Domain.Entities
{
    public class ReaderProfile : Entity
    {
        public virtual User User { get; }
        public string LibraryCard { get; }
        public string PassportSeriesAndNumber { get; }
        public DateTime BornYear { get; }
        public virtual IReadOnlyList<TakenBook> TakenBooks { get; }
        public virtual IReadOnlyList<ReservedBook> ReservedBooks { get; }
        public virtual IReadOnlyList<ElectronicCopyRequest> ElectronicCopyRequests { get; }
        public virtual IReadOnlyList<Genre> TopGenres { get; }
        private readonly List<Evaluation> _evaluations = new List<Evaluation>();
        public virtual IReadOnlyList<Evaluation> Evaluations => _evaluations.ToList();

        protected ReaderProfile()
        {
        }

        public ReaderProfile(string libraryCard, string passportSeriesAndNumber, DateTime bornYear) : this()
        {
            LibraryCard = libraryCard;
            PassportSeriesAndNumber = passportSeriesAndNumber;
            BornYear = bornYear;
        }

        public virtual void AddEvaluationToBook(Evaluation evaluation)
        {
            _evaluations.Add(evaluation);
        }
    }
}
