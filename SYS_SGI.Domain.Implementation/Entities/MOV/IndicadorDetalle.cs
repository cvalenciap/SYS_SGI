using SYS_SGI.Domain.Implementation.Entities.BASE;
using System.ComponentModel.DataAnnotations;
using System;

namespace SYS_SGI.Domain.Implementation.Entities.MOV
{
    public class IndicadorDetalle : Audity
    {
        [Display(Name = "Indicador Detalle")]
        public float CodigoIndicadorDetalle { get; set; }

        [Display(Name = "Indicador")]
        public float CodigoIndicador { get; set; }

        [Display(Name = "Alineamiento")]
        public float CodigoAlineamientoEstrategico { get; set; }

        [Display(Name = "Tipo Valor")]
        public int CodigoTipoValor { get; set; }

        [Display(Name = "Tipo Registro")]
        public int CodigoTipoRegistro { get; set; }

        [Display(Name = "Tipo Plan")]
        public int CodigoTipoPlan { get; set; }

        [Display(Name = "Fecha Período")]
        public DateTime FechaPeriodo { get; set; }

        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

        [Display(Name = "Valor Al")]
        public decimal ValorAl { get; set; }

        [Display(Name = "Comentario")]
        public string Comentario { get; set; }

        [Display(Name = "Comentario Al")]
        public string ComentarioAl { get; set; }
    }
}
