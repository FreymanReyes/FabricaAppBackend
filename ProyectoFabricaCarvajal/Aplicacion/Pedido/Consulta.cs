using MediatR;
using Persistencia;
using Microsoft.EntityFrameworkCore;


namespace Aplicacion.Pedido
{
    public class Consulta
    {
        public class ListaPedidos : IRequest<List<Dominio.Pedido>> { }

        public class Manejador : IRequestHandler<ListaPedidos, List<Dominio.Pedido>>
        {
            private readonly ArchivoContexto _context;

            public Manejador(ArchivoContexto context)
            {
                _context = context;
            }

            public async Task<List<Dominio.Pedido>> Handle(ListaPedidos request, CancellationToken cancellationToken)
            {
                var pedidos = await _context.Pedido
                    .Include(x => x.Usuario.PedidoListaUsuario)
                    .Include(x => x.Producto.PedidoListaProducto)
                    .ToListAsync();

                return pedidos;
            }
        }
    }
}
