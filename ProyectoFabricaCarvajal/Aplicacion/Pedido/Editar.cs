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

namespace Aplicacion.Pedido
{
    public class Editar
    {
        public class EditarPedido : IRequest
        {
            public int ID { get; set; }
            public int productoID { get; set; }
            public float cantidad { get; set; }
            public string usuarioID { get; set; }
        }
        public class Manejador : IRequestHandler<EditarPedido>
        {
            private readonly ArchivoContexto _context;
            public Manejador(ArchivoContexto context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(EditarPedido request, CancellationToken cancellationToken)
            {
                var usuario = _context.Users.Find(request.usuarioID);
                if (usuario == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Usuario No Enconrado" });
                }

                var producto = _context.Producto.Find(request.productoID);
                if (producto == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Producto No Enconrado" });
                }
                var pedido = await _context.Pedido.FindAsync(request.ID);
                if (pedido == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { Mensaje = "No se encontro el pedido" });
                }

                if (request.cantidad == pedido.PedCant)
                {
                    pedido.PedCant = pedido.PedCant;
                }
                else
                {
                    pedido.PedCant = request.cantidad;
                }

                var subtotal = producto.ProValor * pedido.PedCant;

                pedido.Producto = producto ?? pedido.Producto;
                pedido.Usuario = usuario ?? pedido.Usuario;
                pedido.PedVrUnit = (float)producto.ProValor;
                pedido.PedSubtot = (float)subtotal;
                pedido.PedIVA = (int)Math.Round((float)(subtotal * 0.19));
                pedido.PedTotal = (int)Math.Round((float)(subtotal * 1.19));

                var valor = await _context.SaveChangesAsync();

                if (valor > 0)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.OK, new { Mensaje = "Pedido Editado" });
                }
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { Mensaje = "No se pudo editar el pedido" });

            }
        }
    }
}
