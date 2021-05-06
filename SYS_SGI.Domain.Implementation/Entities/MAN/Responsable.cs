using SYS_SGI.Domain.Implementation.Entities.BASE;
using System.ComponentModel.DataAnnotations;
using System;

namespace SYS_SGI.Domain.Implementation.Entities.MAN
{
    public class Responsable : Audity
    {
        [Display(Name = "Responsable")]
        public float CodigoResponsable { get; set; }

        [Display(Name = "Tipo Documento")]
        public int CodigoTipoDocumento { get; set; }

        [Display(Name = "Género")]
        public int CodigoGenero { get; set; }

        [Display(Name = "Cargo")]
        public int CodigoCargo { get; set; }

        [Display(Name = "Nombre")]
        public string Nombres { get; set; }

        [Display(Name = "Apellido Paterno")]
        public string ApellidoPaterno { get; set; }

        [Display(Name = "Apellido Materno")]
        public string ApellidoMaterno { get; set; }

        [Display(Name = "Número Documento")]
        public string NumeroDocumento { get; set; }

        [Display(Name = "Correo Electrónico")]
        public string CorreoElectronico { get; set; }

        [Display(Name = "Nombres")]
        public string NombreJefeDirecto { get; set; }

        [Display(Name = "Cargo")]
        public int CodigoCargoJefeDirecto { get; set; }

        [Display(Name = "Correo Electrónico")]
        public string CorreoJefeDirecto { get; set; }

        [Display(Name = "Nombres")]
        public string NombreGerente { get; set; }

        [Display(Name = "Cargo")]
        public int CodigoCargoGerente { get; set; }

        [Display(Name = "Correo Electrónico")]
        public string CorreoGerente { get; set; }

    }
}
