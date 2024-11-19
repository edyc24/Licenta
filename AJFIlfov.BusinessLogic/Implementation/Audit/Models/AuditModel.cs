// BusinessLogic.Implementation.AnuntService.Models/AnuntModel.cs
namespace AJFIlfov.BusinessLogic.Implementation.Audituri.Models
{
    public class AuditModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string ActionPerformed { get; set; }
        public DateTime Timestamp { get; set; }
    }
}