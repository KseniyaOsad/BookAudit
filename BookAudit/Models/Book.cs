using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookAudit.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Заполните поле")]
        [StringLength(200, ErrorMessage = "Название книги должно быть в диапазоне от {2} до {1} символов.", MinimumLength = 2)]
        [Display(Name = "Название книги")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Заполните поле")]
        [Display(Name = "Описание книги")]
        public string Description { get; set; }
        
        [Display(Name = "Автор книги")]
        [Range(1, 1000000, ErrorMessage = "Укажите автора")]
        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
        // В данном случае у книги может быть только один автор.

        [DefaultValue(false)]
        public bool Reserve { get; set; }

        [DefaultValue(false)]
        public bool InArchive { get; set; }
    }
}
