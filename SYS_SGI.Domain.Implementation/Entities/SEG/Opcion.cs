using SYS_SGI.Domain.Implementation.Entities.BASE;
using System.ComponentModel.DataAnnotations;

namespace SYS_SGI.Domain.Implementation.Entities.SEG
{
    public class Opcion : Audity
    {
        [Display(Name = "Opción")]
        public long CodigoOpcion { get; set; }

        [Display(Name = "Módulo")]
        public long CodigoModulo { get; set; }

        [Display(Name = "Opción Padre")]
        public long OpcionPadre { get; set; }

        [Display(Name = "Opción")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Icono")]
        public string Glyphicon { get; set; }

        [Display(Name = "Controlador")]
        public string Controlador { get; set; }

        [Display(Name = "Método")]
        public string Metodo { get; set; }

        [Display(Name = "Área")]
        public string Area { get; set; }

        [Display(Name = "Orden")]
        public int Orden { get; set; }

    }
}
