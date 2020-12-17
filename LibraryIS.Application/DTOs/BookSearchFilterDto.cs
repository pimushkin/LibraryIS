using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryIS.Core.Entities;

namespace LibraryIS.Application.DTOs
{
    public class BookSearchFilterDto
    {
        public string? Title { get; set; }
        public string? Genre { get; set; } 
        public string[]? Authors { get; set; }
        public string? Language { get; set; } 
        public double? Rating { get; set; }
        public DateTime? BeginningPublicationDate { get; set; }
        public DateTime? EndPublicationDate { get; set; }
    }
}
