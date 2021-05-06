using SYS_SGI.Domain.Implementation.Entities.BASE;
using SYS_SGI.Domain.Implementation.Entities.SEG;

namespace SYS_SGI.Domain.Implementation.LogicalEntities.SEG
{
    public class OpcionLogic : Opcion
    {
        public InfoMovs InfoMovs { get; set; }

        public int CantidadTotalRegistros { get; set; }

        public int CodigoAccion { get; set; }

        public string Modulo { get; set; }

        public string OpcionPadreDesc { get; set; }

        public string GlyphiconModulo { get; set; }

        public int CodigoPerfil { get; set; }

        public string Perfil { get; set; }

        public string RutaImagen { get; set; }

        public string ControladorModulo { get; set; }

        public string MetodoModulo { get; set; }

    }
}
