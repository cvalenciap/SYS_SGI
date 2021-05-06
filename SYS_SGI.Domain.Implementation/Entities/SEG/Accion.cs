using SYS_SGI.Domain.Implementation.Entities.BASE; 
using System.ComponentModel.DataAnnotations; 

namespace SYS_SGI.Domain.Implementation.Entities.SEG
{ 
	 public class Accion : Audity 
	 { 
	 	 [Display(Name = "Acción")] 
	 	 public float CodigoAccion { get; set; } 

	 	 [Display(Name = "Nombre")] 
	 	 public string Nombre { get; set; } 

	 	 [Display(Name = "Descripción")] 
	 	 public string Descripcion { get; set; } 

	 } 
} 

