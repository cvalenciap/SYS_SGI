using SYS_SGI.Domain.Implementation.Common.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace SYS_SGI.Domain.Implementation.Entities.BASE
{
    public class Entity
    {
        public int I_TOTAL_RECORDS { get; set; }
        public int ESTADO { get; set; }

        [Display(Name = "Usu. Crea:")]
        public string USUARIO_CREACION { get; set; }

        [Display(Name = "Fecha Crea:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? FECHA_CREACION { get; set; }

        public string TERMINAL_CREACION { get; set; }

        [Display(Name = "Usu. Mod.:")]
        public string USUARIO_MODIFICACION { get; set; }

        [Display(Name = "Fecha Mod.:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? FECHA_MODIFICACION { get; set; }

        public string TERMINAL_MODIFICACION { get; set; }

        private string _accion = Enums.Action.New;

        public string ACCION
        {
            get
            {
                return this._accion;
            }
            set
            {
                _accion = string.IsNullOrEmpty(value) ? Enums.Action.New : value;
            }
        }

        public long TotalRegistros { get; set; }
    }
}