using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtyfyBackend.Core.Models.Token
{
    public class TokenModel
    {
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}
