using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAudit.Data
{
    public interface IAuthorRepository<T>
    {
        List<T> GetAllAuthors();

        void CreateAuthor(T author);
    }
}
