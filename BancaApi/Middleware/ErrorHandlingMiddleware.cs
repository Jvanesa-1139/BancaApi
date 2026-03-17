namespace BancaApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;  

        public ErrorHandlingMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;  
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var error = new
                {
                    mensaje = "Ocurrio un error interno. Intente mas tarde.",
                    detalle = _env.IsDevelopment() ? ex.Message : null  
                };

                await context.Response.WriteAsJsonAsync(error);
            }
        }
    }
}