using SYS_SGI.Domain.Implementation.Entities.BASE;
using System.ComponentModel.DataAnnotations;

namespace SYS_SGI.Domain.Implementation.Entities.SEG
{
    public class Sistema : Audity
    {
        [Display(Name = "Sistema")]
        public long CodigoSistema { get; set; }

        [Display(Name = "Empresa")]
        public long CodigoEmpresa { get; set; }
        
        [Display(Name = "Sistema")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Ruta")]
        public string Ruta { get; set; }

        [Display(Name = "Token")]
        public string Token { get; set; }

        [Display(Name = "Parámetro")]
        public int Parametro { get; set; }

        [Display(Name = "Valor de Parámetro")]
        public int ValorParametro { get; set; }
    }
}
