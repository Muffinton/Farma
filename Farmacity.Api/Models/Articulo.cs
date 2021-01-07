using System.ComponentModel.DataAnnotations;

namespace Farmacity.Api.Models
{
    public class Articulo
    {
        [Key]
        public int IdArticulo { get; set; }
        [StringLength(250)]
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public double Precio { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public int Activo { get; set; }
    }
}