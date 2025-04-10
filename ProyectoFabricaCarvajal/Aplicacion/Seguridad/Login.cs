using Aplicacion.Interfaces;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Aplicacion.Seguridad
{
    public class Login
    {
        public class EjecutaLogin: IRequest<UsuarioData>
        {
            [Required]
            public string? Username { get; set; }
            [Required]
            public string? Upassword { get; set; }
        }



        
        public class Manejador : IRequestHandler<EjecutaLogin, UsuarioData>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly SignInManager<Usuario> _signInManager;
            private readonly IJwtGenerador _jwtGenerador;
            public Manejador(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IJwtGenerador jwtGenerador)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtGenerador = jwtGenerador;
            }
            public async Task<UsuarioData> Handle(EjecutaLogin request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByNameAsync(request.Username);

                if (usuario == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);
                }

                var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, request.Upassword, false);

                if (resultado.Succeeded)
                {
                    return new UsuarioData
                    {
                        Id = usuario.Id,
                        UserName = usuario.UserName,
                        Email = usuario.Email,
                        Token = _jwtGenerador.CrearToken(usuario)
                    };
                }

                throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);
            }
        }
    }
}
