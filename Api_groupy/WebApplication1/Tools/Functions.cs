using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebApplication1.Models;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.S3.Model;
using System.Configuration;
using System.Net;

namespace WebApplication1.Tools
{
    public static class Functions
    {

        public static DateTime? GetDateFromUrl(string queryString, string key)
        {
            string initialDate = HttpUtility.ParseQueryString(queryString).Get(key);
            if (!string.IsNullOrEmpty(initialDate))
            {
                return DateTime.Parse(initialDate);
            }
            return null;
        }
        public static string GetStringFromUrl(string queryString, string key)
        {
            return HttpUtility.ParseQueryString(queryString).Get(key);            
        }
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

        public static async Task<UploadResponseModel> UploadImage(string code , string fileBytesString, string subPath = null)
        {
            string BUCKET_NAME = "try-site";
            string AWS_ID = "AKIA3HN4SL75XZ7EOPOJ";
            string AWS_SECRET = "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";
            bool fileWasCreated = false;


            UploadResponseModel uploadResponse = new();
            string filePath = null;
            try
            {
                var fileName = $"{code}-{DateTime.UtcNow:yyyy-MM-dd-HH-mm-ss}.jpg";

                UploadPartResponse response = null;

                var fileBytes = Convert.FromBase64String(fileBytesString);

                filePath = Path.Combine("./", fileName);
                File.WriteAllBytes(filePath, fileBytes);
                fileWasCreated = true;

                var finalFileName = fileName;

                if (!String.IsNullOrWhiteSpace(subPath))
                    finalFileName = $"{subPath}/{fileName}";

                var request = new UploadPartRequest
                {
                    BucketName = BUCKET_NAME,
                    Key = finalFileName,
                    FilePath = filePath,
                };
                var Client = new AmazonS3Client(AWS_ID, AWS_SECRET, RegionEndpoint.USEast1);
                response = await Client.UploadPartAsync(request);

                uploadResponse.StatusCode = response.HttpStatusCode;
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    uploadResponse.Message = response.ETag;
                    uploadResponse.PathToFile = $"https://try-site.s3.us-east-1.amazonaws.com/{finalFileName}";
                    uploadResponse.FileName = finalFileName;
                    return uploadResponse;
                }
                else
                {
                    uploadResponse.Message = "Ocorreu um erro ao processar o upload da imagem";
                    return uploadResponse;
                }
            }
            catch (Exception e)
            {
                uploadResponse.Message = e.Message;
                return uploadResponse;
            }
            finally
            {
                if (fileWasCreated)
                    File.Delete(filePath);
            }
        }

    }
    public class UploadResponseModel
    {
        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string PathToFile { get; set; }

        public string FileName {get;set;}
    }

}
