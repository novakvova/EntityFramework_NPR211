using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace BAL.Utilities
{
    internal class UploadDTO
    {
        public string Photo { get; set; }
    }
    internal class UploadResponseDTO
    {
        public string Image { get; set; }
    }
    public static class PhotoSaver
    {
        public static string UploadImage(byte[] imageBytes)
        {
            string base64 = Convert.ToBase64String(imageBytes);
            string server = "https://solido.tk";
            UploadDTO upload = new UploadDTO();
            upload.Photo = base64;
            string json = JsonConvert.SerializeObject(upload);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            WebRequest request = WebRequest.Create($"{server}/api/account/upload");
            request.Method = "POST";
            request.ContentType = "application/json";
            using (var stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }
            try
            {
                var response = request.GetResponse();
                using (var stream = new StreamReader(response.GetResponseStream()))
                {
                    string data = stream.ReadToEnd();
                    var resp = JsonConvert.DeserializeObject<UploadResponseDTO>(data);
                    return $"{server}{resp.Image}";
                }
            }
            catch (Exception) {  }



            return null;
        }
    }
}
