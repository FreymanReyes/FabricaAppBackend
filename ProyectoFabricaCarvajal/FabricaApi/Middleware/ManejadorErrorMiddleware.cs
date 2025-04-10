using System.Net;
using Aplicacion.ManejadorError;
using Newtonsoft.Json;

namespace WebAPI.Middleware
{
    public class ManejadorErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ManejadorErrorMiddleware> _logger;
        public ManejadorErrorMiddleware(RequestDelegate next, ILogger<ManejadorErrorMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ManejadorExcepcionAsincrona(context, ex, _logger);
            }
        }
        private async Task ManejadorExcepcionAsincrona(HttpContext context, Exception ex, ILogger<ManejadorErrorMiddleware> logger)
        {
            object? errores = null;
            switch (ex)
            {
                case ManejadorExcepcion me:
                    logger.LogError(ex, "Manejador Error");
                    errores = me._Errores;
                    context.Response.StatusCode = (int)me._Codigo;
                    break;
                case Exception e:
                    logger.LogError(ex, "Error De Servidor");
                    errores = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            context.Response.ContentType = "application/json";
            if (errores != null)
            {
                var resultados = JsonConvert.SerializeObject(new { errores });
                await context.Response.WriteAsync(resultados);
            }
        }
    }
}