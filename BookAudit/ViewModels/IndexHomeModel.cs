using BookAudit.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAudit.ViewModels
{
    public class IndexHomeModel
    {
        public SelectList Authors { get; set; }

        public SelectList Filters { get; set; } 

        public IEnumerable<Book> Books { get; set; }

        public string TextInFieldName { get; set; }

    }
}
