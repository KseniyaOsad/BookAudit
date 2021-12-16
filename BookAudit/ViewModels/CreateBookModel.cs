using BookAudit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAudit.ViewModels
{
    public class CreateBookModel
    {
        public IEnumerable<Author> Authors { get; set; }
        public Book Book { get; set; }

    }
}
