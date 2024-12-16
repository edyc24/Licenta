namespace QnA.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public int? BestAnswerId { get; set; }
        public List<AnswerModel> Answers { get; set; }
        public List<SuggestionModel> Suggestions { get; set; }
    }

    public class AnswerModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public int UpVote { get; set; }
        public int DownVote { get; set; }
        public int? UserPoints { get; set; }
    }

    public class SuggestionModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string SuggestedContent { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}