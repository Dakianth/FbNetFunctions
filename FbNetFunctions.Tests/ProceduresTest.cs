using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FbNetFunctions.Tests
{
    [TestClass]
    public class ProceduresTest
    {
        [TestMethod]
        public void DownloadImageTest()
        {
            var result = Procedures.DownloadImage("https://selos.tjmg.jus.br/sisnor/eselo/consultaSeloseAtos.jsf?selo=ABC12345&codigo=1234567812345678", 150, 150, 500);
            bool l = result.MoveNext();
        }
    }
}
