using SYS_SGI.Domain.Implementation.Entities.BASE;
using System.ComponentModel.DataAnnotations;
using System;

namespace SYS_SGI.Domain.Implementation.Entities.MOV
{
    public class AlineamientoConfiguracion : Audity
    {
        [Display(Name = "Lineamiento")]
        public float CodigoAlineamientoConfiguracion { get; set; }

        [Display(Name = "Anexo")]
        public int CodigoTipoGuiaEmpresarial { get; set; }

        [Display(Name = "Lineamiento1")]
        public int CodigoTipoAlineamiento1 { get; set; }

        [Display(Name = "Lineamiento2")]
        public int CodigoTipoAlineamiento2 { get; set; }

        [Display(Name = "Lineamiento3")]
        public int CodigoTipoAlineamiento3 { get; set; }

        [Display(Name = "Lineamiento4")]
        public int CodigoTipoAlineamiento4 { get; set; }

        [Display(Name = "Lineamiento5")]
        public int CodigoTipoAlineamiento5 { get; set; }

        [Display(Name = "Lineamiento6")]
        public int CodigoTipoAlineamiento6 { get; set; }

        [Display(Name = "Lineamiento7")]
        public int CodigoTipoAlineamiento7 { get; set; }

        [Display(Name = "Lineamiento8")]
        public int CodigoTipoAlineamiento8 { get; set; }

        [Display(Name = "Lineamiento9")]
        public int CodigoTipoAlineamiento9 { get; set; }

        [Display(Name = "Lineamiento10")]
        public int CodigoTipoAlineamiento10 { get; set; }

        [Display(Name = "Indicador")]
        public bool Indicador { get; set; }

    }
}
