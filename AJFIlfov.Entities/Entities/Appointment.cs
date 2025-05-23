﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AJFIlfov.Common;

namespace AJFIlfov.Entities.Entities
{
    public class Appointment : IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } // e.g., Scheduled, Completed, Cancelled
        public string PhoneNumber { get; set; }
        public string Name { get; set; }

    }

}
