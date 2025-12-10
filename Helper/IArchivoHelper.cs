using API_de_Gestión_de_Archivos.Models;

namespace API_de_Gestión_de_Archivos.Helper
{
    public interface IArchivoHelper
    {
        public Result<List<string>> ObtenerNombreArchivos();
        public Task<Result<(Stream, string tipoContenido)>> DescargarArchivo(string nombreArchivo);
        public Task<Result<string>> SubirArchivo(IFormFile archivo);
    }
}
