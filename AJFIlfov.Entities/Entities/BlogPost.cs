using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AJFIlfov.Common;

namespace AJFIlfov.Entities.Entities
{
    public class BlogPost : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PublishedBy { get; set; }
    }

}
