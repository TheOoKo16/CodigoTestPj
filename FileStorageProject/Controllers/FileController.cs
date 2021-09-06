using Data.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
//using System.Web.Http;

namespace WebApi.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private IWebHostEnvironment _hostEnvironment;

        public FileController(IWebHostEnvironment environment)
        {
            _hostEnvironment = environment;
        }

        [HttpPost("upload")]
        public async Task<ActionResult> upload(FileUploadViewModel fvm)
        {
            string path = Path.Combine(_hostEnvironment.WebRootPath, fvm.filepath);
            //String path = HttpContext.Current.Server.MapPath(fvm.filepath);
            return Ok();
        }

            //public HttpResponseMessage upload(HttpRequestMessage request, FileUploadViewModel fvm)
            //{

            //    String result;

            // String path = HttpContext.Current.Server.MapPath(fvm.filepath); //Path          
            //    //Check if directory exist
            //    if (!System.IO.Directory.Exists(path))
            //    {
            //        System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
            //    }

            //    if (fvm.photo != null)
            //    {
            //        result = PhotoUploadHelper.uploadPhoto(fvm.photo, path, fvm.uploadtype);
            //    }
            //    else
            //    {
            //        result = null;
            //    }



            //    return request.CreateResponse<string>(HttpStatusCode.OK, result);
            //}
        }
}
