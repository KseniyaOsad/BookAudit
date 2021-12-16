using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookAudit.Models
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Заполните поле")]
        [StringLength(100, ErrorMessage = "Имя должно быть в диапазоне от {2} до {1} символов.", MinimumLength = 2)]
        public string Name { get; set; }
    }
}
