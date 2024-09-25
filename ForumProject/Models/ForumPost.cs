using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectForum.Models
{
    public class ForumPost
    {
        [Key]
        public string Uid { get; private set; }

        public string OwnerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public int Rating { get; set; }

        public DateTime DateCreated { get; set; }

        [NotMapped]
        public virtual SiteUser Owner { get; set; }

        [NotMapped]
        public virtual IEnumerable<Comment> Comments { get; set; }

        public ForumPost()
        {
            DateCreated = DateTime.Now;
            Uid = Guid.NewGuid().ToString();
            Rating = 0;
        }

    }
}
