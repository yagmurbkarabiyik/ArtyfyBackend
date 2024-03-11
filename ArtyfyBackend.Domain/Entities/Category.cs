namespace ArtyfyBackend.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        /// <summary>
        /// Relations
        /// </summary>
        public List<Product> Products { get; set; }
    }
}