namespace MVC_One_To_Many_Relation_with_EF_Core.Models
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public List<ProductTag>? productTags { get; set; }
    }
}
