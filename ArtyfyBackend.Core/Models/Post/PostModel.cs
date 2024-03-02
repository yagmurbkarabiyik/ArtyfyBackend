using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtyfyBackend.Core.Models.Post
{
    public class PostModel
    {
        public string Image { get; set; }
        public string Content { get; set; }
        public int LikeCount { get; set; }
    }
}
