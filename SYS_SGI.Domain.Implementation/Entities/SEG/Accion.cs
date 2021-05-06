using SYS_SGI.Domain.Implementation.Entities.BASE; 
using System.ComponentModel.DataAnnotations; 

namespace SYS_SGI.Domain.Implementation.Entities.SEG
{ 
	 public class Accion : Audity 
	 { 
	 	 [Display(Name = "Acci�n")] 
	 	 public float CodigoAccion { get; set; } 

	 	 [Display(Name = "Nombre")] 
	 	 public string Nombre { get; set; } 

	 	 [Display(Name = "Descripci�n")] 
	 	 public string Descripcion { get; set; } 

	 } 
} 

