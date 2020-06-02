using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praca_Inżynierska
{
    public class RegisterValue
    {
        private ushort _Value;
        public ushort Value
        { get
            {
                return _Value;
            }
            set
            {
                if (_Value != value)
                {
                    _Value = value;
                }
            }
        }
    }
}
