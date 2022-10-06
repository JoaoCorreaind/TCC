using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Tools
{
    public static class Functions
    {
        //Função que salva uma imagem no servidor e retorna o path e nome
        public async static Task<ImageModel> SaveImageInDisk(IFormFile file, string webPath)
        {
            try
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                string combine = fileName + DateTime.Now.ToString("fffssmmyy");
                string path = Path.Combine(webPath + "/Image/", combine + extension );

                using (Stream fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return new ImageModel
                {
                    Name = fileName,
                    Path = "Image/" + combine + extension,
                };
            }
            catch (Exception e)
            {

                throw e;
            }            
        }

        public static string RemoveAccents(this string str)
        {
            return new string(str
                .Normalize(NormalizationForm.FormD)
                .Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                .ToArray());
        }
    }

}
