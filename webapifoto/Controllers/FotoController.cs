using Microsoft.AspNetCore.Mvc;
using webapifoto.Models; 

namespace webapifoto.Controllers
{
    [Route("api/foto")]
    [ApiController]
    public class FotoController : Controller
    {
        public static IWebHostEnvironment _webHostEnviroment;

        public FotoController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnviroment = webHostEnvironment;
        }

        [HttpPost]

        public async Task<ActionResult<foto>> PostIndex([FromForm] foto foto)
        {
            try
            {
                string webRootPath = _webHostEnviroment.WebRootPath;
                string rutaArchivos = Path.Combine(webRootPath, "files");

                if(foto.Archivo.Length > 0)
                {
                    if (!Directory.Exists(rutaArchivos))
                    {
                        Directory.CreateDirectory(rutaArchivos);
                    }
                    using (FileStream filestream = System.IO.File.Create(Path.Combine(rutaArchivos, foto.Archivo.FileName)))
                    {
                        await foto.Archivo.CopyToAsync(filestream);
                        filestream.Flush();
                    }
                    foto.Url = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/files/" + foto.Archivo.FileName;
                }
            }
            catch(Exception ex)
            {
                return Problem(ex.Message + _webHostEnviroment.WebRootPath);
            }
            return CreatedAtAction(nameof(PostIndex), new { foto.Nombre, foto.Url });
        }
    }
}
