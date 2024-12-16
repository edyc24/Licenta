using AJFIlfov.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJFIlfov.Entities.Entities
{
    public class Suggestion : IEntity
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string SuggestedContent { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }

        public string UserName { get; set; }
    }
}
