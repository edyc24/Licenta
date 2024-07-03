namespace AJFIlfov.BusinessLogic.Implementation.StadionLocalitateService.Models
{
    public class StadionLocalitateModel
    {
        public Guid IdStadionLocalitate { get; set; }
        public Guid? IdStadion { get; set; }
        public Guid? IdLocalitate { get; set; }
        public string? StadionNume { get; set; } // For display purposes
        public string? LocalitateNume { get; set; } // For display purposes
    }
}
