using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryIS.Application.Interfaces
{
    public interface IEvaluationService
    {
        public Task<Dictionary<Guid, double>> GetBooksRatingsAsync();
    }
}
