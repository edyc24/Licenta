using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AJFIlfov.Common;

namespace AJFIlfov.Entities.Entities
{
    public class Question : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public int? BestAnswerId { get; set; }
        public List<Answer> Answers { get; set; }
        public List<Suggestion> Suggestions { get; set; }

        public string UserName { get; set; }
    }
}
