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
        private SerialPort serialPort = null;                           //zarezerwowanie nazwy zmiennej portu seryjnego
        int duration = 0;                                               //ilość tików czasu = 0
        public bool start;                                              //zmienna start (zatrzymuje 
        public byte slaveAddress = 9;                                          //adres urządzenia slave
        public ushort readStartAddress = 4000;                                 //
        public ushort writeStartAddress;                                //
        public ushort numberOfPoints = 5;                                     //ilość adresów rejestru, które program ma obsłużyć
        public List<Excel_Export> export = new List<Excel_Export>();    //przypisanie zmiennej export do listy klasy Excel Export
        public string settingsvalues;
        public Form1()
        {
            try
            {
                InitializeComponent();
                textBoxDCSpeed.Text = Convert.ToString(trackBarDCSpeed.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                start = false;
                string[] ports = SerialPort.GetPortNames();             //pobranie listy dostępnych portów COM
                cBoxComPort.Items.AddRange(ports);                      //pokazanie dostępnych portów w polu wyboru Port COM
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void BtnOpenComPort_Click(object sender, EventArgs e)
        {                           //do dokończenia tak aby mieć wybór Parity i Stop Bite
            try
            {
                serialPort = new SerialPort(cBoxComPort.Text, Convert.ToInt32(cBoxBaudRate.Text), Parity.None, Convert.ToInt32(cBoxDataBits.Text), StopBits.One);
                serialPort.Open();
                progressBarComPort.Value = 100;                        //Progress bar zmienia kolor na zielony, jako oznaczenie poprawnego połączenia z portem COM.
                //readWrite.OpenSerialPort();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBarComPort.Value = 0;                           //W wypadku wystąpienia błędu, 
            }
        }

        private void BtnCloseComPort_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort.Close();                                     // zamyka port COM
                progressBarComPort.Value = 0;                           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnComPortRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();             //pobiera nazwy dostępnych portów COM
                cBoxComPort.Items.Clear();                              //czyści wartości w polu wyboru
                cBoxComPort.Items.AddRange(ports);                      //pokazuje dostępne porty w polu wyboru
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                StartTest();
                start = true;                       //ustawia wartość start na true
                Read();
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnWriteMultipleRegistersWord_Click(object sender, EventArgs e)
        {
            try
            {
                writeStartAddress = 4001;
                Write();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnStop_Click(object sender, EventArgs e)
        {
            try
            {
                Stop();                        //ustawia wartość parametru start na false, po powoduje zatrzymanie ściągania wartości z sterownika
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                timer.Interval = 1;                 //ustawia częstotliwość ticku na 1ms    
                duration++;                         //dodaje wartość licznika z każdym tickiem
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = export;      //exportuje zawartość listy Excel Export do widoku danych
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void BtnExportClear_Click(object sender, EventArgs e)
        {
            try
            {
                export.Clear();
                export = new List<Excel_Export>();
                export.Add(new Excel_Export { ID = 0, Speed = "0", Torque = "0", x1 = "0", x2 = "0", x3 = "0" });
                dataGridView1.DataSource = export;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }
        private void Read()
        {
            try
            {
                int ID = 1;                                 //ustawia wartość ID do exportu wartości do listy Excel Export
                duration = 0;                               //zeruje licznik ticków czasu
                timer.Enabled = true;                       //włącza timer
                timer.Start();                              //startuje timer


                while (start == true)                                                       //sprawdza czy zmianna start ma wartość true
                {
                    IModbusMaster masterRtu = ModbusSerialMaster.CreateRtu(serialPort);         //tworzy połączenie do protokołu modbus przez port seryjny
                    ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>                          //nie podejmuje następnego działania przed dostaniem informacji zwrotnej
                    {
                        ushort[] result1 = masterRtu.ReadHoldingRegisters(slaveAddress, readStartAddress, numberOfPoints);      //zczytuje wartosci z rejestrów dla adresu urządzenia slave, adresu rejestru i liczbie kolejnych adresów
                        export.Add(new Excel_Export { ID = ID, Time = Convert.ToString(duration), Speed = Convert.ToString(result1[0]), Torque = Convert.ToString(result1[1]), x1 = Convert.ToString(result1[2]), x2 = Convert.ToString(result1[3]), x3 = Convert.ToString(result1[4]) }); //dodaje wartości zczytane do listy Excel Export
                        if (result1.Length == 0)
                        {
                            start = false;
                        }
                        for (int i = 0; i < 5; i++)                                             //dla każdej kolejnej wartości w result 1
                        {
                            chart2.Invoke(new Action(delegate ()                                //"wzywa" wykres chart2 
                            {
                                chart2.Series["Test" + Convert.ToString(i)].Points.AddXY(Convert.ToString(duration), result1[i]);   //łączy wartości rejestru z konkretnymi seriami wykresu
                                }));
                        }
                        textBoxRead4000.Invoke(new Action(delegate ()                           //"wzywa" okna tekstowe
                        {
                            textBoxRead4000.Text = Convert.ToString(result1[0]);                //wstawia pojedyńcze wartości z resutl1 w odpowiednie pole tekstowe
                                textBoxRead4001.Text = Convert.ToString(result1[1]);
                            textBoxRead4002.Text = Convert.ToString(result1[2]);
                            labelMaintPower.Text = (result1[0] * result1[1]).ToString();
                            labelDuration.Text = duration.ToString();
                            chart2.Series["Power"].Points.AddXY(Convert.ToString(duration), (result1[0] * result1[1]));

                        }));

                        ID = ID + 1;                // zwiększa ID    
                        Thread.Sleep(20);
                    }));
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void StartTest()
        {
            IModbusMaster masterRtu = ModbusSerialMaster.CreateRtu(serialPort);
            ushort[] result1 = masterRtu.ReadHoldingRegisters(slaveAddress, readStartAddress, numberOfPoints);
        }
        private void Stop()
        {
            start = false;
        }
        private void Write()
        {
            try
            {
                IModbusMaster masterRtu = ModbusSerialMaster.CreateRtu(serialPort);            //tworzy połączenie do protokołu modbus przez port seryjny      
                ushort[] WriteMulipleRegistersTable = { Convert.ToUInt16(textBoxWrite4001.Text), Convert.ToUInt16(textBoxWrite4002.Text), 0, 0, 0, 0, 0, 0, 0, Convert.ToUInt16(textBoxWrite4010.Text), Convert.ToUInt16(textBoxWrite4011.Text) }; //tworzy tabelę wartości z pól tekstowych 
                masterRtu.WriteMultipleRegisters(slaveAddress, writeStartAddress, WriteMulipleRegistersTable);      //wysyła wcześniej zdefiniowane wartości na konkretne adresy urządzenia
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TrackBarDCSpeed_Scroll(object sender, EventArgs e)
        {
            textBoxDCSpeed.Text = trackBarDCSpeed.Value.ToString();
        }

        private void TrackBarDCTorque_Scroll(object sender, EventArgs e)
        {
            textBoxDCTorque.Text = trackBarDCTorque.Value.ToString();
        }

        private void TrackBarDCx1_Scroll(object sender, EventArgs e)
        {
            textBoxDCx1.Text = trackBarDCx1.Value.ToString();
        }

        private void TrackBarDCx2_Scroll(object sender, EventArgs e)
        {
            textBoxDCx2.Text = trackBarDCx2.Value.ToString();
        }

        private void TrackBarDCx3_Scroll(object sender, EventArgs e)
        {
            textBoxDCx3.Text = trackBarDCx3.Value.ToString();
        }

        private void TrackBarAsychSpeed_Scroll(object sender, EventArgs e)
        {
            textBoxAsynchSpeed.Text = trackBarAsychSpeed.Value.ToString();
        }

        private void TrackBarAsychTorque_Scroll(object sender, EventArgs e)
        {
            textBoxAsynchTorque.Text = trackBarAsychTorque.Value.ToString();
        }

        private void TrackBarAsychx1_Scroll(object sender, EventArgs e)
        {
            textBoxAsynchx1.Text = trackBarAsychx1.Value.ToString();
        }

        private void TrackBarAsychx2_Scroll(object sender, EventArgs e)
        {
            textBoxAsynchx2.Text = trackBarAsychx2.Value.ToString();
        }

        private void TrackBarAsychx3_Scroll(object sender, EventArgs e)
        {
            textBoxAsynchx3.Text = trackBarAsychx3.Value.ToString();
        }

        private void TrackBarBLDCSpeed_Scroll(object sender, EventArgs e)
        {
            textBoxBLDCSpeed.Text = trackBarBLDCSpeed.Value.ToString();
        }

        private void TrackBarBLDCTorque_Scroll(object sender, EventArgs e)
        {
            textBoxBLDCTorque.Text = trackBarBLDCTorque.Value.ToString();
        }

        private void TrackBarBLDCx1_Scroll(object sender, EventArgs e)
        {
            textBoxBLDCx1.Text = trackBarBLDCx1.Value.ToString();
        }

        private void TrackBarBLDCx2_Scroll(object sender, EventArgs e)
        {
            textBoxBLDCx2.Text = trackBarBLDCx2.Value.ToString();
        }

        private void TrackBarBLDCx3_Scroll(object sender, EventArgs e)
        {
            textBoxBLDCx3.Text = trackBarBLDCx3.Value.ToString();
        }

        private void TrackBarPMSMSpedd_Scroll(object sender, EventArgs e)
        {
            textBoxPMSMSp.Text = trackBarPMSMSpeed.Value.ToString();
        }

        private void TrackBarPMSMTorque_Scroll(object sender, EventArgs e)
        {
            textBoxPMSMTorque.Text = trackBarPMSMTorque.Value.ToString();
        }

        private void TrackBarPMSMx2_Scroll(object sender, EventArgs e)
        {
            textBoxPMSMx2.Text = trackBarPMSMx2.Value.ToString();
        }

        private void TrackBarPMSMx1_Scroll(object sender, EventArgs e)
        {
            textBoxPMSMx1.Text = trackBarPMSMx1.Value.ToString();
        }

        private void TrackBarPMSMx3_Scroll(object sender, EventArgs e)
        {
            textBoxPMSMx3.Text = trackBarPMSMx3.Value.ToString();
        }
    }
    
}