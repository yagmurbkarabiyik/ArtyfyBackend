using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtyfyBackend.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }

        /// <summary>
        /// Relations
        /// </summary>
        public int UserId { get; set; }
        public UserApp UserApp { get; set; }
    }
}
