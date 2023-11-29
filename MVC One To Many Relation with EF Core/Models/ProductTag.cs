namespace MVC_One_To_Many_Relation_with_EF_Core.Models
{
    public class ProductTag : BaseEntity
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
