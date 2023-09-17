namespace Api.Core.Domain
{
    public class Category : BaseEntity
    {
        public string Definition { get; set; }
        public List<Product>? Products { get; set; }
    }
}
