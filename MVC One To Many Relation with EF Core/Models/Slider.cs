using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_One_To_Many_Relation_with_EF_Core.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]

        public string Title1 { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Title2 { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Title3 { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Description { get; set; }
        [StringLength(maximumLength: 100)]

        public string? Image { get; set; }
        public string RedirctUrl1 { get; set; }

        [NotMapped]
        public IFormFile? IFormFile { get; set; }
        


    }
}
