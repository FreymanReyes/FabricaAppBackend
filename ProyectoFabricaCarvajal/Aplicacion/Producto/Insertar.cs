using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Aplicacion.Producto
{
    public class Insertar
    {
        public class NuevoProducto: IRequest
        {
            [Required(ErrorMessage = "La descripcion es obligatoria")]
            public string? Descripcion { get; set; }
            [Required(ErrorMessage = "precio es obligatorio")]
            public float? Precio { get; set; }
        }

        public class Manejador : IRequestHandler<NuevoProducto>
        {
            private readonly ArchivoContexto _context;
            public Manejador(ArchivoContexto context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(NuevoProducto request, CancellationToken cancellationToken)
            {
                var producto = new Dominio.Producto
                {
                    ProdDesc = request.Descripcion,
                    ProValor = request.Precio,
                };

                _context.Producto.Add(producto);
                var valor = await _context.SaveChangesAsync();
                if(valor > 0)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.OK);
                }
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { Mensaje = "No se pudo almacenar el Producto" });
            }
        }
    }
}
