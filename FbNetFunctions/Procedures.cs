using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

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

        public static IEnumerator<(string, byte[])> DownloadData(string s)
        {
            var client = new WebClient();
            var data = client.DownloadData(s);

            yield return (s, data);
        }
    }
}