using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LearningAspNetWebApp.Models
{
    public class Category
    {
        [Key] public int Id { get; set; }

        private string? _name;

        [Required(ErrorMessage = "Please enter the Category Name before continuing")]
        public string? Name
        {
            get => _name;
            set => _name = value?.Trim();
        }

        //TODO Get used to using these attributes
        [Required(ErrorMessage = "Please enter the display order before continuing")]
        [DisplayName("Display Order")]
        [Range(1, 10, ErrorMessage = $"{nameof(DisplayOrder)} must be within range of 1 to 10")]
        public int DisplayOrder { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
    }
}