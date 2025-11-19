using System;
using System.ComponentModel.DataAnnotations;

namespace CaptureTheIsland.Models
{
    public class Resource
    {
        // EF Core will configure the database to generate this value
        public int ResourceId { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a link.")]
        public string Link { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; }

        public string Slug => Name.Replace(' ', '-').ToLower();

    }
}
