using Microsoft.AspNetCore.Mvc;
using System.Drawing;

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
                long totalSize = 0;
                var fileInfos = new List<FileInfoResponse>();
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

                        var fileInfo = new FileInfo(fileName);

                        totalSize += fileInfo.Length;

                        var fileInfoResponse = new FileInfoResponse
                        {
                            FileName = formFile.FileName,
                            FilePath = fileName,
                            Size = fileInfo.Length,
                            Extension = fileInfo.Extension.TrimStart('.')
                        };

                        fileInfos.Add(fileInfoResponse);
                    }
                }

                return Ok(new { count = files.Count, totalSize, files = fileInfos });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        public class FileInfoResponse
        {
            public string FileName { get; set; }
            public string FilePath { get; set; }
            public long Size { get; set; }
            public string Extension { get; set; }
        }
    }
}

