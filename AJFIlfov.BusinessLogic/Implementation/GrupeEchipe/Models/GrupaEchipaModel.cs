using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AJFIlfov.BusinessLogic.Implementation.GrupeEchipaService.Models
{
    public class GrupaEchipaModel
    {
        public Guid IdGrupaEchipa { get; set; }
        public Guid IdEchipa { get; set; }
        public Guid IdGrupa { get; set; }
        public string EchipaNume { get; set; } 
        public string GrupaNume { get; set; } 
    }
}
