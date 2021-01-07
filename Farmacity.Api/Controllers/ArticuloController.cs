using System.Web.Http;
using Farmacity.Api.Models;
using Farmacity.Api.Service;

namespace Farmacity.Api.Controllers
{
    public class ArticuloController : BaseController
    {
        private ArticuloRepository _articuloRepository;

        public ArticuloController()
        {
            _articuloRepository = new ArticuloRepository();
        }

        [HttpGet]
        public IHttpActionResult ObtenerListado()
        {
            if (!ModelState.IsValid) return CreateHttpErrorMessage(GetModelErrors());
            var articulos = _articuloRepository.ObtenerTodos();
            return Ok(articulos);
        }

        [HttpPost]
        public IHttpActionResult AgregarArticulo(Articulo articulo)
        {
            if (!ModelState.IsValid) return CreateHttpErrorMessage(GetModelErrors());
            _articuloRepository.AgregarArticulo(articulo);
            return Ok();

        }

        [HttpPatch]
        public IHttpActionResult EditarArticulo(Articulo articulo)
        {
            if (!ModelState.IsValid) return CreateHttpErrorMessage(GetModelErrors());
            _articuloRepository.EditarArticulo(articulo);
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult BorrarArticulo(Articulo articulo)
        {
            if (!ModelState.IsValid) return CreateHttpErrorMessage(GetModelErrors());
            _articuloRepository.BorrarArticulo(articulo);
            return Ok();
        }
    }
}
