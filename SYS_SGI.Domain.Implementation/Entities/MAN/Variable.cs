using SYS_SGI.Domain.Implementation.Entities.BASE;
using System.ComponentModel.DataAnnotations;
using System;

namespace SYS_SGI.Domain.Implementation.Entities.MAN
{
    public class Variable : Audity
    {
        [Display(Name = "Variable")]
        public float CodigoVariable { get; set; }

        [Display(Name = "Periodicidad")]
        public int CodigoPeriodicidad { get; set; }

        [Display(Name = "Unidad Medida")]
        public int CodigoUnidadMedida { get; set; }
        
        [Display(Name = "Área")]
        public int CodigoArea { get; set; }

        [Display(Name = "Responsable")]
        public int CodigoResponsable { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Especificar")]
        public string Especificar { get; set; }

    }
}
