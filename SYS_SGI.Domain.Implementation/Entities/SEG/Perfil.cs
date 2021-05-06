using SYS_SGI.Domain.Implementation.Entities.BASE;
using System;
using System.ComponentModel.DataAnnotations;

namespace SYS_SGI.Domain.Implementation.Entities.SEG
{
    public class Perfil : Audity
    {
        [Display(Name = "Perfil")]
        public long CodigoPerfil { get; set; }

        [Display(Name = "Sistema")]
        public long CodigoSistema { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}
