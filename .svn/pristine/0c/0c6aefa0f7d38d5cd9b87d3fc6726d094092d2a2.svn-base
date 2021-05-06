using SYS_SGI.Domain.Implementation.Entities.BASE;
using System.ComponentModel.DataAnnotations;

namespace SYS_SGI.Domain.Implementation.Entities.SEG
{
    public class Parametro : Audity
    {
        [Display(Name = "Parámetro")]
        public long CodigoParametro { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar un {0} de elemento de parámetro")]
        public string Nombre { get; set; }

        [Display(Name = "Nombre Corto")]
        [Required(ErrorMessage = "Debe ingresar un {0} de elemento de parámetro")]
        public string NombreCorto { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Elemento")]
        public int CodigoElemento { get; set; }

        [Display(Name = "Valor")]
        public string Valor { get; set; }
    }
}
