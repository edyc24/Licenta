using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJFIlfov.BusinessLogic.Implementation.Account.Models
{
    public class CreateAppointmentModel
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Description { get; set; }
        public string Name { get; set; } // Assuming UserId is passed in the model
        public string PhoneNumber { get; set; } // Assuming UserId is passed in the model
    }

}
