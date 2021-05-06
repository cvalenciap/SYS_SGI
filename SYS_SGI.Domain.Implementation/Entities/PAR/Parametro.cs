using System;
using SYS_SGI.Domain.Implementation.Entities.BASE;
using System.ComponentModel.DataAnnotations;

namespace SYS_SGI.Domain.Implementation.Entities.PAR
{
    public class Parametro : Audity
    {
        [Display(Name = "Código")]        
        public long CodigoParametro { get; set; }
        
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar un {0} de parámetro")]
        public string Nombre { get; set; }

        [Display(Name = "Nombre Corto")]
        [Required(ErrorMessage = "Debe ingresar un {0} de parámetro")]
        public string NombreCorto { get; set; }
        
        [Display(Name = "Descripción")] 
        public string Descripcion { get; set; }

        [Display(Name = "Orden")]
        public int Orden { get; set; }

        [Display(Name = "Funcional")]
        public bool Funcional { get; set; }

        [Display(Name = "Valor")]
        public string Valor { get; set; }
    }
}
