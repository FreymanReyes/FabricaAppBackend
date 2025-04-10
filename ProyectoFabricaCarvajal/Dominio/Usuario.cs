using Microsoft.AspNetCore.Identity;

namespace Dominio
{
    public class Usuario: IdentityUser
    {
        //public IEnumerable<Pedido> PedidosUsers { get; set; }
        //public IEnumerable<Pedido> Pedidos { get; set; }
        public ICollection<Pedido>? PedidoListaUsuario { get; set; }
    }
}
