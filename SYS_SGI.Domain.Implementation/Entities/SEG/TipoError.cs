using SYS_SGI.Domain.Implementation.Entities.BASE; 
using System.ComponentModel.DataAnnotations; 

namespace SYS_SGI.Domain.Implementation.Entities.SEG
{ 
	 public class TipoError : Audity 
	 { 
	 	 [Display(Name = "Codigo Tipo Error")] 
	 	 public float CodigoTipoError { get; set; } 

	 	 [Display(Name = "Codigo")] 
	 	 public string Codigo { get; set; } 

	 	 [Display(Name = "Nombre")] 
	 	 public string Nombre { get; set; } 

	 	 [Display(Name = "Descripcion")] 
	 	 public string Descripcion { get; set; } 

	 } 
} 

