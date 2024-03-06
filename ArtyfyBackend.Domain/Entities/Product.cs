namespace ArtyfyBackend.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public int Stock { get; set; } 
        public double Price { get; set; } 
        public bool IsSellable { get; set; }

        /// <summary>
        /// Relations
        /// </summary>
        public string UserAppId { get; set; } = null!;
        public UserApp UserApp { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}