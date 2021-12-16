using BookAudit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAudit.Data
{
    public class AuthorRepository : IAuthorRepository<Author>
    {
        BookContext _context;

        public AuthorRepository(BookContext context)
        {
            _context = context;
        }

        public void CreateAuthor(Author author)
        {
            _context.Add(author);
            _context.SaveChanges();
        }

        public List<Author> GetAllAuthors()
        {
            return _context.Author.OrderBy(a=>a.Name).ToList();
        }
    }
}
