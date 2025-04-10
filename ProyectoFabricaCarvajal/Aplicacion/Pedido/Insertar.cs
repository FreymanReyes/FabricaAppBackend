using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using System.Net;

namespace Aplicacion.Pedido
{
    public class Insertar
    {
        public class NuevoPedido : IRequest
        {
            public int productoID { get; set; }
            public float cantidad { get; set; }
            public string usuarioID { get; set; }

        }
       
        public class Manejador : IRequestHandler<NuevoPedido>
        {
            private readonly ArchivoContexto _context;
            public Manejador(ArchivoContexto context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(NuevoPedido request, CancellationToken cancellationToken)
            {
                

                var usuario = _context.Users.Find(request.usuarioID);
                if (usuario==null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Usuario No Enconrado" });
                }
                var producto = _context.Producto.Find(request.productoID);
                if (producto == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Producto No Enconrado" });
                }

                var subtotal = producto.ProValor * request.cantidad;

                var pedido = new Dominio.Pedido
                {
                    Usuario=usuario,
                    Producto=producto,
                    PedVrUnit = (float)producto.ProValor,
                    PedCant = request.cantidad,
                    PedSubtot = (float)subtotal,
                    PedIVA = (int)Math.Round((float)(subtotal * 0.19)),
                    PedTotal = (int)Math.Round((float)(subtotal * 1.19))
                };

                _context.Pedido.Add(pedido);



                int valor = _context.SaveChanges();

                if (valor > 0)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.OK, new { mensaje = "Pedido Almacenado" });
                }

                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No se pudo almacenar el pedido" });
            }
        }
    }
}
