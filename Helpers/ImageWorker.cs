using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace Helpers
{
    public static class ImageWorker
    {
        /// <summary>
        /// Збереження фото в папку по url
        /// </summary>
        /// <param name="url">Адреса фото в мережі</param>
        /// <returns>Повертаємо назву фото</returns>
        public static string? ImageSaveUrl(string url, string folderName, string? imageName = null)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        //список байт фото
                        byte[] imageBytes = response.Content.ReadAsByteArrayAsync().Result;
                        return SaveBytesImage(imageBytes, folderName, imageName);
                    }
                    else
                    {
                        Console.WriteLine("Запит по фото пройшов із проблемой {0}", response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка збереження фото {0}", ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Збереження фото в папку по path
        /// </summary>
        /// <param name="path">Адреса фото на диску</param>
        /// <returns>Повертаємо назву фото</returns>
        public static string? ImageSaveFile(string path, string folderName, string? imageName = null)
        {
            try
            {
                byte[] imageBytes = File.ReadAllBytes(path);
                return SaveBytesImage(imageBytes, folderName, imageName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка збереження фото {0}", ex.Message);
                return null;
            }

        }

        private static string SaveBytesImage(byte[] imageBytes, string folderName, string? imageName = null)
        {
            int[] sizes = [50, 150, 300, 600, 1200]; //розмір фото, яке буде зберігатися
                                                     //Рандомна величина, яка не може повторится при генерації
            string fileName = imageName ?? Guid.NewGuid().ToString() + ".webp";
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "images", folderName);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            foreach (var size in sizes)
            {
                var outPath = Path.Combine(dir, size + "_" + fileName);
                using (var image = Image.Load(imageBytes))
                {
                    image.Mutate(x =>
                    {
                        x.Resize(new ResizeOptions
                        {
                            Size = new Size(size, size),
                            Mode = ResizeMode.Max
                        });
                    });
                    using (var ms = new MemoryStream())
                    {
                        image.Save(ms, new WebpEncoder());
                        var bytesOut = ms.ToArray();
                        File.WriteAllBytes(outPath, bytesOut);
                    }
                }
            }
            return fileName;
        }

        public static Stream GetFileStream(string path)
        {
            using (var image = Image.Load(path))
            {
                var ms = new MemoryStream();
                image.Save(ms, new JpegEncoder());
                return ms;
            }
        }
    }
}
