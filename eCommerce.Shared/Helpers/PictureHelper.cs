using eCommerce.Shared.Commons;
using eCommerce.Shared.Extensions;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace eCommerce.Shared.Helpers
{
    public class PictureHelper
    {
        private readonly string _webRootPath;

        public PictureHelper(string webRootPath)
        {
            _webRootPath = webRootPath;
        }

        public string Thumbnail(int pictureID, string pictureURL, int targetSize = 75, string seoTitle = "", string defaultPic = eCommerceConstants.DEFAULT_PICTURE)
        {
            var thumbnail = defaultPic;

            try
            {
                seoTitle = seoTitle.SafeSubstring(50);

                if (!string.IsNullOrEmpty(pictureURL))
                {
                    string thumbnailName = $"{pictureID}_{targetSize}{(string.IsNullOrEmpty(seoTitle) ? "" : $"_{seoTitle}_")}{Path.GetExtension(pictureURL)}";
                    string thumbnailFilePath = Path.Combine(_webRootPath, eCommerceConstants.IMAGES_THUMBNAIL_DIRECTORY, thumbnailName);
                    string originalFilePath = Path.Combine(_webRootPath, eCommerceConstants.MAIN_IMAGES_DIRECTORY, pictureURL);

                    using (var mutex = new Mutex(false, thumbnailName))
                    {
                        if (!Directory.Exists(Path.Combine(_webRootPath, eCommerceConstants.IMAGES_THUMBNAIL_DIRECTORY)))
                        {
                            Directory.CreateDirectory(Path.Combine(_webRootPath, eCommerceConstants.IMAGES_THUMBNAIL_DIRECTORY));
                        }

                        if (File.Exists(thumbnailFilePath))
                        {
                            thumbnail = thumbnailName;
                        }
                        else
                        {
                            mutex.WaitOne();

                            if (!File.Exists(thumbnailFilePath) && File.Exists(originalFilePath))
                            {
                                using (var image = Image.Load(originalFilePath))
                                {
                                    image.Mutate(x => x.Resize(new ResizeOptions
                                    {
                                        Mode = ResizeMode.Max,
                                        Size = CalculateDimensions(image.Size(), targetSize)
                                    }));

                                    image.Save(thumbnailFilePath);
                                }

                                thumbnail = thumbnailName;
                            }

                            mutex.ReleaseMutex();
                        }
                    }
                }

                return $"{eCommerceConstants.THUMBNAIL_FOLDERNAME}{thumbnail}";
            }
            catch
            {
                return pictureURL;
            }
        }

        private static Size CalculateDimensions(Size originalSize, int targetSize, bool ensureSizePositive = true)
        {
            float width, height;

            if (originalSize.Height > originalSize.Width)
            {
                width = originalSize.Width * (targetSize / (float)originalSize.Height);
                height = targetSize;
            }
            else
            {
                width = targetSize;
                height = originalSize.Height * (targetSize / (float)originalSize.Width);
            }

            if (!ensureSizePositive) return new Size((int)Math.Round(width), (int)Math.Round(height));

            return new Size((int)Math.Max(Math.Round(width), 1), (int)Math.Max(Math.Round(height), 1));
        }
    }
}
