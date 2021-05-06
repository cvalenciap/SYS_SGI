using SYS_SGI.Domain.Implementation.Entities.BASE;
using System;
using System.ComponentModel.DataAnnotations;

namespace SYS_SGI.Domain.Implementation.Entities.SEG
{
    public class Usuarios : Audity
    {
        [Display(Name = "Usuario")]
        public long CodigoUsuario { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido")]
        public string Apellido { get; set; }        
        
        [Display(Name = "Correo Eletrónico")]
        public string Email { get; set; }
        
        [Display(Name = "Tipo Documento")]
        public long TipoDocumentoIdentidad { get; set; }

        [Display(Name = "Número Documento")]
        public string NumDocumentoIdentidad { get; set; }

        [Display(Name = "Foto")]
        public byte[] Foto { get; set; }

        [Display(Name = "Usuario")]
        public string Usuario { get; set; }

        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; }
        
        [Display(Name = "Dominio de Red")]
        public string DominioRed { get; set; }

        [Display(Name = "Código Persona")]
        public string CodigoPersona { get; set; }
        
        [Display(Name = "Código Cargo")]
        public long CodigoCargo { get; set; }        
    }
}
