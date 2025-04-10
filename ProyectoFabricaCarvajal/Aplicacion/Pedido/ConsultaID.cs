using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Pedido
{
    public class ConsultaID
    {
        public class PedidoUnico : IRequest<Dominio.Pedido>
        {
            public int Id { get; set; }
        }

        public class Manejador : IRequestHandler<PedidoUnico, Dominio.Pedido>
        {

            private readonly ArchivoContexto _context;

            public Manejador(ArchivoContexto context)
            {
                _context = context;
            }

            public async Task<Dominio.Pedido> Handle(PedidoUnico request, CancellationToken cancellationToken)
            {
                var pedido = await _context.Pedido.FindAsync(request.Id);

                if (pedido == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { Mensaje = "No se encontro el producto" });
                }

                return pedido;
            }
        }
    }
}
