using BookAudit.Data;
using BookAudit.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAudit.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository<Author> _authorRepository;

        public AuthorController(IAuthorRepository<Author> iAuthor)
        {
            _authorRepository = iAuthor;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name")] Author author)
        {
            if (ModelState.IsValid)
            {
                _authorRepository.CreateAuthor(author);
                return RedirectToAction("Create", "Book");
            }
            return View();

        }
    }
}
