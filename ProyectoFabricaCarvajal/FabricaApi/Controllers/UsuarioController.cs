using Aplicacion.Seguridad;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace FabricaApi.Controllers
{
    public class UsuarioController : MiControllerBase
    {
        [HttpPost("Registrar")]
        public async Task<ActionResult<UsuarioData>> Registrar(Insertar.NuevoUsuario parametros)
        {
            return await Mediator.Send(parametros);
        }


        [HttpPost("login")]
        public async Task<ActionResult<UsuarioData>> Login(Login.EjecutaLogin parametros)
        {
            return await Mediator.Send(parametros);
        }

        [HttpGet]
        public async Task<ActionResult<UsuarioData>> DevolverUsuario()
        {
            return await Mediator.Send(new UsuarioActual.EjecutarUsuarioActual());
        }
    }
}
