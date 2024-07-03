using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJFIlfov.BusinessLogic.Implementation.DisponibilitateService.Models
{
    public class CerereModel
    {
        public Guid IdCerere { get; set; }
        public DateTime Zi { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? OraIncepere { get; set; }
        public DateTime? OraIncheiere { get; set; }
        public string Status { get; set; }
    }
}
