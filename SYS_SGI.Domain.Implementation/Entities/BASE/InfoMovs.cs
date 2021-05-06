using SYS_SGI.Domain.Implementation.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS_SGI.Domain.Implementation.Entities.BASE
{
    public class InfoMovs
    {
        public int I_TOTAL_RECORDS { get; set; }
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
