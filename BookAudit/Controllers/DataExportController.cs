using BookAudit.Data;
using BookAudit.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookAudit.Controllers
{
    public class DataExportController : Controller
    {
        private readonly IBookRepository<Book> _bookRepository;
        private readonly IHostingEnvironment _hostEnvironment;
        public DataExportController(IHostingEnvironment hostEnvironment, IBookRepository<Book> iBook)
        {
            _hostEnvironment = hostEnvironment;
            _bookRepository = iBook;
        }
        public IActionResult DownloadCsv()
        {
            WriteCsv();
            return View();
        }

        public void WriteCsv()
        {
            string path = Path.Combine(_hostEnvironment.WebRootPath, "csvFiles/");
            string filename = "book.csv";
            StreamWriter sw = new StreamWriter(path + filename, false);
            List<BookAndAuthorToCSV> books = _bookRepository.GetAllBooks()
                .Select(b=>new BookAndAuthorToCSV() {
                    id=b.Id, title=b.Name, authorName = b.Author.Name
                }).ToList();
            
            //headers 
            foreach (BookAndAuthorToCSV book in books)
            {
                if (book.title.Contains(','))
                {
                    book.title = String.Format("\"{0}\"", book.title);
                }
                sw.Write(book.id + "," + book.title+ "," + book.authorName);
                sw.Write(sw.NewLine);
            }
            sw.Write(sw.NewLine);
            sw.Close();
        }
        private class BookAndAuthorToCSV
        {
            public int id { get; set; }
            public string title { get; set; }
            public string authorName { get; set; }
        }
    }
}
