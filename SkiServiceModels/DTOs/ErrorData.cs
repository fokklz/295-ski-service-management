using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiServiceModels.DTOs
{
    public class ErrorData
    {

        public string MessageCode { get; set; }

        public bool Breaking { get; set; } = false;
    }
}
