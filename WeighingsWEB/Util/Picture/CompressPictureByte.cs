using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace Util.Picture
{

	class CompressPictureByte
	{
		private Bitmap bitmap;

		public CompressPictureByte(byte[] pictureArray)
		{
			using (MemoryStream memoryStream = new MemoryStream(
				pictureArray ?? throw new ArgumentNullException("Нулевой массив байт")))
			{

				bitmap = new Bitmap(memoryStream);
				memoryStream.Close();
			}
		}

		public CompressPictureByte Resize(float compressRation)
		{

			bitmap = new Bitmap(
				bitmap ?? throw new NullReferenceException("Картинка не была декодирована."), 
				new Size(
					Convert.ToInt32(bitmap.Width / compressRation), 
					Convert.ToInt32(bitmap.Height / compressRation)
				)
			);

			return this;
		}

		public CompressPictureByte Compress(string type, int quality)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				ImageCodecInfo encoder = ImageCodecInfo
					.GetImageEncoders()
					.First(e => e.MimeType == $"image/{type}");


				EncoderParameters encoderParameter = new EncoderParameters
				{
					Param = new[] {
					new EncoderParameter(Encoder.Quality, quality)
					}
				};


				bitmap.Save(memoryStream, encoder, encoderParameter);
				bitmap = (Bitmap)Image.FromStream(memoryStream);
				memoryStream.Close();
			}
			return this;

		}

		public byte[] GetPictureArrayByte(ImageFormat format)
		{
			byte[] pictureArray = null;
			using (var memoryStream = new MemoryStream())
			{

				Bitmap bmp = new Bitmap(bitmap);
				bitmap.Dispose();
				bitmap = bmp;

				(bitmap ?? throw new NullReferenceException("Картинка не задана"))
					.Save(memoryStream, format);
				pictureArray = memoryStream.ToArray();
			}

			return pictureArray;
		}


		public Bitmap GetPictureResult()
		{
			return bitmap ?? throw new NullReferenceException("Картинка не была декодирована.") ;
		}
	}
}
