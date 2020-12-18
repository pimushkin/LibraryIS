using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryIS.Application.DTOs
{
    public class ElectronicCopyRequestDto
    {
        public Guid BookId { get; set; }
        public string PagesRange { get; set; }
    }
}
