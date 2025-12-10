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

        [HttpGet("obtener-nombre-archivos")]
        public IActionResult ObtenerNombreArchivos()
        {
            var nombreArchivo =  _archivoService.ObtenerNombreArchivos();
            return Ok(new
            {
                success = true,
                nombreArchivos = nombreArchivo.Value
            });
        }

        [HttpGet("descargar-archivo/{nombreArchivo}")]
        public async Task<IActionResult> DescargarArchivo(string nombreArchivo)
        {
            if (nombreArchivo == null || nombreArchivo.Trim() == "") {
                return BadRequest(new
                {
                    success = false,
                    error = "El nombre del archivo no puede ser null o estar vacio"
                });
            }

            var archivoDescargado = await _archivoService.DescargarArchivo(nombreArchivo);

            if (archivoDescargado.IsFailure) {
                return NotFound(new
                {
                    success = false,
                    error = archivoDescargado.Error
                });
            }

            var (stream, contentType) = archivoDescargado.Value;


            return File(stream, contentType, nombreArchivo);
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
