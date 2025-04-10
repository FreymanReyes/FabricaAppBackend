using System.ComponentModel.DataAnnotations;
using System.Net;
using Aplicacion.Interfaces;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad
{
    public class Insertar
    {
        public class NuevoUsuario : IRequest<UsuarioData>
        {
            [Required(ErrorMessage = "El Nombre de usuario es obligatorio")]
            public string? UserName { get; set; }
            [Required(ErrorMessage = "el correo electronico es obligatoria")]
            public string? Email { get; set; }
            [Required(ErrorMessage = "el password es obligatorio")]
            public string? Password { get; set; }
        }


        public class Manejador : IRequestHandler<NuevoUsuario, UsuarioData>
        {
            private readonly ArchivoContexto _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerador _jwtGenerador;
            public Manejador(ArchivoContexto context, UserManager<Usuario> userManager, IJwtGenerador jwtGenerador)
            {
                _context = context;
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
            }
            public async Task<UsuarioData> Handle(NuevoUsuario request, CancellationToken cancellationToken)
            {
                var existe = await _context.Users.Where(u => u.Email == request.Email || u.UserName == request.UserName).AnyAsync();

                if (existe)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "el email o el nombre de usuario ya existe" });
                }

                var usuario = new Usuario
                {
                    Email = request.Email,
                    UserName = request.UserName
                };

                var resultado = await _userManager.CreateAsync(usuario, request.Password);

                if (resultado.Succeeded)
                {
                    return new UsuarioData
                    {
                        Id = usuario.Id,
                        UserName = usuario.UserName,
                        Email = usuario.Email,
                        Token = _jwtGenerador.CrearToken(usuario),
                    };
                }
                throw new Exception("No se creo el usuario");
            }
        }
    }
}