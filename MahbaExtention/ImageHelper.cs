using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;


namespace MahbaExtention
{
    public static class ImageHelper
    {
        /// <summary>
        /// چرخش تصویر
        /// </summary>
        /// <param name="file">مسیر فایل تصویر</param>
        /// <param name="rotateFlipType">نوع چرخش</param>
        public static Image RotateImage(string file, RotateFlipType rotateFlipType)
        {
            Image image = Image.FromFile(file);
            int pageCount = image.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page);
            Image[] pages = new Image[pageCount];
            for (int i = 0; i < pageCount; i++)
            {
                image.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, i);
                pages[i] = GetCopyOfImage(image);
            }
            for (int i = 0; i < pageCount; i++)
            {
                pages[i].RotateFlip(rotateFlipType);
            }
            if (pageCount == 1)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                pages[0].Save(ms, (image.RawFormat.Guid == ImageFormat.MemoryBmp.Guid) ? ImageFormat.Png : image.RawFormat);
                image.Dispose();
                System.IO.File.WriteAllBytes(file, ms.ToArray());
                ms.Dispose();
                return Image.FromFile(file);
            }
            else
            {
                byte[] bytes = ConvertImagesToMultiTiffBytes(pages);
                image.Dispose();
                System.IO.File.WriteAllBytes(file, bytes);
                return Image.FromFile(file);
            }
        }

        /// <summary>
        /// تبدیل تصاویر به یک تصویر چند صفحه ای
        /// MultiTiff
        /// </summary>
        /// <param name="pages">تصاویر</param>
        /// <param name="saveTo">مسیر ذخیره</param>
        /// <param name="compressionType">نوع فشرده سازی</param>
        /// <returns></returns>
        public static Image ConvertImagesToMultiTiff(Image[] pages, string saveTo, CompressionTypes compressionType = CompressionTypes.CompressionNone)
        {
            byte[] bytes = ConvertImagesToMultiTiffBytes(pages, compressionType);
            System.IO.File.WriteAllBytes(saveTo, bytes);
            return Image.FromFile(saveTo);
        }

        public static byte[] ConvertImagesToMultiTiffBytes(Image[] pages, CompressionTypes compressionType = CompressionTypes.CompressionNone)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                Image image = pages[0];

                ImageCodecInfo encoderInfo = ImageCodecInfo.GetImageEncoders().First(i => i.MimeType == "image/tiff");
                EncoderParameters encoderParameters;
                if (compressionType == CompressionTypes.CompressionNone)
                    encoderParameters = new EncoderParameters(1);
                else
                    encoderParameters = new EncoderParameters(2);

                encoderParameters.Param[0] = new EncoderParameter(Encoder.SaveFlag, (long)EncoderValue.MultiFrame);
                if (compressionType != CompressionTypes.CompressionNone)
                    encoderParameters.Param[1] = new EncoderParameter(Encoder.Compression, (long)(compressionType));
                image.Save(ms, encoderInfo, encoderParameters);

                encoderParameters.Param[0] = new EncoderParameter(Encoder.SaveFlag, (long)EncoderValue.FrameDimensionPage);
                if (compressionType != CompressionTypes.CompressionNone)
                    encoderParameters.Param[1] = new EncoderParameter(Encoder.Compression, (long)(compressionType));
                for (int i = 1; i < pages.Length; i++)
                    image.SaveAdd(pages[i], encoderParameters);

                encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.SaveFlag, (long)EncoderValue.Flush);
                image.SaveAdd(encoderParameters);
                ms.Flush();
                return ms.ToArray();
            }
        }

        /// <summary>
        /// تبدیل تصاویر به یک تصویر چند صفحه ای
        /// MultiTiff
        /// </summary>
        public static Bitmap ConvertImagesToMultiTiff(Image[] pages)
        {
            if (pages.Length == 0)
                return null;
            Bitmap bitmap = null;
            System.IO.MemoryStream memoryStream = null;
            EncoderParameters tiffEncoderParams = new EncoderParameters(1);
            try
            {
                bitmap = new Bitmap(pages[0].Width, pages[0].Height);
                bitmap.SetResolution(pages[0].HorizontalResolution, pages[0].VerticalResolution);

                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.DrawImageUnscaled(pages[0], 0, 0);
                graphics.Dispose();

                if (pages.Length > 1)
                {
                    memoryStream = new System.IO.MemoryStream();
                    tiffEncoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.MultiFrame);
                    ImageCodecInfo tiffEncoderInfo = ImageCodecInfo.GetImageEncoders().First(i => i.MimeType == "image/tiff");
                    bitmap.Save(memoryStream, tiffEncoderInfo, tiffEncoderParams);
                }
                for (int i = 1; i < pages.Count(); i++)
                {
                    Bitmap newPage = new Bitmap(pages[i].Width, pages[i].Height);
                    newPage.SetResolution(pages[i].HorizontalResolution, pages[i].VerticalResolution);
                    Graphics g = Graphics.FromImage(newPage);
                    g.DrawImageUnscaled(pages[i], 0, 0);
                    g.Dispose();

                    tiffEncoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.FrameDimensionPage);

                    bitmap.SaveAdd(newPage, tiffEncoderParams);
                    newPage.Dispose();
                }
                if (pages.Length > 1)
                {
                    tiffEncoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.Flush);
                    bitmap.SaveAdd(tiffEncoderParams);
                    memoryStream.Flush();
                }
            }
            catch
            {
                if (memoryStream != null)
                    memoryStream.Dispose();
                memoryStream = null;
                if (bitmap != null)
                    bitmap.Dispose();
                bitmap = null;
            }
            finally
            {
                if (memoryStream != null)
                {
                    bitmap.Dispose();
                    bitmap = (Bitmap)Image.FromStream(memoryStream);
                }
            }
            return bitmap;
        }

        /// <summary>
        /// تبدیل تصویر به یک فرمت خاص و دریافت بایت های تصویر
        /// </summary>
        /// <param name="image">تصویر</param>
        /// <param name="format">فرمت</param>
        /// <returns></returns>
        public static byte[] GetImageBytes(Image image, ImageFormat format)
        {
            Bitmap bitmap = new Bitmap(image.Width, image.Height);
            bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawImageUnscaled(image, 0, 0);
            g.Dispose();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bitmap.Save(ms, (format.Guid == ImageFormat.MemoryBmp.Guid) ? ImageFormat.Png : format);
                ms.Flush();
                return ms.ToArray();
            }
        }

        /// <summary>
        /// دریافت یک کپی از تصویر
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Bitmap GetCopyOfImage(Image image)
        {
            Bitmap bitmap = new Bitmap(image.Width, image.Height);
            bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawImageUnscaled(image, 0, 0);
            g.Dispose();
            return bitmap;
        }

        /// <summary>
        /// تبدیل یک تصویر چند صفحه ای به آرایه ای از بایت
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] GetMultiTiffImageBytes(Image image)
        {
            Bitmap bitmap = null;
            System.IO.MemoryStream memoryStream = null;
            int frameCount = image.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page);
            EncoderParameters tiffEncoderParams = new EncoderParameters(1);
            try
            {
                bitmap = new Bitmap(image.Width, image.Height);
                bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.DrawImageUnscaled(image, 0, 0);
                graphics.Dispose();

                if (frameCount > 1)
                {
                    memoryStream = new System.IO.MemoryStream();
                    tiffEncoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.MultiFrame);
                    ImageCodecInfo tiffEncoderInfo = ImageCodecInfo.GetImageEncoders().First(i => i.MimeType == "image/tiff");
                    bitmap.Save(memoryStream, tiffEncoderInfo, tiffEncoderParams);
                }
                for (int i = 1; i < frameCount; i++)
                {
                    Bitmap newPage = new Bitmap(image.Width, image.Height);
                    newPage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                    Graphics g = Graphics.FromImage(newPage);
                    image.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, i);
                    g.DrawImageUnscaled(image, 0, 0);
                    g.Dispose();

                    tiffEncoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.FrameDimensionPage);

                    bitmap.SaveAdd(newPage, tiffEncoderParams);
                    newPage.Dispose();
                }
                if (frameCount > 1)
                {
                    tiffEncoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.Flush);
                    bitmap.SaveAdd(tiffEncoderParams);
                    memoryStream.Flush();
                }
            }
            catch
            {
                if (memoryStream != null)
                    memoryStream.Dispose();
                memoryStream = null;
                if (bitmap != null)
                    bitmap.Dispose();
                bitmap = null;
                throw;
            }
            byte[] data = memoryStream.ToArray();
            if (memoryStream != null)
                memoryStream.Dispose();
            memoryStream = null;
            if (bitmap != null)
                bitmap.Dispose();
            bitmap = null;
            return data;
        }

        /// <summary>
        /// دریافت یک کپی از تصویر چند صفحه ای
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Bitmap GetMultiCopyOfImage(Image image)
        {
            Bitmap thumbImage = null;
            System.IO.MemoryStream memoryStream = null;
            int frameCount = image.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page);
            EncoderParameters tiffEncoderParams = new EncoderParameters(1);
            try
            {
                thumbImage = new Bitmap(image.Width, image.Height);
                thumbImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                Graphics graphics = Graphics.FromImage(thumbImage);
                graphics.DrawImageUnscaled(image, 0, 0);
                graphics.Dispose();

                if (frameCount > 1)
                {
                    memoryStream = new System.IO.MemoryStream();
                    tiffEncoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.MultiFrame);
                    ImageCodecInfo tiffEncoderInfo = ImageCodecInfo.GetImageEncoders().First(i => i.MimeType == "image/tiff");
                    thumbImage.Save(memoryStream, tiffEncoderInfo, tiffEncoderParams);
                }
                for (int i = 1; i < frameCount; i++)
                {
                    image.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, i);
                    Bitmap newPage = new Bitmap(image.Width, image.Height);
                    newPage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                    Graphics g = Graphics.FromImage(newPage);
                    g.DrawImageUnscaled(image, 0, 0);
                    g.Dispose();

                    tiffEncoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.FrameDimensionPage);

                    thumbImage.SaveAdd(newPage, tiffEncoderParams);
                    newPage.Dispose();
                }
                if (frameCount > 1)
                {
                    tiffEncoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.Flush);
                    thumbImage.SaveAdd(tiffEncoderParams);
                    memoryStream.Flush();
                }
            }
            catch
            {
                if (memoryStream != null)
                    memoryStream.Dispose();
                memoryStream = null;
                if (thumbImage != null)
                    thumbImage.Dispose();
                thumbImage = null;
            }
            finally
            {
                if (memoryStream != null)
                {
                    thumbImage.Dispose();
                    thumbImage = (Bitmap)Image.FromStream(memoryStream);
                }
            }
            return thumbImage;
        }

        /// <summary>
        /// دریافت یک تصویر کوچک از تصویر اصلی
        /// </summary>
        /// <param name="image">تصویر اصلی</param>
        /// <param name="size">اندازه</param>
        /// <param name="backColor">رنگ پس زمینه</param>
        public static Image GetThumbnail(Image image, Size size, Color backColor)
        {
            if (size.Width <= 0 || size.Height <= 0)
                throw new ArgumentException();
            Image thumbImage = null;
            System.IO.MemoryStream memoryStream = null;
            int frameCount = image.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page);
            EncoderParameters tiffEncoderParams = new EncoderParameters(1);
            try
            {
                Size scaledSize = GetSizedImageBounds(image, size);
                thumbImage = new Bitmap(scaledSize.Width, scaledSize.Height);

                Graphics graphics = Graphics.FromImage(thumbImage);
                DrawImage(graphics, image, backColor, scaledSize);
                graphics.Dispose();

                if (frameCount > 1)
                {
                    memoryStream = new System.IO.MemoryStream();
                    tiffEncoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.MultiFrame);
                    ImageCodecInfo tiffEncoderInfo = ImageCodecInfo.GetImageEncoders().First(i => i.MimeType == "image/tiff");
                    thumbImage.Save(memoryStream, tiffEncoderInfo, tiffEncoderParams);
                }
                for (int i = 1; i < frameCount; i++)
                {
                    Image newPage = new Bitmap(scaledSize.Width, scaledSize.Height);

                    Graphics g = Graphics.FromImage(newPage);
                    image.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, i);
                    DrawImage(g, image, backColor, scaledSize);
                    g.Dispose();

                    tiffEncoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.FrameDimensionPage);

                    thumbImage.SaveAdd(newPage, tiffEncoderParams);
                    newPage.Dispose();
                }
                if (frameCount > 1)
                {
                    tiffEncoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.Flush);
                    thumbImage.SaveAdd(tiffEncoderParams);
                    memoryStream.Flush();
                }
            }
            catch
            {
                if (memoryStream != null)
                    memoryStream.Dispose();
                memoryStream = null;
                if (thumbImage != null)
                    thumbImage.Dispose();
                thumbImage = null;
            }
            finally
            {
                if (memoryStream != null)
                {
                    thumbImage.Dispose();
                    thumbImage = Image.FromStream(memoryStream);
                }
            }
            return thumbImage;
        }

        /// <summary>
        /// رسم تصویر
        /// </summary>
        /// <param name="g"></param>
        /// <param name="image"></param>
        /// <param name="backColor"></param>
        /// <param name="size"></param>
        public static void DrawImage(Graphics g, Image image, Color backColor, Size size)
        {
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            using (Brush brush = new SolidBrush(backColor))
                g.FillRectangle(Brushes.White, 0, 0, size.Width, size.Height);
            g.DrawImage(image, 0, 0, size.Width, size.Height);
        }

        /// <summary>
        /// دریافت طول و عرض از یک تصویر با توجه به طول و عرضی که به تابع داده می شود به شکلی که نسبت طول و عرض تصویر اصلی حفظ شود
        /// </summary>
        /// <param name="image">تصویر</param>
        /// <param name="fit">اندازه</param>
        /// <returns>اندازه جدید به شکلی که نسبت طول و عرض تصویر اصلی حفظ شود</returns>
        public static Size GetSizedImageBounds(Image image, Size fit)
        {
            float f = System.Math.Max((float)image.Width / (float)fit.Width, (float)image.Height / (float)fit.Height);
            if (f < 1.0f) f = 1.0f; // Do not upsize small images
            int width = (int)System.Math.Round((float)image.Width / f);
            int height = (int)System.Math.Round((float)image.Height / f);
            return new Size(width, height);
        }

        public enum CompressionTypes
        {
            //
            // Summary:
            //     Specifies the LZW compression scheme. Can be passed to the TIFF encoder as
            //     a parameter that belongs to the Compression category.
            CompressionLZW = 2,
            //
            // Summary:
            //     Specifies the CCITT3 compression scheme. Can be passed to the TIFF encoder
            //     as a parameter that belongs to the compression category.
            CompressionCCITT3 = 3,
            //
            // Summary:
            //     Specifies the CCITT4 compression scheme. Can be passed to the TIFF encoder
            //     as a parameter that belongs to the compression category.
            CompressionCCITT4 = 4,
            //
            // Summary:
            //     Specifies the RLE compression scheme. Can be passed to the TIFF encoder as
            //     a parameter that belongs to the compression category.
            CompressionRle = 5,
            //
            // Summary:
            //     Specifies no compression. Can be passed to the TIFF encoder as a parameter
            //     that belongs to the compression category.
            CompressionNone = 6,
        }


        /// <summary>
        /// تبدیل تصویر
        /// </summary>
        /// <param name="image">تصویر</param>
        /// <param name="format">نوع</param>
        /// <param name="compressionType">فشرده سازی</param>
        /// <returns></returns>
        public static Image ConvertImage(Image image, ImageFormat format, CompressionTypes compressionType)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            if (compressionType == CompressionTypes.CompressionNone)
            {
                image.Save(ms, format);
            }
            else
            {
                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, (long)compressionType);
                image.Save(ms, GetImageCodecInfo(format), encoderParameters);
            }
            return Image.FromStream(ms);
        }

        /// <summary>
        /// تبدیل تصویر
        /// </summary>
        /// <param name="image">تصویر</param>
        /// <param name="format">نوع</param>
        /// <param name="compressionType">فشرده سازی</param>
        /// <param name="saveTo">مسیر ذخیره</param>
        public static void ConvertImage(Image image, ImageFormat format, CompressionTypes compressionType, string saveTo)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                if (compressionType == CompressionTypes.CompressionNone)
                {
                    image.Save(ms, format);
                }
                else
                {
                    EncoderParameters encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, (long)compressionType);
                    image.Save(ms, GetImageCodecInfo(format), encoderParameters);
                }
                System.IO.File.WriteAllBytes(saveTo, ms.ToArray());
            }
        }

        public static ImageCodecInfo GetImageCodecInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageEncoders().Where(t => t.FormatID == format.Guid).Single();
        }

        /// <summary>
        /// بارگذاری تصویر
        /// </summary>
        /// <param name="fileName">فایل</param>
        /// <returns></returns>
        public static System.Drawing.Image LoadImageFromFile(string fileName)
        {
            Image image = null;
            System.IO.FileStream stream = null;
            try
            {
                stream = System.IO.File.OpenRead(fileName);
                System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
                byte[] buffer = new byte[10 * 1024];
                while (true)
                {
                    int read = stream.Read(buffer, 0, 10000);
                    if (read == 0)
                        break;
                    memoryStream.Write(buffer, 0, read);
                }
                image = Bitmap.FromStream(memoryStream);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
            return image;
        }

        /// <summary>
        /// اینورت کردن تصویر
        /// </summary>
        /// <param name="image">تصویر</param>
        /// <returns></returns>
        public static Bitmap Invert(Image image)
        {
            Bitmap bitmap = GetCopyOfImage(image);
            BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                int nOffset = stride - bitmap.Width * 3;
                int nWidth = bitmap.Width * 3;
                for (int y = 0; y < bitmap.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        p[0] = (byte)(255 - p[0]);
                        ++p;
                    }
                    p += nOffset;
                }
            }
            bitmap.UnlockBits(bmData);
            return bitmap;
        }

        /// <summary>
        /// سیاه سفید کردن تصویر
        /// </summary>
        /// <param name="image">تصویر</param>
        /// <returns></returns>
        public static Image SetGrayScale(Image image)
        {
            Bitmap bitmap = GetCopyOfImage(image);
            BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                int nOffset = stride - bitmap.Width * 3;
                byte red, green, blue;
                for (int y = 0; y < bitmap.Height; ++y)
                {
                    for (int x = 0; x < bitmap.Width; ++x)
                    {
                        blue = p[0];
                        green = p[1];
                        red = p[2];

                        p[0] = p[1] = p[2] = (byte)(.299 * red + .587 * green + .114 * blue);

                        p += 3;
                    }
                    p += nOffset;
                }
            }
            bitmap.UnlockBits(bmData);
            return bitmap;
        }

        /// <summary>
        /// تنظیم روشنایی تصویر
        /// </summary>
        /// <param name="file">مسیر فایل تصویر</param>
        /// <param name="brightness">مقدار روشنایی</param>
        public static void SetBrightness(string file, int brightness)
        {
            Image image = Image.FromFile(file);
            ImageFormat format = image.RawFormat;
            Image newImage = SetBrightness(image, brightness);
            image.Dispose();
            System.IO.File.WriteAllBytes(file, GetImageBytes(newImage, format));
        }

        /// <summary>
        /// تنظیم روشنایی تصویر
        /// </summary>
        /// <param name="image">تصویر</param>
        /// <param name="brightness">مقدار روشنایی</param>
        /// <returns></returns>
        public static Image SetBrightness(Image image, int brightness)
        {
            Bitmap bitmap = GetCopyOfImage(image);
            if (brightness < -255 || brightness > 255)
                throw new Exception();
            BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            int nVal = 0;
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                int nOffset = stride - bitmap.Width * 3;
                int nWidth = bitmap.Width * 3;
                for (int y = 0; y < bitmap.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nVal = (int)(p[0] + brightness);

                        if (nVal < 0) nVal = 0;
                        if (nVal > 255) nVal = 255;

                        p[0] = (byte)nVal;

                        ++p;
                    }
                    p += nOffset;
                }
            }
            bitmap.UnlockBits(bmData);
            return bitmap;
        }

        /// <summary>
        /// تنظیم کنتراست تصویر
        /// </summary>
        /// <param name="file">مسیر فایل</param>
        /// <param name="contrast">مقدار کنتراست</param>
        public static void SetContrast(string file, int contrast)
        {
            Image image = Image.FromFile(file);
            ImageFormat format = image.RawFormat;
            Image newImage = SetContrast(image, contrast);
            image.Dispose();
            System.IO.File.WriteAllBytes(file, GetImageBytes(newImage, format));
        }

        /// <summary>
        /// تنظیم کنتراست تصویر
        /// </summary>
        /// <param name="image">تصویر</param>
        /// <param name="nContrast">مقدار کنتراست</param>
        /// <returns></returns>
        public static Image SetContrast(Image image, int nContrast)
        {
            Bitmap bitmap = GetCopyOfImage(image);
            if (nContrast < -100)
                throw new Exception();
            if (nContrast > 100)
                throw new Exception();
            double pixel = 0, contrast = (100.0 + nContrast) / 100.0;
            contrast *= contrast;
            int red, green, blue;
            BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - bitmap.Width * 3;

                for (int y = 0; y < bitmap.Height; ++y)
                {
                    for (int x = 0; x < bitmap.Width; ++x)
                    {
                        blue = p[0];
                        green = p[1];
                        red = p[2];

                        pixel = red / 255.0;
                        pixel -= 0.5;
                        pixel *= contrast;
                        pixel += 0.5;
                        pixel *= 255;
                        if (pixel < 0) pixel = 0;
                        if (pixel > 255) pixel = 255;
                        p[2] = (byte)pixel;

                        pixel = green / 255.0;
                        pixel -= 0.5;
                        pixel *= contrast;
                        pixel += 0.5;
                        pixel *= 255;
                        if (pixel < 0) pixel = 0;
                        if (pixel > 255) pixel = 255;
                        p[1] = (byte)pixel;

                        pixel = blue / 255.0;
                        pixel -= 0.5;
                        pixel *= contrast;
                        pixel += 0.5;
                        pixel *= 255;
                        if (pixel < 0) pixel = 0;
                        if (pixel > 255) pixel = 255;
                        p[0] = (byte)pixel;

                        p += 3;
                    }
                    p += nOffset;
                }
            }
            bitmap.UnlockBits(bmData);
            return bitmap;
        }

        /// <summary>
        /// تنظیم گاما برای تصویر
        /// </summary>
        /// <param name="image"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Image SetGamma(Image image, double value)
        {
            return SetGamma(image, value, value, value);
        }

        /// <summary>
        /// تنظیم گاما برای تصویر
        /// </summary>
        /// <param name="image">تصویر</param>
        /// <param name="red">مقدار قرمز</param>
        /// <param name="green">مقدار سبز</param>
        /// <param name="blue">مقدار آبی</param>
        /// <returns></returns>
        public static Image SetGamma(Image image, double red, double green, double blue)
        {
            Bitmap bitmap = GetCopyOfImage(image);
            if (red < .2 || red > 5)
                throw new Exception();
            if (green < .2 || green > 5)
                throw new Exception();
            if (blue < .2 || blue > 5)
                throw new Exception();
            byte[] redGamma = new byte[256];
            byte[] greenGamma = new byte[256];
            byte[] blueGamma = new byte[256];
            for (int i = 0; i < 256; ++i)
            {
                redGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / red)) + 0.5));
                greenGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / green)) + 0.5));
                blueGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / blue)) + 0.5));
            }
            BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                int nOffset = stride - bitmap.Width * 3;
                for (int y = 0; y < bitmap.Height; ++y)
                {
                    for (int x = 0; x < bitmap.Width; ++x)
                    {
                        p[2] = redGamma[p[2]];
                        p[1] = greenGamma[p[1]];
                        p[0] = blueGamma[p[0]];
                        p += 3;
                    }
                    p += nOffset;
                }
            }
            bitmap.UnlockBits(bmData);
            return bitmap;
        }

        /// <summary>
        /// تنطیم شدت رنگ
        /// </summary>
        /// <param name="image">تصویر</param>
        /// <param name="red">مقدار قرمز</param>
        /// <param name="green">مقدار سبز</param>
        /// <param name="blue">مقدار آبی</param>
        /// <returns></returns>
        public static Image SetColor(Image image, int red, int green, int blue)
        {
            Bitmap bitmap = GetCopyOfImage(image);
            if (red < -255 || red > 255)
                throw new Exception();
            if (green < -255 || green > 255)
                throw new Exception();
            if (blue < -255 || blue > 255)
                throw new Exception();
            BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                int nOffset = stride - bitmap.Width * 3;
                int nPixel;
                for (int y = 0; y < bitmap.Height; ++y)
                {
                    for (int x = 0; x < bitmap.Width; ++x)
                    {
                        nPixel = p[2] + red;
                        nPixel = Math.Max(nPixel, 0);
                        p[2] = (byte)Math.Min(255, nPixel);

                        nPixel = p[1] + green;
                        nPixel = Math.Max(nPixel, 0);
                        p[1] = (byte)Math.Min(255, nPixel);

                        nPixel = p[0] + blue;
                        nPixel = Math.Max(nPixel, 0);
                        p[0] = (byte)Math.Min(255, nPixel);

                        p += 3;
                    }
                    p += nOffset;
                }
            }
            bitmap.UnlockBits(bmData);
            return bitmap;
        }
    }
}
