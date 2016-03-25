using System.Web;
using System.Web.Mvc;
using System.IO;
using Framework.Library.Web.Captcha;

namespace Framework.Library.Mvc.Captcha
{
    public class CaptchaResult
    {
        // default buffer size as defined in BufferedStream type 
        private const int BufferSize = 0x1000;

        private readonly ICaptchaImage image;
        private readonly int quality;
        private readonly int width;
        private readonly int height;

        public CaptchaResult(ICaptchaImage image, int quality, int width, int height)
        {
            this.image = image;
            this.quality = quality;
            this.width = width;
            this.height = height;
        }

        public void ExecuteResult(HttpResponseBase response)
        {
            response.ContentType = "image/jpeg";
            var ms = new MemoryStream();
            image.SaveImageToStream(ms, quality, width, height);
            ms.Position = 0;

            // grab chunks of data and write to the output stream 
            var outputStream = response.OutputStream;
            using (ms)
            {
                var buffer = new byte[BufferSize];
                while (true)
                {
                    int bytesRead = ms.Read(buffer, 0, BufferSize);
                    if (bytesRead == 0)
                    {
                        // no more data 
                        break;
                    }
                    outputStream.Write(buffer, 0, bytesRead);
                }
            }
        }
    }
}
