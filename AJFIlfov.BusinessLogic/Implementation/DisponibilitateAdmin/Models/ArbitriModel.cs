namespace AJFIlfov.BusinessLogic.Implementation.DisponibilitateAdminService.Models
{
    public class RefereeAvailabilityModel
    {
        public string RefereeName { get; set; }
        public string Rol { get; set; }
        public string Categorie { get; set; }
        public List<AvailabilityStatus> Availability { get; set; }
    }

    public class AvailabilityStatus
    {
        public string Zi { get; set; }
        public string Status { get; set; }
        public string? OraIncepere { get; set; } 
        public string? OraIncheiere { get; set; } 
    }
}
