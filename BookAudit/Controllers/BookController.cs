using BookAudit.Models;
using BookAudit.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookAudit.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookAudit.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository<Book> _bookRepository;

        private readonly IAuthorRepository<Author> _authorRepository;

        public BookController(IBookRepository<Book> iBook, IAuthorRepository<Author> iAuthor)
        {
            _bookRepository = iBook;
            _authorRepository = iAuthor;
        }

        public IActionResult Index(int? authorId, string name, int? reservation, int? inArchieve)
        {
            List<Book> books = FilterBooks(authorId, name, reservation, inArchieve);
            List<Author> authors = GetAllAuthors();
            authors.Insert(0, new Author { Name = "Все авторы", Id = 0 });
            
            IndexHomeModel viewModel = new IndexHomeModel
            {
                Books = books,
                Authors = new SelectList(authors, "Id", "Name", authorId),
                TextInFieldName = (name != null) ? name : ""
            };
            return View(viewModel);
        }

        public IActionResult Details(int? id)
        {
            try
            {
                Book book = GetBookById(id);
                return View(book);
            }
            catch 
            {
                return RedirectToAction( nameof(Index), "Book");
            }
        }

        public IActionResult Create()
        {
            List<Author> authors = GetAllAuthors();
            authors.Insert(0, new Author { Name = "Виберите автора", Id = 0 });
            CreateBookModel viewModel = new CreateBookModel
            {
                Authors = authors
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Description,AuthorId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.CreateBook(book);
                return RedirectToAction(nameof(Index), "Book");
            }
            List<Author> authors = GetAllAuthors();
            authors.Insert(0, new Author { Name = "Виберите автора", Id = 0 });
            CreateBookModel viewModel = new CreateBookModel
            {
                Authors = authors
            };
            return View(viewModel);

        }

        public IActionResult BookReservation(int? bookId, bool isReserved, string actionName)
        {
            try
            {
                ChangeBookReservation(bookId, isReserved);

                if (actionName == "Details")
                {
                    return Redirect("/Book/"+ actionName +"/"+ bookId);
                }
                return RedirectToAction(actionName, "Book");
            }
            catch
            {
                return RedirectToAction(actionName, "Book");
            }
        }
        public IActionResult BookArchievation(int? bookId, bool isArchieved, string actionName)
        {
            try
            {
                ChangeBookArchievation(bookId, isArchieved);

                if (actionName == "Details")
                {
                    return Redirect("/Book/" + actionName + "/" + bookId);
                }
                return RedirectToAction(actionName, "Book");
            }
            catch
            {
                return RedirectToAction(actionName, "Book");
            }
        }
        public void ChangeBookArchievation(int? bookId, bool isArchieved)
        {
            if (bookId == null || !_bookRepository.IsBookIdExists((int)bookId))
            {
                throw new Exception("Id is incorrect or book doesn't exist");
            }
            _bookRepository.ChangeBookArchievation((int)bookId, !isArchieved);
        }
        public void ChangeBookReservation(int? bookId, bool isReserved )
        {
            if (bookId == null || !_bookRepository.IsBookIdExists((int)bookId))
            {
                throw new Exception("Id is incorrect or book doesn't exist");
            }
            _bookRepository.ChangeBookReservation((int)bookId, !isReserved);
        }

        public Book GetBookById(int? bookId)
        {
            if (bookId == null || bookId <= 0)
            {
                throw new Exception("Id is incorrect");
            }
            Book book = _bookRepository.GetBookById((int)bookId);
            if (book == null)
            {
                throw new Exception("Book doesn't found");
            }
            return book;
        }

        public List<Book> FilterBooks(int? authorId, string name, int? inReserve, int? inArchieve)
        {
            // Убираем отступы у строки.
            name = (name != null) ? name.Trim() : name;
            // authorId теперь либо null либо больше 0 (чтоб дальше долго не проверять).
            authorId = (authorId <= 0 || authorId == null) ? null : authorId;

            // Проверяем 0 случай, когда все поля фильтрации не заполнены.
            if (
                    (authorId == null)
                    && String.IsNullOrEmpty(name)
                    && (inReserve == null || inReserve < 0 || inReserve > 1)
                    && (inArchieve == null || inArchieve < 0 || inArchieve > 1)
                 )
            {
                return _bookRepository.GetAllBooks();
            }

            List<Book> books;
            // Проверяем первый случай когда заполнены все поля фильтрации.
            if ((authorId != null) && !String.IsNullOrEmpty(name))
            {
                books = _bookRepository.FilterBooks((int)authorId, name);
            }
            // Заполнен только автор книги.
            else if (authorId != null)
            {
                books = _bookRepository.FilterBooks((int)authorId);
            }
            // Заполнено только название книги.
            else if (!String.IsNullOrEmpty(name))
            {
                books = _bookRepository.FilterBooks(name);
            }
            // Поля фильтрации пустые, получаем весь список.
            else
            {
                books = _bookRepository.GetAllBooks();
            }

            // Выбраны поля резервации и архивации.
            if (inReserve >= 0 && inReserve <=1 && inArchieve >= 0 && inArchieve <= 1)
            {
                bool reservation = (inReserve == 0)? false: true;
                bool archievation = (inArchieve == 0)? false: true;
                return books.Where(b => b.InArchive == archievation && b.Reserve == reservation).ToList();
            }
            // Выбраны поля резервации.
            else if (inReserve >= 0 && inReserve <= 1)
            {
                bool reservation = (inReserve == 0) ? false : true;
                return books.Where(b => b.Reserve == reservation).ToList();
            }
            // Выбраны поля архивации.
            else if (inArchieve >= 0 && inArchieve <= 1)
            {
                bool archievation = (inArchieve == 0) ? false : true;
                return books.Where(b => b.InArchive == archievation).ToList();
            }
            // Поля резервации и архивации не выбраны.
            else
            {
                return books;
            }
        }

        public List<Author> GetAllAuthors()
        {
            return _authorRepository.GetAllAuthors();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
