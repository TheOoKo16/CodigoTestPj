using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Helper
{
   public class FileUploadApiRequestHelper
    {
        public static async Task<ImageNameViewModel> upload(FileUploadViewModel fvm)
        {
            string url = string.Format("api/file/upload");
            return await ApiRequest<FileUploadViewModel, ImageNameViewModel>.PostDiffRequest(url, fvm);
        }
    }
}
