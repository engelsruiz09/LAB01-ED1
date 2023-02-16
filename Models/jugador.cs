using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LAB01_ED1_G.Models
{
    public class jugador
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string club { get; set; }
        public decimal? salario { get; set; }
        public string posicion { get; set; }
        public decimal? compesacion { get; set; } //el ? significa que los objetos son de tipo anulables
        public int id { get; set; }
    }
}
