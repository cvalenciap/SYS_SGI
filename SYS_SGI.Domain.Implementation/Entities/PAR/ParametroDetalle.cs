using SYS_SGI.Domain.Implementation.Entities.BASE;
using System.ComponentModel.DataAnnotations;

namespace SYS_SGI.Domain.Implementation.Entities.PAR
{
    public class ParametroDetalle : Audity
    {
        [Display(Name = "Detalle")]
        public long CodigoParametroDetalle { get; set; }

        [Display(Name = "Par�metro")]        
        public long CodigoParametro { get; set; }

        [Display(Name = "C�digo")]
        public string CodigoElemento { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar un {0} de elemento de par�metro")]
        public string Nombre { get; set; }

        [Display(Name = "Nombre Corto")]
        [Required(ErrorMessage = "Debe ingresar un {0} de elemento de par�metro")]
        public string NombreCorto { get; set; }

        [Display(Name = "Descripci�n")]
        public string Descripcion { get; set; }

        [Display(Name = "Valor")]
        public string Valor { get; set; }
    }
}
