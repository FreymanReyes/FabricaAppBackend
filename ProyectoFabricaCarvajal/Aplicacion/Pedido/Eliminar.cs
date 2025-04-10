using Aplicacion.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Pedido
{
    public class Eliminar
    {
        public class eliminarPedido : IRequest
        {
            public int Id { get; set; }
        }

        public class Manejador : IRequestHandler<eliminarPedido>
        {

            private readonly ArchivoContexto _context;

            public Manejador(ArchivoContexto context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(eliminarPedido request, CancellationToken cancellationToken)
            {
                var pedido = await _context.Pedido.FindAsync(request.Id);

                

                if (pedido == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { Mensaje = "No se encontro el pedido" });
                }
                _context.Remove(pedido);

                await _context.SaveChangesAsync();

                throw new ManejadorExcepcion(HttpStatusCode.OK, new { mensaje = "Pedido Eliminado" });
            }
        }
    }
}
