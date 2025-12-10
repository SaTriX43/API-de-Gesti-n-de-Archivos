using API_de_Gestión_de_Archivos.Models;

namespace API_de_Gestión_de_Archivos.Helper
{
    public interface IArchivoHelper
    {
        public Task<Result<string>> SubirArchivo(IFormFile archivo);
    }
}
