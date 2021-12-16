using BookAudit.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using UnitTestBookAudit.Data;

namespace UnitTestBookAudit
{
    [TestClass]
    public class BookController_UnitTest
    {
        BookController hm;
        [TestInitialize]
        public void InitializeTest()
        {
            hm = new BookController(new TestBookRepository(), new TestAuthorRepository());
        }

        [TestMethod]
        public void Get_AllAuthors_ReturnsTwoAuthor_Test()
        {
            var res = hm.GetAllAuthors();
            Assert.AreEqual(2, res.Count);
        }

        [TestMethod]
        [DataRow("First")]
        [DataRow("Second")]
        public void Filter_BooksByName_ReturnsBooksContainingTheSameName_Test(string name)
        {
            var res = hm.FilterBooks(null, name, null, null);
            bool actual = res[0].Name.Contains(name) ? true : false;
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(15)]
        public void Filter_BooksByAuthorId_ReturnsBooksContainingTheSameAuthorId_Test(int id)
        {
            var res = hm.FilterBooks(id, null, null, null);
            int actual = res.First().AuthorId;
            Assert.AreEqual(id, actual);
        }

        [TestMethod]
        [DataRow(1, "First")]
        [DataRow(15, "Second")]
        public void Filter_BooksByAuthorIdAndName_ReturnsTwoBooks_Test(int id, string name)
        {
            var res = hm.FilterBooks(id, name, null, null);
            Assert.AreEqual(2, res.Count);
        }

        [TestMethod]
        [DataRow(-1, "", null, null)]
        [DataRow(0, "    ", -1, null)]
        [DataRow(null, null, null, -1)]
        public void Filter_Books_DataIsIncorrect_ReturnsFourBook_Test(int? id, string name, int? reserve, int? archieve)
        {
            var res = hm.FilterBooks(id, name, reserve, archieve);
            Assert.AreEqual(4, res.Count);
        }
    }
}
