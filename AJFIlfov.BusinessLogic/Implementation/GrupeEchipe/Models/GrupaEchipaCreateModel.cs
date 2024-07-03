using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AJFIlfov.BusinessLogic.Implementation.GrupeEchipaService.Models
{
    public class GrupaEchipaCreateModel
    {
        public Guid IdEchipa { get; set; }
        public Guid IdGrupa { get; set; }
    }
}
