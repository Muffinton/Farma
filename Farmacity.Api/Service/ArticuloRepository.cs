using System.Collections.Generic;
using System.Linq;
using Farmacity.Api.Models;

namespace Farmacity.Api.Service
{
    public class ArticuloRepository
    {
        public List<Articulo> ObtenerTodos()
        {
            using (var db = new Context())
            {
                return db.Articulos.ToList();
            }
        }

        public void AgregarArticulo(Articulo articulo)
        {
            using (var db = new Context())
            {
                db.Articulos.Add(articulo);
                db.SaveChanges();
            }
        }

        public void EditarArticulo(Articulo articulo)
        {
            using (var db = new Context())
            {
                Articulo oArticulo = (from q in db.Articulos
                    where q.IdArticulo == articulo.IdArticulo
                    select q).First();

                oArticulo.Activo = articulo.Activo;
                oArticulo.Descripcion = articulo.Descripcion;
                oArticulo.Precio = articulo.Precio;
                oArticulo.Stock = articulo.Stock;
                db.SaveChanges();
            }
        }

        public void BorrarArticulo(Articulo articulo)
        {
            using (var db = new Context())
            {
                Articulo oArticulo = (from q in db.Articulos
                    where q.IdArticulo == articulo.IdArticulo
                    select q).First();

                db.Articulos.Remove(oArticulo);
                db.SaveChanges();
            }
        }
    }
}