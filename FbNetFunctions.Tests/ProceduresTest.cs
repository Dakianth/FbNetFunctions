using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FbNetFunctions.Tests
{
    [TestClass]
    public class ProceduresTest
    {
        [TestMethod]
        public void DownloadFileTest()
        {
        }

        [TestMethod]
        public void DownloadDataTest()
        {
            var result = Procedures.DownloadData("https://chart.googleapis.com/chart?chs=100x100&cht=qr&chl=https://selos.tjmg.jus.br/sisnor/eselo/consultaSeloseAtos.jsf?selo=ABC12345%26codigo=1234567812345678");
            var l = result.MoveNext();
        }
    }
}
