using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Producto
{
    public class Consulta
    {
        public class ListaProductos : IRequest<List<Dominio.Producto>> { }
        
        public class Manejador : IRequestHandler<ListaProductos, List<Dominio.Producto>>
        {
            private readonly ArchivoContexto _context;

            public Manejador(ArchivoContexto context)
            {
                _context = context;
            }

            public async Task<List<Dominio.Producto>> Handle(ListaProductos request, CancellationToken cancellationToken)
            {
                var productos = await _context.Producto.ToListAsync();
                return productos;
            }
        }
    }
}
