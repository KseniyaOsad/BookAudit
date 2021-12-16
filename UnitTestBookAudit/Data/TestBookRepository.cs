using BookAudit.Data;
using BookAudit.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestBookAudit.Data
{
    
    // Симуляция репозитория с книгами.
    class TestBookRepository : IBookRepository<Book>
    {
        private Book _book = new Book()
        {
            Name = "C# .Net",
            Description = "Description",
            Reserve = false,
            InArchive = false
        };

        public void ChangeBookArchievation(int bookId, bool newArchievationValue)
        {
            _book.InArchive = newArchievationValue;
        }

        public void ChangeBookReservation(int bookId, bool newReservationValue)
        {
            _book.Reserve = newReservationValue;
        }

        public void CreateBook(Book book)
        {
            // Делаем вид, что тут происходит какое-то сохранение 
        }

        public List<Book> FilterBooks(string bookName)
        {
            return new List<Book> {
                new Book() {
                    Name = "C# .Net " + bookName,
                    Description = "Description",
                    Reserve = false,
                    InArchive = true
                } };
        }

        public List<Book> FilterBooks(int authorId)
        {
            return new List<Book> {
                new Book() {
                    Name = "C# .Net ",
                    Description = "Description",
                    AuthorId = authorId,
                    Reserve = false,
                    InArchive = true
                } };
        }

        public List<Book> FilterBooks(int authorId, string bookName)
        {
            return new List<Book> {
                new Book() {
                    Name = "C# .Net " + bookName,
                    Description = "Description",
                    AuthorId = authorId,
                    Reserve = false,
                    InArchive = true
                },
                new Book() {
                    Name = bookName+ "C# .Net",
                    Description = "Description",
                    AuthorId = authorId,
                    Reserve = true,
                    InArchive = false
                } };
        }

        public List<Book> GetAllBooks()
        {
            // Делаем вид, что есть только 4 записи. 
            return new List<Book> { 
                new Book() {
                    Name = "C# .Net",
                    Description = "Description",
                    Reserve = false,
                    InArchive = true
                },
                new Book() {
                    Name = "C# .Net",
                    Description = "Description",
                    Reserve = true,
                    InArchive = false
                },
                new Book() {
                    Name = "C# .Net",
                    Description = "Description",
                    Reserve = true,
                    InArchive = true
                },
                new Book() {
                    Name = "C# .Net",
                    Description = "Description",
                    Reserve = false,
                    InArchive = false
                }
            };
        }

        public Book GetBookById(int bookId)
        {
            // Делаем вид, что есть только 100 записей. 
            if (bookId > 100)
            {
                return null;
            }
            _book.Id = bookId;
            return _book;
        }

        public bool IsBookIdExists(int bookId)
        {
            // Делаем вид, что есть только 100 записей. 
            if (bookId > 100)
                return false;
            return true;

        }
    }
}
