using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;

namespace FbNetFunctions
{
    public static class Procedures
    {
        public static IEnumerator<(string, string, string)> DownloadFile(string s, string n)
        {
            var root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Automatiza", "QrCodes");
            Directory.CreateDirectory(root);

            var d = Path.Combine(root, n);
            var client = new WebClient();
            client.DownloadFile(s, d);

            yield return (s, n, d);
        }

        public static IEnumerator<(string, string)> DownloadData(string s)
        {
            var client = new WebClient();
            var data = client.DownloadData(s);

            byte[] output = null;
            using (var ms_input = new MemoryStream(data))
            {
                var i = new Bitmap(ms_input);
                using (var ms_output = new MemoryStream())
                {
                    ImageCodecInfo jpegEncoder = GetEncoder(ImageFormat.Jpeg);
                    var encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 30L);
                    i.Save(ms_output, jpegEncoder, encoderParameters);
                    output = ms_output.ToArray();
                }
            }
            string sdata = output.ByteArrayToString1();
            yield return (s, sdata);
        }
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                    return codec;
            }
            return null;
        }
    }
    public static class ProceduresExt
    {
        public static string ByteArrayToString1(this byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static string ByteArrayToString2(this byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }
    }
}