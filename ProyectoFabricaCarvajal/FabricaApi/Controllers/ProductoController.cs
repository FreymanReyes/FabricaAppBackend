using Aplicacion.Producto;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace FabricaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController: MiControllerBase
    {



        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Producto>>> GetListaProductos()
        {
            return await Mediator.Send(new Consulta.ListaProductos());
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Unit>> PostNuevoProducto(Insertar.NuevoProducto data)
        {
            return await Mediator.Send(data);
        }

        [HttpGet("Unico/{id}")]
        [Authorize]
        public async Task<ActionResult<Dominio.Producto>> GetProductoUnico(int id)
        {
            return await Mediator.Send(new ConsultaID.ProductoUnico { Id = id });
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Unit>> PutEditarProducto(int id, Editar.EditarProducto data)
        {
            data.Id = id;
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Unit>> DeleteEliminarProducto(int id)
        {
            return await Mediator.Send(new Eliminar.eliminarProducto { Id = id });
        }
    }
}
