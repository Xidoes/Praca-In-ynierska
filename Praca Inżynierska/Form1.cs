using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modbus.Device;
using Modbus.IO;
using Modbus.Utility;
using System.IO.Ports;
using System.Threading;

namespace Praca_Inżynierska
{
    public partial class Form1 : Form
    {

        private SerialPort serialPort = null;
        int duration = 0;
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();
                comboBoxComPort.Items.AddRange(ports);

                //serialPort = new SerialPort("COM5", 9600, Parity.None, 8, StopBits.One);
                //serialPort.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void BtnWriteMultipleRegisters_Click(object sender, EventArgs e)
        {
            try
            {
                IModbusMaster masterRtu = ModbusSerialMaster.CreateRtu(serialPort);
                byte slaveAddress = 9;
                ushort startAddress = 4001;
                string[] strArray = textBox11.Text.Split(',');
                float[] floatArray = new float[strArray.Length];
                for (int i = 0; i < strArray.Length; i ++)
                {
                    floatArray[i] = float.Parse(strArray[i]);
                }
                ushort[] data = ModbusUtilityNew.ConvertfloatArrayToUshortArray(floatArray);
                masterRtu.WriteMultipleRegisters(slaveAddress, startAddress, data);

            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtnOpenComPort_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort = new SerialPort(comboBoxComPort.Text, Convert.ToInt32(cBoxBaudRate.Text), Parity.None, Convert.ToInt32(cBoxDataBits.Text), StopBits.One);
                serialPort.Open();
                progressBarComPort.Value = 100;
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBarComPort.Value = 0;
            }
        }

        private void BtnCloseComPort_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort.Close();
                progressBarComPort.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnComPortRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();
                comboBoxComPort.Items.Clear();
                comboBoxComPort.Items.AddRange(ports);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReadHoldingRegister_Click(object sender, EventArgs e)
        {
            try
            {
                byte slaveAddress = 9;
                ushort startAddress = 4000;
                ushort numberOfPoints = 2;
                IModbusMaster masterRtu = ModbusSerialMaster.CreateRtu(serialPort);
                ushort[] ushortArray = masterRtu.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
                float[] result = ModbusUtilityNew.ConvertUshortArrayToFloatArrat(ushortArray);
                textBox12.Text = string.Empty;
                foreach (float item in result)
                {
                    textBox12.Text += string.Format("{0}/ ", item);
                }
            
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                timer.Enabled = true;
                timer.Start();
                ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
                {
                    while (true)
                    {
                        byte slaveAddress = 9;
                        ushort startAddress = 4000;
                        ushort numberOfPoints = 1;
                        IModbusMaster masterRtu = ModbusSerialMaster.CreateRtu(serialPort);
                        ushort[] result1 = masterRtu.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);

                        foreach (ushort item in result1)
                        {

                            textBox23.Invoke(new Action(delegate ()
                            {
                               textBox23.Text = Convert.ToString(item);
                            }));

                            chart2.Invoke(new Action(delegate ()
                            {
                                chart2.Series["Test"].Points.AddXY(Convert.ToString(duration), item);
                            }));
                            //textBox23.Text = Convert.ToString(item);
                            //textBox29.Text = Convert.ToString(duration);
                            //this.chart2.Series["Test"].Points.AddXY(Convert.ToString(duration), item);
                            //this.chart6.Series["Test"].Points.AddXY(duration, item);
                        }
                        Thread.Sleep(50);
                        ushort startAddress2 = 4001;
                        ushort[] result2 = masterRtu.ReadHoldingRegisters(slaveAddress, startAddress2, numberOfPoints);
                        foreach (ushort item in result2)
                        {
                            //textBox27.Text += string.Format("{0}/ ", item);
                            textBox27.Invoke(new Action(delegate ()
                            {
                                textBox27.Text = Convert.ToString(item);
                            }));
                            chart2.Invoke(new Action(delegate ()
                            {
                                chart2.Series["Test2"].Points.AddXY(Convert.ToString(duration), item);
                            }));
                        }
                        Thread.Sleep(100);
                    }
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Button8_Click(object sender, EventArgs e)
        {
            this.chart2.Series.Clear();
            this.chart2.Series.Add("Test");
           
        }

        private void BtnWriteMultipleRegistersWord_Click(object sender, EventArgs e)
        {
            try
            {
                IModbusMaster masterRtu = ModbusSerialMaster.CreateRtu(serialPort);
                byte slaveAddress = 9;
                ushort startAddress = 4001;

                ushort[] WriteMulipleRegistersTable = { Convert.ToUInt16(textBox24.Text), Convert.ToUInt16(textBox25.Text) };
                masterRtu.WriteMultipleRegisters(slaveAddress, startAddress, WriteMulipleRegistersTable);

            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            string WriteMultipleRegisters = (textBox24.Text + "," + textBox25.Text);
            textBox26.Text = WriteMultipleRegisters;
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Interval = 1;
            duration++;
        }
    }
}
