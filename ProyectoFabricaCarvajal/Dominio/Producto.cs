using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Producto
    {
        [Key]
        public int Id{ get; set; }
        public string? ProdDesc { get; set; }
        public float? ProValor { get; set; }
        //public IEnumerable<Pedido> Pedidos { get; set; }
        public ICollection<Pedido>? PedidoListaProducto { get; set; }
        /*public Producto()
        {
            Id = Guid.NewGuid().ToString();
        }*/




    }
}
