using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_One_To_Many_Relation_with_EF_Core.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool IsAvailable { get; set; }
        public double Tax { get; set; }
        public double CostPrice { get; set; }
        public double SalePrice { get; set; }
        public double DiscountedPrice { get; set; }
        
        public List<ProductTag>? ProductTag { get; set; }
        [NotMapped]
        public List<int>? TagIds { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
        [NotMapped]
        public List<IFormFile>? ImageFiles { get; set; }
        [NotMapped]
        public IFormFile? ProductPoster { get; set; }
        [NotMapped]
        public IFormFile? ProductHower { get; set; }
        [NotMapped]
        public List<int>? ProductImageIds { get; set; }
    }
}
