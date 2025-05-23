﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJFIlfov.BusinessLogic.Implementation.Account.Models
{
    public class AppointmentModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; } // Add this line
        public string Description { get; set; }
        public string Status { get; set; } // e.g., Scheduled, Completed, Cancelled
        public string UserName { get; set; }
    }


}
