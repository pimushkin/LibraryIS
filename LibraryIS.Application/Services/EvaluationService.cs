using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryIS.Application.Interfaces;
using LibraryIS.Core.Entities;
using LibraryIS.Core.Interfaces;

namespace LibraryIS.Application.Services
{
    public class EvaluationService : IEvaluationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EvaluationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Dictionary<Guid, double> GetBooksRatings()
        {
            var evaluations = _unitOfWork.GetRepository<Evaluation>().Filter(includeProperties: "Book")
                .GroupBy(x => x.Book.Id);
            var ratings = evaluations.ToDictionary(evaluation => evaluation.Key,
                evaluation => evaluation.Average(ev => ev.Rating));

            return ratings;
        }
    }
}
