using System.ComponentModel.DataAnnotations;

namespace LoopSocialApp.Data.DataModels
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public int NumberOfReposrts { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
