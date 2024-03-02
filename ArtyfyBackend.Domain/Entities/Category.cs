using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
