using BookAudit.Controllers;
using BookAudit.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
        public void Get_AllAuthors_ReturnsTwoAuthor()
        {
            var res = hm.GetAllAuthors();
            Assert.AreEqual(2, res.Count);
        }

        [TestMethod]
        [DataRow(null, "C#",  null, null)]
        [DataRow(null, "MVC", null, null)]
        public void Filter_BooksByName_ReturnsBooksContainingTheSameValue(int? authorId, string name, int? inReserve, int? inArchieve)
        {
            var books = hm.FilterBooks(authorId, name, inReserve, inArchieve);
            bool actual = books.All(b => b.Name.Contains(name.Trim()));
            Assert.IsTrue(actual);
        }

        [TestMethod]
        [DataRow(1, " ", null, null)]
        [DataRow(3, "MVC", null, null)]
        public void Filter_BooksByAuthorId_ReturnsBooksContainingTheSameValue(int? authorId, string name, int? inReserve, int? InArchive)
        {
            var books = hm.FilterBooks(authorId, name, inReserve, InArchive);
            bool actual = books.All(b => b.AuthorId== authorId);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        [DataRow(1, " ", null, null)]
        [DataRow(3, "MVC", null, null)]
        public void Filter_BooksByAuthorIdAndName_ReturnsBooksContainingTheSameValue(int? authorId, string name, int? inReserve, int? InArchive)
        {
            var books = hm.FilterBooks(authorId, name, inReserve, InArchive);
            bool actual = books.All(b => b.AuthorId == authorId && b.Name.Contains(name.Trim()));
            Assert.IsTrue(actual);
        }

        [TestMethod]
        [DataRow(1, " ",      1, null)]
        [DataRow(3, "MVC",    0, null)]
        [DataRow(null, "MVC", 0, null)]
        [DataRow(null, "MVC", 1, null)]
        public void Filter_BooksByReservation_ReturnsBooksContainingTheSameValue(int? authorId, string name, int? inReserve, int? InArchive)
        {
            var books = hm.FilterBooks(authorId, name, inReserve, InArchive);
            bool isReserve = (inReserve == 1) ? true : false;
            bool actual = books.All(b => b.Reserve == isReserve);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        [DataRow(1, " ",      null, 1)]
        [DataRow(3, "MVC",    null, 0)]
        [DataRow(null, "MVC", null, 0)]
        [DataRow(null, "MVC", null, 1)]
        public void Filter_BooksByArchivation_ReturnsBooksContainingTheSameValue(int? authorId, string name, int? inReserve, int? InArchive)
        {
            var books = hm.FilterBooks(authorId, name, inReserve, InArchive);
            bool isArchieve = (InArchive == 1) ? true : false;
            bool actual = books.All(b => b.InArchive == isArchieve);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        [DataRow(null, " ",   1, 1)]
        [DataRow(null, null,  0, 1)]
        [DataRow(null, "",    1, 0)]
        [DataRow(null, "",    0, 0)]
        public void Filter_BooksByArchivationAndReservation_ReturnsBooksContainingTheSameValue(int? authorId, string name, int? inReserve, int? InArchive)
        {
            var books = hm.FilterBooks(authorId, name, inReserve, InArchive);
            bool isReserve = (inReserve == 1) ? true : false;
            bool isArchieve = (InArchive == 1) ? true : false;
            bool actual = books.All(b => b.InArchive == isArchieve && b.Reserve == isReserve);
            Assert.IsTrue(actual);
        }
        
        [TestMethod]
        [DataRow(1, " MVC",   1, 1)]
        [DataRow(3, "c#",  0, 1)]
        [DataRow(2, "Angular",    1, 0)]
        [DataRow(5, "Patterns",    0, 0)]
        public void Filter_BooksByAllFilters_ReturnsBooksContainingTheSameValue(int? authorId, string name, int? inReserve, int? InArchive)
        {
            var books = hm.FilterBooks(authorId, name, inReserve, InArchive);
            bool isReserve = (inReserve == 1) ? true : false;
            bool isArchieve = (InArchive == 1) ? true : false;
            bool actual = books.All(b => 
                b.InArchive == isArchieve 
                && b.Reserve == isReserve
                && b.AuthorId == authorId 
                && b.Name.Contains(name.Trim()));
            Assert.IsTrue(actual);
        }


        [TestMethod]
        [DataRow(-1, "", null, 45)]
        [DataRow(0, "    ", -1, null)]
        [DataRow(null, null, null, -1)]
        [DataRow(null, null, 67, -1)]
        public void Filter_Books_DataIsIncorrect_ReturnsFourBook(int? id, string name, int? reserve, int? archieve)
        {
            var res = hm.FilterBooks(id, name, reserve, archieve);
            Assert.AreEqual(4, res.Count);
        }

        [TestMethod]
        [DataRow(1, true)]
        [DataRow(2, false)]
        public void Change_BookReservation_ReturnsThisBookWithNewValue(int? id, bool reserve)
        {
            hm.ChangeBookReservation(id, reserve);
            Book book = hm.GetBookById(id);
            Assert.IsFalse(book.Reserve== reserve);
        }

        [TestMethod]
        [DataRow(1, true)]
        [DataRow(2, false)]
        public void Change_BookArchivation_ReturnsThisBookWithNewValue(int? id, bool arch)
        {
            hm.ChangeBookArchievation(id, arch);
            Book book = hm.GetBookById(id);
            Assert.IsFalse(book.InArchive == arch);
        }

        [TestMethod]
        [DataRow(-1, true)]
        [DataRow(null, false)]
        [DataRow(0, false)]
        public void Change_BookArchivation_CathExeption(int? id, bool arch)
        {
            try
            {
                hm.ChangeBookArchievation(id, arch);
                Assert.Fail("No exeption");
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Id is incorrect or book doesn't exist");
            }
        }

        [TestMethod]
        [DataRow(-1, true)]
        [DataRow(null, false)]
        [DataRow(0, false)]
        public void Change_BookReservation_CathExeption(int? id, bool reserve)
        {
            try
            {
                hm.ChangeBookReservation(id, reserve);
                Assert.Fail("No exeption");
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Id is incorrect or book doesn't exist");
            }
        }

    }
}
