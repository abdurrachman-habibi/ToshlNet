using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToshlNet.Models
{
    public class OAuthToken
    {
        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public long ExpiresIn { get; set; }

        public string RefreshToken { get; set; }

        public string Scope { get; set; }
    }
}
