using System.ComponentModel.DataAnnotations;


namespace Dominio
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }


        public string UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int ProductoId { get; set; }
        public Producto? Producto { get; set; }


        public float PedVrUnit { get; set; }
        public float PedCant { get; set; }
        public float PedSubtot { get; set; }
        public float PedIVA { get; set; }
        public float PedTotal{ get; set; }

    }
}
