using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LAB01_ED1_G.Models
{
    public class jugador
    {
        
        public int? ID { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        public string Nombre { get; set; }
        [Display(Name = "Apellido")]
        [Required]
        public string Apellido { get; set; }
        [Display(Name = "Rol")]
        [Required]
        public string Rol { get; set; }
        [Display(Name = "KDA")]
        [Required]
        public decimal? KDA { get; set; }
        [Display(Name = "Creep Score")]
        [Required]
        public int? CreepScore { get; set; }
        [Display(Name = "Equipo")]
        [Required]
        public string Equipo { get; set; }


    }
}
