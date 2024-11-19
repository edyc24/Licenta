using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AJFIlfov.Common;

namespace AJFIlfov.Entities.Entities
{
    public class Audit : IEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string ActionPerformed { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
