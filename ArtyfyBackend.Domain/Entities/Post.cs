using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtyfyBackend.Domain.Entities
{
    public class Post : BaseEntity
    {
        public string Image { get; set; }
        public string Content { get; set; }
        public int LikeCount { get; set; }

        /// <summary>
        /// Relations
        /// </summary>
        public int AppUserId { get; set; }
        public virtual UserApp UserApp { get; set; }
    }
}
