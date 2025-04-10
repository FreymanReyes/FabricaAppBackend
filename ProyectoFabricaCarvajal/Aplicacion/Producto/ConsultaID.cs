using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Producto
{
    public class ConsultaID
    {
        public class ProductoUnico : IRequest<Dominio.Producto>
        {
            public int Id { get; set; }
        }

        public class Manejador : IRequestHandler<ProductoUnico, Dominio.Producto>
        {

            private readonly ArchivoContexto _context;

            public Manejador(ArchivoContexto context)
            {
                _context = context;
            }

            public async Task<Dominio.Producto> Handle(ProductoUnico request, CancellationToken cancellationToken)
            {
                var producto = await _context.Producto.FindAsync(request.Id);
                if (producto == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { Mensaje = "No se encontro el pedido" });
                }
                return producto;
            }
        }
    }
}