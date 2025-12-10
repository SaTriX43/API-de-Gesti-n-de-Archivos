using API_de_Gestión_de_Archivos.Helper;
using API_de_Gestión_de_Archivos.Models;
using System.Net.NetworkInformation;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_de_Gestión_de_Archivos.Services
{
    public class ArchivoService : IArchivoService
    {
        private readonly IArchivoHelper _archivoHelper;

        public ArchivoService(IArchivoHelper archivoHelper)
        {
            _archivoHelper = archivoHelper;
        }

        public Result<List<string>> ObtenerNombreArchivos()
        {
            var nombreArchivos = _archivoHelper.ObtenerNombreArchivos();
            return nombreArchivos;
        }
        public async Task<Result<(Stream,string tipoContenido)>> DescargarArchivo(string nombreArchivo)
        {
            return await _archivoHelper.DescargarArchivo(nombreArchivo);
        }
        public async Task<Result<string>> SubirArchivo(IFormFile archivo)
        {
            var rutaArchivo = Path.GetExtension(archivo.FileName).ToLower();

            var extensionesPermitida = new List<string>
            {
                ".jpg",
                ".png",
                ".pdf",
                ".txt"
            };

            if(!extensionesPermitida.Contains(rutaArchivo))
            {
                return Result<string>.Failure($"Solo se permiten extensiones de tipo jpg, .png, .pdf, .txt");
            }

            long maxFileSizeBytes = 5 * 1024 * 1024;

            if (archivo.Length > maxFileSizeBytes)
            {
                return Result<string>.Failure("$Su archivo debe de pesar menos de 5MB");
            }

            var archivoGuardado = await _archivoHelper.SubirArchivo(archivo);

            if(archivoGuardado.IsFailure)
            {
                return Result<string>.Failure(archivoGuardado.Error);
            }

            return Result<string>.Success(archivoGuardado.Value);
        }
    }
}
