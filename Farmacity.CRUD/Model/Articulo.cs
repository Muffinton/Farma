namespace Farmacity.CRUD.Model
{
    public class Articulo
    {
        public int IdArticulo { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }
        public int Activo { get; set; }
    }
}
