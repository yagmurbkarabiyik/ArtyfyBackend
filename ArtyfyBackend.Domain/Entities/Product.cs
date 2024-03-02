using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtyfyBackend.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public bool IsSellable { get; set; }

        /// <summary>
        /// Relations
        /// </summary>
        public int UserId { get; set; }
        public UserApp UserApp { get; set; }
        public int CategoryId { get; set; }
        public List<Category> Categories { get; set; }
    }
}
