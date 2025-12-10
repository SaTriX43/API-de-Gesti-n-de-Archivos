📁 API de Gestión de Archivos — ASP.NET Core 8

API desarrollada en .NET 8 para gestionar archivos de forma segura.
Incluye subida, descarga, validaciones, manejo de streams y listado de archivos guardados.

Este proyecto forma parte de un entrenamiento práctico orientado a Backend .NET Jr.

🚀 Tecnologías utilizadas

ASP.NET Core 8 (Web API)

Serilog (logging)

C# 12

IFormFile + Streams

Patrón Result (éxito/error)

Middleware personalizado de manejo de errores

Swagger / OpenAPI

📌 Funcionalidades principales
✔ Subir archivos (POST /api/archivo/subir-archivo)

Permite subir un archivo mediante multipart/form-data.

Validaciones realizadas:

Extensiones permitidas: .jpg, .png, .pdf, .txt

Tamaño máximo: 5 MB

Rechazo de extensiones peligrosas

El archivo se guarda en la carpeta /Uploads

✔ Descargar archivo (GET /api/archivo/descargar-archivo/{nombre})

Retorna el archivo como stream, con su Content-Type.

Si el archivo no existe → 404

Descarga segura utilizando FileStream

✔ Listar archivos (GET /api/archivo/obtener-nombre-archivos)

Devuelve una lista con todos los nombres de archivos almacenados en /Uploads.

📂 Estructura del proyecto
API de Gestión de Archivos
│── Controllers/
│   └── ArchivoController.cs
│
│── Helper/
│   ├── ArchivoHelper.cs
│   └── IArchivoHelper.cs
│
│── Services/
│   ├── ArchivoService.cs
│   └── IArchivoService.cs
│
│── Middleware/
│   └── ErrorHandlerMiddleware.cs
│
│── Models/
│   └── Result.cs
│
│── Uploads/          → Carpeta donde se almacenan los archivos
│── appsettings.json
│── Program.cs
│── README.md

⚙ Validaciones implementadas
Regla	Estado
Tamaño máximo 5MB	✔ Implementado
Extensiones permitidas	✔ .jpg, .png, .pdf, .txt
Extensiones peligrosas	✔ Rechazadas
Manejo de excepciones E/S	✔ UnauthorizedAccess, IOException, etc
Middleware de errores global	✔ Implementado
Stream seguro para descarga	✔ Implementado
🧩 Diseño de la arquitectura
1️⃣ Controller

Mínima lógica → solo recibe datos, delega al Service y retorna respuestas HTTP.

2️⃣ Service

Reglas de negocio + validaciones.
Utiliza Result Pattern para controlar errores sin excepciones.

3️⃣ Helper

Acceso al sistema de archivos (E/S):

Crear carpetas

Guardar archivos

Abrir streams

Leer nombres

4️⃣ Middleware de errores

Captura excepciones no controladas y devuelve errores unificados en formato JSON.

📝 Ejemplo de respuesta exitosa (subir archivo)
{
  "success": true,
  "nombreArchivo": "a738c21f-3249-4cf1-8b8d-reporte.pdf"
}

🛑 Ejemplo de error de validación
{
  "success": false,
  "error": "Solo se permiten extensiones .jpg, .png, .pdf, .txt"
}

▶ Cómo ejecutar el proyecto

Clonar el repositorio

Abrir en Visual Studio o VS Code

Ejecutar:

dotnet restore
dotnet run


Abrir Swagger:

https://localhost:{puerto}/swagger