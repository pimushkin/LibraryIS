﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryIS.Application.DTOs
{
    public class EvaluationDto
    {
        public double Rating { get; set; }
        public Guid BookId { get; set; }
    }
}
