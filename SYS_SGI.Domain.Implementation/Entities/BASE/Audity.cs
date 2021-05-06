using System;
using System.ComponentModel.DataAnnotations;

namespace SYS_SGI.Domain.Implementation.Entities.BASE
{
    public class Audity
    {
        [Display(Name = "Estado")]
        public bool Estado { get; set; }

        [Display(Name = "Usuario Creación")]
        public string UsuarioCreacion { get; set; }

        [Display(Name = "Fecha Creación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaCreacion { get; set; }

        public string terminalCreacion;

        [Display(Name = "Terminal Creación")]
        public string TerminalCreacion { get { return Environment.MachineName; } set { this.terminalCreacion = value; } }
        
        [Display(Name = "Usuario Modificación")]
        public string UsuarioModificacion { get; set; }

        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaModificacion { get; set; }

        public string terminalModificacion;

        [Display(Name = "Terminal Modificación")]
        public string TerminalModificacion { get { return Environment.MachineName; } set { this.terminalModificacion = value; } }
    }
}
