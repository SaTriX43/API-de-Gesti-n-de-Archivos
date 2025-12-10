using API_de_Gestión_de_Archivos.Models;

namespace API_de_Gestión_de_Archivos.Services
{
    public interface IArchivoService
    {
        public Task<Result<string>> SubirArchivo(IFormFile archivo);
    }
}
