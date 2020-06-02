using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praca_Inżynierska
{
    public class PortCOMSetup
    {
        public string baudRate { get; set;}
        public string dataBits { get; set; }
        public string stopBits { get; set; }
        public string parity { get; set; }
    }
}
