using System.ComponentModel.DataAnnotations;

namespace ForumProject.Models.ViewModel
{
    public class ForumPostDTO
    {
        public string OwnerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
