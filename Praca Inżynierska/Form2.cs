using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praca_Inżynierska
{
    public partial class Form2 : Form
    {
        public PortCOMSetup portCOM = new PortCOMSetup();
        
        public Form2(string value)
        {
            InitializeComponent();
            value = cBoxBaudRate.Text;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
