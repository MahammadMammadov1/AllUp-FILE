namespace MVC_One_To_Many_Relation_with_EF_Core.Models
{
    public class ProductImage : BaseEntity
    {
        public string ImageUrl { get; set; }
        public bool? isPoster { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
