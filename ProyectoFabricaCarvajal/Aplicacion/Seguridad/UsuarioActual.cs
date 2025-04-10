using Aplicacion.Interfaces;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class UsuarioActual
    {
        public class EjecutarUsuarioActual: IRequest<UsuarioData>{ }

        public class Manejador : IRequestHandler<EjecutarUsuarioActual, UsuarioData>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerador _jwtgenerador;
            private readonly IUsuarioSesion _usuarioSesion;
            public Manejador(UserManager<Usuario> userManager, IJwtGenerador jwtGenerador, IUsuarioSesion usuarioSesion)
            {
                _userManager = userManager;
                _jwtgenerador = jwtGenerador;
                _usuarioSesion = usuarioSesion;
            }
            public async Task<UsuarioData> Handle(EjecutarUsuarioActual request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());

                return new UsuarioData
                {
                    UserName = usuario.UserName,
                    Email = usuario.Email,
                    Token = _jwtgenerador.CrearToken(usuario)
                };
            }
        }
    }
}
