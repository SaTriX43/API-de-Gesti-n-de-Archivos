using API_de_Gestión_de_Archivos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_de_Gestión_de_Archivos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivoController : ControllerBase
    {
        private readonly IArchivoService _archivoService;
        public ArchivoController(IArchivoService archivoService) { 
            _archivoService = archivoService;
        }

        [HttpPost("subir-archivo")]
        public async Task<IActionResult> SubirArchivo([FromForm] IFormFile archivo)
        {
            var archivoSubido = await _archivoService.SubirArchivo(archivo);

            if(archivoSubido.IsFailure)
            {
                return BadRequest(new
                {
                    success = false,
                    error = archivoSubido.Error
                });
            }

            return Created("",new
            {
                success = true,
                nombreArchivo = archivoSubido.Value
            });
        }
    }
}
