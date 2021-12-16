using BookAudit.Data;
using BookAudit.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestBookAudit.Data
{
    // Симуляция репозитория с авторами.
    class TestAuthorRepository : IAuthorRepository<Author>
    {
        public void CreateAuthor(Author author)
        {
            // Делаем вид, что тут происходит какое-то сохранение 
        }

        public List<Author> GetAllAuthors()
        {
            // Делаем вид, что есть только 4 записи. 
            return new List<Author> {
                new Author() {
                    Name = "Freeman"
                },
                new Author() {
                    Name = "Troelsen"
                }
            };
        }
    }
}
