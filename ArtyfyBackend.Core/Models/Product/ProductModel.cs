using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtyfyBackend.Core.Models.Product
{
    public class ProductModel
    {
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public int? Stock { get; set; }
        public double? Price { get; set; }
        public bool? IsSellable { get; set; }

        public int? UserId { get; set; }
        public int? CategoryId { get; set; }
    }
}
