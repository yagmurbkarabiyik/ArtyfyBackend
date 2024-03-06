namespace ArtyfyBackend.Core.Models.Product
{
    public class ProductModel
    {
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public int Stock { get; set; }
        public double Price { get; set; }
        public bool IsSellable { get; set; }

        public string UserAppId { get; set; } = null!;
        public int? CategoryId { get; set; }
    }
}