using Microsoft.AspNetCore.Mvc;

namespace AdminPagosApi.Controllers
{
    [ApiController]
    [Route("AdmonPago/Api/Archivo")]
    public class ArchivoController : ControllerBase
	{
        private readonly IWebHostEnvironment _environment;

        public ArchivoController(IWebHostEnvironment env)
        {
            _environment = env;
        }


        [HttpPost]
        public async Task<ActionResult> Post(List<IFormFile> files)
        {
            try
            {
            
                long size = files.Sum(f => f.Length);

                foreach (var formFile in files)
                {

                  

                    if (formFile.Length > 0)
                    {
                        var fileName = Path.Combine(_environment.ContentRootPath, "uploads", formFile.FileName);

                        using (var stream = System.IO.File.Create(fileName))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }

                return Ok(new { count = files.Count, size });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
    }
}

