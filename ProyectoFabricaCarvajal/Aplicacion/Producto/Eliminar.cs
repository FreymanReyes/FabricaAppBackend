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

namespace Aplicacion.Producto
{
    public class Eliminar
    {
        public class eliminarProducto : IRequest
        {
            public int Id { get; set; }
        }

        public class Manejador : IRequestHandler<eliminarProducto>
        {

            private readonly ArchivoContexto _context;

            public Manejador(ArchivoContexto context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(eliminarProducto request, CancellationToken cancellationToken)
            {
                var pedido_producto = await _context.Pedido.Where(x => x.ProductoId == request.Id).ToListAsync();
                var pedido_producto_count = pedido_producto.Count();
                if (pedido_producto_count > 0)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { Mensaje = "No se puede eliminar el producto porque pertenece a un pedido" });
                }

                var producto = await _context.Producto.FindAsync(request.Id);


                if (producto == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { Mensaje = "No se encontro el producto" });
                }
                 _context.Remove(producto);

                await _context.SaveChangesAsync();

                throw new ManejadorExcepcion(HttpStatusCode.OK, new { mensaje = "Producto Eliminado" });
            }
        }
    }
}
