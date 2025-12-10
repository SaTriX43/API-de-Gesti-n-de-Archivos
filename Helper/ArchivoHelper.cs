using API_de_Gestión_de_Archivos.Models;

namespace API_de_Gestión_de_Archivos.Helper
{
    public class ArchivoHelper : IArchivoHelper
    {
        private readonly IWebHostEnvironment _environment;

        public ArchivoHelper(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public Result<List<string>> ObtenerNombreArchivos()
        {
            var rutaCarpetaCompleta = Path.Combine(_environment.ContentRootPath, "Uploads");
            if(!Directory.Exists(rutaCarpetaCompleta))
            {
                Directory.CreateDirectory(rutaCarpetaCompleta);
                return Result<List<string>>.Success(new List<string>());
            }

            var archivosRutaCompleta = Directory.GetFiles(rutaCarpetaCompleta);

            var nombres = archivosRutaCompleta.Select(r => Path.GetFileName(r)).ToList();

            return Result<List<string>>.Success(nombres);

        }
        public async Task<Result<(Stream, string tipoContenido)>> DescargarArchivo(string nombreArchivo)
        {
            var rutaCarpetaCompleta = Path.Combine(_environment.ContentRootPath,"Uploads");
            var rutaArchivoCompleta = Path.Combine(rutaCarpetaCompleta, nombreArchivo);

            if(!File.Exists(rutaArchivoCompleta))
            {
                return Result<(Stream, string tipoContenido)>.Failure($"El archivo con nombre {nombreArchivo} no existe");
            }

            var stream = new FileStream(rutaArchivoCompleta, FileMode.Open, FileAccess.Read);
            var contentType = "application/octet-stream";

            return Result<(Stream, string tipoContenido)>.Success((stream,contentType));
        }
        public async Task<Result<string>> SubirArchivo(IFormFile archivo)
        {
            try
            {

                var rutaCarpetaCompleta = Path.Combine(_environment.ContentRootPath, "Uploads");

                if(!Directory.Exists(rutaCarpetaCompleta))
                {
                    Directory.CreateDirectory(rutaCarpetaCompleta);
                }

                var archivoNombreAleatorio = Guid.NewGuid().ToString() + archivo.FileName;
                var rutaCompletaArchivo = Path.Combine(rutaCarpetaCompleta, archivoNombreAleatorio);

                using(var stream = new FileStream(rutaCompletaArchivo,FileMode.Create))
                {
                    await archivo.CopyToAsync(stream);
                }

                return Result<string>.Success(archivoNombreAleatorio);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Result<string>.Failure("No tienes permisos para guardar archivos en el servidor.");
            }
            catch (DirectoryNotFoundException ex)
            {
                return Result<string>.Failure("La carpeta de destino no existe y no pudo ser creada.");
            }
            catch (IOException ex)
            {
                return Result<string>.Failure("Ocurrió un error de E/S al guardar el archivo.");
            }
            catch (Exception ex)
            {
                // Error inesperado → lo capturará también el middleware
                return Result<string>.Failure("Error inesperado al guardar el archivo.");
            }
        }
    }
}
