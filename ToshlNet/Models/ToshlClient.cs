using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ToshlNet.Models
{
    public class ToshlClient
    {        
        public string Id { get; set; }

        public string Secret { get; set; }

        public string RedirectUrl { get; set; }
    }
}
