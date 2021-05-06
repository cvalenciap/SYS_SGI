using System.Configuration;

namespace SYS_SGI.Presentation.Utilities
{
    public static class Constantes
    {
        public static class Permisos
        {
            public static class Codigo
            {
                public const int Lectura = 1;
                public const int Escritura = 2;
                public const int ControlTotal = 3;
            }

            public static class Style
            {
                public const string Activo = "pointer-events:auto;opacity:1;";
                public const string Desactivo = "pointer-events:none;opacity:0.6;";
                public const string Disable = "pointer-events: none;cursor: default;";
            }
        }

        public static class Sistema
        {
            public const string Cliente = "Corpac";
            public const int CodigoLocalidad = 1;
        }

        public static class Servicio
        {
            public static class Token
            {
                public static string SSPL = ConfigurationManager.AppSettings["CLIENT_SITE_TOKEN"].ToString();
                public static string SIGA = ConfigurationManager.AppSettings["SIGA_WEB_SERVICE"].ToString();
            }

            public static class Resultado
            {
                public const string Success = "Ok";
            }
        }

        public static class Formato
        {
            public static class Fecha
            {
                public const string FormatDate_Slash_MMyyyy = "MM/yyyy";
                public const string FormatDate_Slash_ddMMyyyy = "dd/MM/yyyy";
                public const string FormatDate_Slash_ddMMyyyy_HHmm = "dd/MM/yyyy HH:mm";
                public const string FormatDate_Guion_MMyyyy = "MM-yyyy";
                public const string FormatDate_Guion_ddMMyyyy = "dd-MM-yyyy";
                public const string FormatDate_Guion_ddMMyyyy_HHmm = "dd-MM-yyyy HH:mm";
            }

            public static class Decimal
            {
                public const string FormatoDosDecimales = "#0,#.00";
                public const string FormatoMoney = "#,###,##0.00";
                public const string StringFormatToNumber = "{0:#0,#.00}";
            }
        }

        public static class Datos
        {
            public const string Activo = "Activo";
            public const string Desactivo = "Desactivo";
        }

        public static class Controles
        {
            public static class Combo
            {
                public const string Seleccione = "Seleccione";
                public const string Todos = "Todos";
                public const string Otros = "Otros";
            }

            public static class Paginacion
            {
                public static class Descripcion
                {
                    public const string Primero = "Primero";
                    public const string Anterior = "Anterior";
                    public const string Siguieunte = "Siguiente";
                    public const string Ultimo = "Ultimo";
                }

                public static class Orientacion
                {
                    public const string Ascendente = "Ascending";
                    public const string Descendente = "Descending";
                }

                public static class FilasPorPagina
                {
                    public const int Normal = 10;
                    public const int Muchos = 20;
                    public const int Detalle = 5;
                    public const int Todos = 0;
                    public const int PaginaDefecto = 1;
                    public const int Maximo = 5000;
                }
            }

            public static class Modal
            {
                public const string Default = "divModalBody";

                public static class Size
                {
                    public const string SizeWide = "SIZE_WIDE";
                    public const string SizeSmall = "SIZE_SMALL";
                    public const string SizeNormal = "SIZE_NORMAL";
                }

                public static class Type
                {
                    public const string Defecto = "BootstrapDialog.TYPE_DEFAULT";
                    public const string Informativo = "BootstrapDialog.TYPE_INFO";
                    public const string Primario = "BootstrapDialog.TYPE_PRIMARY";
                    public const string Correcto = "BootstrapDialog.TYPE_SUCCESS";
                    public const string Precaucion = "BootstrapDialog.TYPE_WARNING";
                    public const string Peligro = "BootstrapDialog.TYPE_DANGER";
                }
            }

            public static class Alerta
            {
                public static class Tipo
                {
                    public const string Warning = "W";
                    public const string Error = "E";
                    public const string Success = "S";
                    public const string Info = "I";
                    public const string SessionExpired = "SE";
                }

                public static class TipoString
                {
                    public const string Warning = "warning";
                    public const string Error = "error";
                    public const string Success = "success";
                    public const string Info = "info";
                }

                public static class Glificon
                {
                    public const string Warning = "<span class=\"glyphicon glyphicon-remove\"></span>";
                    public const string Error = "<span class=\"glyphicon glyphicon-ok\"></span>";
                    public const string Succes = "<span class=\"glyphicon glyphicon-ok\"></span>";
                    public const string Info = "<span class=\"glyphicon glyphicon-ok\"></span>";
                }

                public static class Icon
                {
                    public const string Warning = "glyphicon glyphicon-warning-sign";
                    public const string Error = "glyphicon glyphicon-warning-sign";
                    public const string Success = "glyphicon glyphicon-exclamation-sign";
                    public const string Info = "glyphicon glyphicon-question-sign";
                }

                public static class From
                {
                    public const string Superior = "top";
                    public const string Inferior = "bottom ";
                }

                public static class Align
                {
                    public const string Izquierda = "left";
                    public const string Centro = "center";
                    public const string Derecha = "right";
                }

                public static class Texto
                {
                    public const string MensajeDefecto = "Ocurrió un error al realizar la operación";
                    public const string TituloDefecto = "Mensaje del sistema";
                }
            }

            public static class Layout
            {
                public const string MenuSpan = "<span class=\"fa fa-list-alt\"></span>";
                public const string MenuSpanColor = "<span style=\"background-color:#2c3b41; color:#2c3b41;\">----</span>";
            }
        }

        public static class Sesion
        {
            public static class Usuario
            {
                public const string Login = "0e358c44-f1b8-426c-ad89-1c0ee8af511b";
                public const string Codigo = "5e43e52e-86b6-4647-a433-ab7e38d565b5";
                public const string NombreCompleto = "0007b3d3-c479-41fa-a2fd-5a58a7bc1ffc";
                public const string Foto = "f55779c2-55a5-475d-a213-cd166a49b5e1";
                public const string Correo = "9cc31d15-956d-4cc3-9960-9a2d549bee0e";
                public const string Area = "fea77990-5ea4-4043-99cd-4f9c832be899";
                public const string CodigoArea = "a3c926a5-62cf-4837-acbf-511cd044f583";
            }

            public static class Perfil
            {
                public const string Nombre = "0dd20e1a-1864-4791-8b5b-225a9d8b0fe1";
                public const string Codigo = "c5812d75-82ec-45dc-815b-ddc3319c9497";
                public const string Descripcion = "995c01a6-b914-4936-8c92-79ff0732d370";
            }

            public static class Empresa
            {
                public const string Codigo = "d3cb963e-9906-425c-917d-9436fe61147b";
                public const string RazonSocial = "cbc900a7-8520-46c2-8702-b3b6533affae";
                public const string Alias = "064f7d92-70fe-4462-a09c-928eae85d7a3";
                public const string Ruc = "064f7s92-70fe-4462-a09c-928eae85d7a3";
                public const string ImagenLogo = "9ba900a7-8527-46c2-8702-b3b6533affae";
                public const string ImagenLogoMini = "764u7d92-70fe-446a-a09c-938eae85d7a3";
                //public const string Lista_Empresa = "a54u7d92-70fe-446a-a09c-938eae85d7a3";
            }

            public static class Sistema
            {
                public const string Codigo = "d3cb963e-9906-422c-917d-9436fe61147b";
                public const string Nombre = "cbc900a7-8529-46c2-8702-b3b6533affae";
                public const string Descripcion = "064f7d92-70fe-4461-a09c-928eae85d7a3";                
            }

            public static class Permisos
            {
                public const string Lista_Modulos = "44c01bce-ecf0-4b0e-a9cd-3b714a5012af";
                public const string Lista_Opciones = "a5712c06-b2e0-4315-aea9-e0589abda36f";
                public const string Lista_PermisosControlador = "08d3f5f7-b9c2-40b9-b751-1e32069d4ba4";
                public const string Lista_SubOpciones = "3e1e0330-f2b3-413c-a1e3-d247e4627dbd";
            }

            public static class FormulaDetalle
            {
                public const string ElementosFormulas = "ElementoFormulaDetale";
            }

            public static class ExportarGrilla
            {
                public const string SessionData = "29092d85-48f9-4e1b-a200-87cf6dfd9f7e";
            }
        }
        
        public static class Controladores
        {
            public static class FlujoCaja
            {
                public static class Tipo
                {
                    public const string raiz = "root";
                    public const string nivel = "estructura";
                    public const string conceptoFinanciero = "concepto-estructura";
                    public const string formula = "formula";
                    public const string codigoFinanciero = "codigo";
                }

                public enum Numerico
                {
                    raiz = 1,
                    nivel = 2,
                    conceptoFinanciero = 3,
                    formula = 4,
                    codigoFinanciero = 5
                }

                public static class Grilla
                {
                    public const string Index = "1";
                    public const string Modal = "2";
                }
            }

            public static class ConceptoFinanciero
            {
                public static class Grilla
                {
                    public const string Index = "1";
                    public const string Modal = "2";
                }
            }

            public static class Formula
            {
                public static class Grilla
                {
                    public const string Index = "1";
                    public const string Modal = "2";
                }
            }
        }
        public static class NTC
        {
            public static class Informe
            {
                public const string Idcos = "1";
                public const string Ieod = "2";
                public const string MantenimientoProgramado = "3";
                public const string MantenimientoProgramadoPreliminar = "4";
            }
            public static class OrigenProgramacion
            {
                public const int Anual = 1;
                public const int Mensual = 2;
                public const int Semanal = 3;
                public const int Diario = 4;
            }
        }
   
    }
}