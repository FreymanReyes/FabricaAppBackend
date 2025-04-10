using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Producto
{
    public class Editar
    {
        public class EditarProducto: IRequest
        {
            public int Id { get; set; }
            [Required]
            public string? Descripcion { get; set; }
            [Required]
            public float? Precio { get; set; }
        }

        public class Manejador : IRequestHandler<EditarProducto>
        {
            private readonly ArchivoContexto _context;
            public Manejador(ArchivoContexto context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(EditarProducto request, CancellationToken cancellationToken)
            {
                var producto = await _context.Producto.FindAsync(request.Id);
                if (producto == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { Mensaje = "No se encontro el producto" });
                }

                producto.ProdDesc = request.Descripcion ?? producto.ProdDesc;
                producto.ProValor = request.Precio ?? producto.ProValor;

                var valor = await _context.SaveChangesAsync();
                if (valor>0)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.OK, new { Mensaje = "Producto Almacenado" });
                    //return Unit.Value;
                }
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { Mensaje = "No se pudo editar el producto" });
            }
        }
    }
}
