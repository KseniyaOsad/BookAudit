using BookAudit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAudit.Data
{
    public interface IBookRepository<T>
    {
        List<T> GetAllBooks();

        List<T> FilterBooks(string bookName);

        List<T> FilterBooks(int authorId);

        List<T> FilterBooks(int authorId, string bookName);

        Book GetBookById(int bookId);

        void ChangeBookReservation(int bookId, bool newReservationValue);

        void ChangeBookArchievation(int bookId, bool newArchievationValue);

        bool IsBookIdExists(int bookId);

        void CreateBook(T book);
    }
}
