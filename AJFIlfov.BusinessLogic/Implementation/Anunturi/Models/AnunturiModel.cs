// BusinessLogic.Implementation.AnuntService.Models/AnuntModel.cs
namespace AJFIlfov.BusinessLogic.Implementation.Anunturi.Models
{
    public class AnuntModel
    {
        public int Id { get; set; }
        public string Titlu { get; set; }
        public string Continut { get; set; }
        public DateTime DataPublicarii { get; set; }
        public byte[] Imagine { get; set; } // Proprietate pentru imagine stocată ca byte[]
        public string TipAnunt { get; set; }
    }
}