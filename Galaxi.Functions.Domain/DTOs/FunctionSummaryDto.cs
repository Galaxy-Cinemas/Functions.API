using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxi.Functions.Domain.DTOs
{
    public class FunctionSummaryDto
    {
        public Guid FunctionId { get; set; }
        public Guid MovieId { get; set; }
        public DateTime FunctionDate { get; set; }
    }
}
