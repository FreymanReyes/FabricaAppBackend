using Dominio;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;
using Aplicacion.Pedido;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace FabricaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : MiControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Pedido>>> GetListaPedidos()
        {
            return await Mediator.Send(new Consulta.ListaPedidos());
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Unit>> PostNuevoPedido(Insertar.NuevoPedido data)
        {
            return await Mediator.Send(data);
        }

        [HttpGet("Unico/{id}")]
        [Authorize]
        public async Task<ActionResult<Dominio.Pedido>> GetPedidoUnico(int id)
        {
            return await Mediator.Send(new ConsultaID.PedidoUnico { Id = id });
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Unit>> PutEditarPedido(int id, Editar.EditarPedido data)
        {
            data.ID = id;
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Unit>> DeleteEliminarPedido(int id)
        {
            return await Mediator.Send(new Eliminar.eliminarPedido { Id = id });
        }
    }
}
