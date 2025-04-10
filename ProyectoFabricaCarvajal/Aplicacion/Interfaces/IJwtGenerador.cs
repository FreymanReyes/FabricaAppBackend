using Dominio;

namespace Aplicacion.Interfaces
{
    public interface IJwtGenerador
    {
        string CrearToken(Usuario usuario);
    }
}
