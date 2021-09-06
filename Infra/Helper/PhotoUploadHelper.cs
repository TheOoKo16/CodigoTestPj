using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.ViewModels;
namespace Infra.Helper
{
   public class PhotoUploadHelper
    {
        public static ImageNameViewModel uploadPhoto(string stringInBase64, string path)
        {
            try
            {
                ImageNameViewModel result = new ImageNameViewModel();
                string imageName = Guid.NewGuid().ToString() + ".jpg";
                result.ImageName = imageName;
                //set the image path
                string imgPath = Path.Combine(path, imageName);
                List<string> bs64List = stringInBase64.Split(',').ToList();
                if (bs64List.Count() > 0)
                {
                    stringInBase64 = bs64List[1];
                }
                byte[] imageBytes = Convert.FromBase64String(stringInBase64);
                System.IO.File.WriteAllBytes(imgPath, imageBytes);

                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}
