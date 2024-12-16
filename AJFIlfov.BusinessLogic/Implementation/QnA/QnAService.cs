using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.BusinessLogic.Implementation.Audituri;
using AJFIlfov.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using QnA.Models;

namespace QnA.BusinessLogic
{
    public class QuestionService : BaseService
    {

        public QuestionService(ServiceDependencies dependencies)
            : base(dependencies)
        {

        }

        public List<QuestionModel> GetAllQuestions()
        {
            var question = UnitOfWork.Questions.Get().OrderByDescending(q => q.CreatedAt).ToList();
            return Mapper.Map<List<QuestionModel>>(question);
        }

        public QuestionModel GetQuestionById(int id)
        {
            var question = UnitOfWork.Questions.Get()
                .Include(q => q.Answers.OrderByDescending(a => a.UpVote)) // Order answers by upvotes
                .FirstOrDefault(q => q.Id == id);

            var questionModel = Mapper.Map<QuestionModel>(question);

            // Map user points for each answer
            foreach (var answer in questionModel.Answers)
            {
                var user = UnitOfWork.Users.Get().FirstOrDefault(u => u.IdUtilizator == answer.UserId);
                if (user != null)
                {
                    answer.UserPoints = user.Points;
                }
            }

            return questionModel;
        }

        public void AddQuestion(string title, string content)
        {
            var question = new QuestionModel
            {
                Title = title,
                Content = content,
                UserId = CurrentUser.Id,
                CreatedAt = DateTime.Now,
                UserName = CurrentUser.FirstName + " " + CurrentUser.LastName
            };

            UnitOfWork.Questions.Insert(Mapper.Map<Question>(question));
            UnitOfWork.SaveChanges();
        }

        public void AddAnswer(int questionId, string content)
        {
            var answer = new AnswerModel
            {
                QuestionId = questionId,
                Content = content,
                UserId = CurrentUser.Id,
                CreatedAt = DateTime.Now,
                UserName = CurrentUser.FirstName + " " + CurrentUser.LastName
            };

            UnitOfWork.Answers.Insert(Mapper.Map<Answer>(answer));
            UnitOfWork.SaveChanges();
        }

        public void AddSuggestion(int questionId, string suggestedContent)
        {
            var suggestion = new SuggestionModel
            {
                QuestionId = questionId,
                SuggestedContent = suggestedContent,
                UserId = CurrentUser.Id,
                CreatedAt = DateTime.Now,
                UserName = CurrentUser.FirstName + " " + CurrentUser.LastName
            };

            UnitOfWork.Suggestions.Insert(Mapper.Map<Suggestion>(suggestion));
            UnitOfWork.SaveChanges();
        }

        public void MarkBestAnswer(int questionId, int answerId)
        {
            var question = UnitOfWork.Questions.Get().FirstOrDefault(q => q.Id == questionId);
            if (question != null)
            {
                question.BestAnswerId = answerId;
                UnitOfWork.Questions.Update(question);
                UnitOfWork.SaveChanges();
            }
        }

        public void UpvoteAnswer(int answerId)
        {
            var answer = UnitOfWork.Answers.Get().FirstOrDefault(a => a.Id == answerId);
            if (answer != null && answer.UserId != CurrentUser.Id)
            {
                answer.UpVote++;
                UnitOfWork.Answers.Update(answer);
                UnitOfWork.SaveChanges();
            }
        } 

        public void DownvoteAnswer(int answerId)
        {
            var answer = UnitOfWork.Answers.Get().FirstOrDefault(a => a.Id == answerId);
            if (answer != null && answer.UserId != CurrentUser.Id)
            {
                answer.DownVote++;
                UnitOfWork.Answers.Update(answer);
                UnitOfWork.SaveChanges();
            }
        }

        public Guid GetAnswerAuthorId(int answerId)
        {
            var answer = UnitOfWork.Answers.Get().FirstOrDefault(a => a.Id == answerId);
            return answer.UserId;
        }

        public int GetQuestionIdByAnswerId(int answerId)
        {
            var answer = UnitOfWork.Answers.Get().FirstOrDefault(a => a.Id == answerId);
            return answer?.QuestionId ?? 0;
        }
    }
}