using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTabloServer.ViewModels
{
    public class PostFloorModel
    {
        public string floor { get; set; }
        public string paraNow { get; set; }
        public List<Models.Para> CHKR { get; set; }
    }
}
