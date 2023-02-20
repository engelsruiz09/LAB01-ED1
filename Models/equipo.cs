using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LAB01_ED1_G.Models
{
    public class equipo
    {
        public int? ID { get; set; }

        [Display(Name = "Nombre Equipo")]
        [Required]
        public string NombreEquipo { get; set; }
        [Display(Name = "Coach")]
        [Required]
        public string Coach { get; set; }
        [Display(Name = "Liga")]
        [Required]
        public string Liga { get; set; }

        [Display(Name = "Fecha de creación")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime? FechaCreacion { get; set; }

    }

}
