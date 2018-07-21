using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace FbNetFunctions
{
    public static class Procedures
    {
        public static IEnumerator<(string, bool?)> DeleteFile(string f)
        {
            var b = File.Exists(f);
            if (b)
                File.Delete(f);

            yield return (f, b);
        }

        public static IEnumerator<(string, string)> FileAppendLine(string f, string l)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(f));
            using (var s = File.AppendText(f))
                s.WriteLine(l);

            yield return (f, l);
        }

        public static IEnumerator<(int?, string)> DownloadImage(string s, int? w, int? h, int? t)
        {
            byte[] output = null;
            Bitmap image = GerarQRCode(w ?? 150, h ?? 150, s);
            using (MemoryStream ms_output = new MemoryStream())
            {
                image.Save(ms_output, ImageFormat.Jpeg);
                output = ms_output.ToArray();
            }

            string sdata = output.ByteArrayToString();
            foreach ((int i, string d) in ChunksUpto(sdata, t ?? 500))
                yield return (i, d);
        }

        public static Bitmap GerarQRCode(int width, int height, string text)
        {
            try
            {
                ZXing.BarcodeWriter bw = new ZXing.BarcodeWriter();
                ZXing.Common.EncodingOptions encOptions = new ZXing.Common.EncodingOptions() { Width = width, Height = height, Margin = 0 };
                bw.Options = encOptions;
                bw.Format = ZXing.BarcodeFormat.QR_CODE;
                Bitmap resultado = new Bitmap(bw.Write(text));
                return resultado;
            }
            catch
            {
                throw;
            }
        }

        private static IEnumerable<(int, string)> ChunksUpto(string str, int maxChunkSize)
        {
            int j = 0;
            for (int i = 0; i < str.Length; i += maxChunkSize)
                yield return (j++, str.Substring(i, Math.Min(maxChunkSize, str.Length - i)));
        }
    }

    public static class ProceduresExt
    {
        public static string ByteArrayToString(this byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);

            return hex.ToString();
        }
    }
}