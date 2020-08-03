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
using System.Xml.Serialization;
using System.IO;

namespace Praca_Inżynierska
{
    public partial class Form1 : Form
    {

        public SerialPort serialPort = new SerialPort();                           //zarezerwowanie nazwy zmiennej portu seryjnego
        int duration = 0;                                               //ilość tików czasu = 0
        public bool start = false;                                              //zmienna start, domyślnie ustawiona na false.
        public byte slaveAddress = 9;                                          //adres urządzenia slave
        public ushort readStartAddress = 4000;                                 //
        public ushort writeStartAddress;                                        //
        public ushort numberOfPoints = 20;                                     //ilość adresów rejestru, które program ma obsłużyć
        public List<Excel_Export> export = new List<Excel_Export>();    //przypisanie zmiennej export do listy klasy Excel Export
        public Settings settings = new Settings();
        public Form2 form2 = new Form2();

        public Form1()
        {
            try
            {
                InitializeComponent();
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
                string[] serialPorts = SerialPort.GetPortNames();             //pobranie listy dostępnych portów COM
                cBoxComPort.Items.AddRange(serialPorts);                      //pokazanie dostępnych portów w polu wyboru Port COM
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\config.xml", FileMode.Open, FileAccess.Read))
                {
                    settings = serializer.Deserialize(fs) as Settings;
                }
               
                textBoxRead4011.Text = settings.AddrM1SpeedR;
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
                serialPort.PortName = cBoxComPort.Text;
                serialPort.BaudRate = Convert.ToInt32(settings.CPBaudRate);
                serialPort.DataBits = Convert.ToInt32(settings.CPDataBits);
                serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), settings.CPParity);
                serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), settings.CPStopBits);
                serialPort.Open();
                progressBarComPort.Value = 100;                        //Progress bar zmienia kolor na zielony, jako oznaczenie poprawnego połączenia z portem COM.
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
                if (start == true)
                {
                    Stop();
                    Thread.Sleep(50);
                    ReadTEST();
                }
                else
                {
                    start = true;                       //ustawia wartość start na true
                                                        //Read();
                    ReadTEST();
                }
            }
            catch (TimeoutException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnWriteMultipleRegistersWord_Click(object sender, EventArgs e)
        {
            try
            {
                writeStartAddress = 4000;
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
                if (serialPort.IsOpen)
                {
                    IModbusMaster masterRtu = ModbusSerialMaster.CreateRtu(serialPort);         //tworzy połączenie do protokołu modbus przez port seryjny
                    int ID = 1;                                 //ustawia wartość ID do exportu wartości do listy Excel Export
                    duration = 0;                               //zeruje licznik ticków czasu
                    timer.Enabled = true;                       //włącza timer
                    timer.Start();                              //startuje timer
                    ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>                          //nie podejmuje następnego działania przed dostaniem informacji zwrotnej
                    {

                        masterRtu.Transport.Retries = 500;
                        masterRtu.Transport.ReadTimeout = 100;
                        while (start == true)                                                       //sprawdza czy zmianna start ma wartość true
                        {

                            ushort[] result1 = masterRtu.ReadHoldingRegisters(slaveAddress, readStartAddress, numberOfPoints);      //zczytuje wartosci z rejestrów dla adresu urządzenia slave, adresu rejestru i liczbie kolejnych adresów
                            export.Add(new Excel_Export { ID = ID, Time = Convert.ToString(duration), Speed = Convert.ToString(result1[0]), Torque = Convert.ToString(result1[1]), x1 = Convert.ToString(result1[2]), x2 = Convert.ToString(result1[3]), x3 = Convert.ToString(result1[4]) }); //dodaje wartości zczytane do listy Excel Export
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
                            duration = duration + 20;
                        }
                    }));
                }
                else
                {
                    MessageBox.Show("Port COM jest zamknięty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                start = false;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void ReadTEST()
        {
            try
            {

                if (serialPort.IsOpen)
                {
                    int speedsum = 0;
                    int ID = 1;
                    duration = 0;
                    timer.Enabled = true;
                    timer.Start();
                    ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
                    {
                        IModbusMaster masterRtu = ModbusSerialMaster.CreateRtu(serialPort);
                        masterRtu.Transport.Retries = 5000;
                        masterRtu.Transport.ReadTimeout = 100;
                        while (start == true)
                        {
                            speedsum = 0;
                            ushort[] result1 = masterRtu.ReadHoldingRegisters(slaveAddress, readStartAddress, numberOfPoints);
                            for (int i = 0; i < 5; i++)
                            {
                                
                                speedsum += result1[i];
                            }
                            textBoxRead4000.Invoke(new Action(delegate ()
                            {
                                textBoxRead4000.Text = Convert.ToString(result1[0]);
                                textBoxRead4001.Text = Convert.ToString(result1[1]);
                            }));

                            export.Add(new Excel_Export() { ID = ID, Speed = Convert.ToString(speedsum), Torque = Convert.ToString(result1[1]) });

                            ID = ID + 1;                // zwiększa ID    
                            Thread.Sleep(20);
                            duration = duration + 20;


                        }
                    }));
                }
                else
                {
                    MessageBox.Show("Port COM jest zamknięty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                start = false;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
                masterRtu.Transport.WriteTimeout = 50;
                masterRtu.Transport.Retries = 5;
                ushort[] WriteMulipleRegistersTable = { Convert.ToUInt16(textBoxWrite4001.Text), Convert.ToUInt16(textBoxWrite4002.Text), 0, 0, 0, 0, 0, 0, 0, Convert.ToUInt16(textBoxWrite4010.Text), Convert.ToUInt16(textBoxWrite4011.Text) }; //tworzy tabelę wartości z pól tekstowych 
                masterRtu.WriteMultipleRegisters(slaveAddress, writeStartAddress, WriteMulipleRegistersTable);      //wysyła wcześniej zdefiniowane wartości na konkretne adresy urządzenia
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            form2.ShowDialog();
        }

        private void BtnTEST_Click(object sender, EventArgs e)
        {
            //Form2 form2 = new Form2(cBoxBaudRate.Text);
            //form2.ShowDialog();
            //form2.GetData();
            //Thread.Sleep(1000);
        }
        private void BtnStartM2DC_Click(object sender, EventArgs e)
        {
            Write();
        }
        private void TrackBarM1Speed_Scroll(object sender, EventArgs e)
        {
            textBoxM1Speed.Text = trackBarM1Speed.Value.ToString();
        }

        private void TrackBarM1Position_Scroll(object sender, EventArgs e)
        {
            textBoxM1Position.Text = trackBarM1Position.Value.ToString();
        }

        private void TrackBarM1Torque_Scroll(object sender, EventArgs e)
        {
            textBoxM1Torque.Text = trackBarM1Torque.Value.ToString();
        }

        private void TrackBarM1Current_Scroll(object sender, EventArgs e)
        {
            textBoxM1Current.Text = trackBarM1Current.Value.ToString();
        }

        private void TrackBarM1Voltage_Scroll(object sender, EventArgs e)
        {
            textBoxM1Voltage.Text = trackBarM1Voltage.Value.ToString();
        }

        private void TrackBarM1P_Scroll(object sender, EventArgs e)
        {
            textBoxM1P.Text = trackBarM1P.Value.ToString();
        }

        private void TrackBarM1I_Scroll(object sender, EventArgs e)
        {
            textBoxM1I.Text = trackBarM1I.Value.ToString();
        }

        private void TrackBarM1D_Scroll(object sender, EventArgs e)
        {
            textBoxM1D.Text = trackBarM1D.Value.ToString();
        }

        private void TrackBarM1x1_Scroll(object sender, EventArgs e)
        {
            textBoxM1x1.Text = trackBarM1x1.Value.ToString();
        }

        private void TrackBarM1x2_Scroll(object sender, EventArgs e)
        {
            textBoxM1x2.Text = trackBarM1x2.Value.ToString();
        }

        private void TrackBarM1x3_Scroll(object sender, EventArgs e)
        {
            textBoxM1x3.Text = trackBarM1x3.Value.ToString();
        }



        private void TrackBarM2DCSpeed_Scroll(object sender, EventArgs e)
        {
            textBoxM2DCSpeed.Text = trackBarM2DCSpeed.Value.ToString();
        }

        private void TrackBarM2DCPosition_Scroll(object sender, EventArgs e)
        {
            textBoxM2DCPosition.Text = trackBarM2DCPosition.Value.ToString();
        }

        private void TrackBarM2DCTorque_Scroll(object sender, EventArgs e)
        {
            textBoxM2DCTorque.Text = trackBarM2DCTorque.Value.ToString();
        }

        private void TrackBarM2DCCurrent_Scroll(object sender, EventArgs e)
        {
            textBoxM2DCCurrent.Text = trackBarM2DCCurrent.Value.ToString();
        }

        private void TrackBarM2DCVoltage_Scroll(object sender, EventArgs e)
        {
            textBoxM2DCVoltage.Text = trackBarM2DCVoltage.Value.ToString();
        }

        private void TrackBarM2DCP_Scroll(object sender, EventArgs e)
        {
            textBoxM2DCP.Text = trackBarM2DCP.Value.ToString();
        }

        private void TrackBarM2DCI_Scroll(object sender, EventArgs e)
        {
            textBoxM2DCI.Text = trackBarM2DCI.Value.ToString();
        }

        private void TrackBarM2DCD_Scroll(object sender, EventArgs e)
        {
            textBoxM2DCD.Text = trackBarM2DCD.Value.ToString();
        }

        private void TrackBarM2DCx1_Scroll(object sender, EventArgs e)
        {
            textBoxM2DCx1.Text = trackBarM2DCx1.Value.ToString();
        }

        private void TrackBarM2DCx2_Scroll(object sender, EventArgs e)
        {
            textBoxM2DCx2.Text = trackBarM2DCx2.Value.ToString();
        }

        private void TrackBarM2DCx3_Scroll(object sender, EventArgs e)
        {
            textBoxM2DCx3.Text = trackBarM2DCx3.Value.ToString();
        }

        private void TrackBarM2AsynchSpeed_Scroll(object sender, EventArgs e)
        {
            textBoxM2AsynchSpeed.Text = trackBarM2AsynchSpeed.Value.ToString();
        }

        private void TrackBarM2AsynchPosition_Scroll(object sender, EventArgs e)
        {
            textBoxM2AsynchPosition.Text = trackBarM2AsynchPosition.Value.ToString();
        }

        private void TrackBarM2AsynchTorque_Scroll(object sender, EventArgs e)
        {
            textBoxM2AsynchTorque.Text = trackBarM2AsynchTorque.Value.ToString();
        }

        private void TrackBarM2AsynchCurrent_Scroll(object sender, EventArgs e)
        {
            textBoxM2AsynchCurrent.Text = trackBarM2AsynchCurrent.Value.ToString();
        }

        private void TrackBarM2AsynchVoltage_Scroll(object sender, EventArgs e)
        {
            textBoxM2AsynchVoltage.Text = trackBarM2AsynchVoltage.Value.ToString();
        }

        private void TrackBarM2AsynchP_Scroll(object sender, EventArgs e)
        {
            textBoxM2AsynchP.Text = trackBarM2AsynchP.Value.ToString();
        }

        private void TrackBarM2AsynchI_Scroll(object sender, EventArgs e)
        {
            textBoxM2AsynchI.Text = trackBarM2AsynchI.Value.ToString();
        }

        private void TrackBarM2AsynchD_Scroll(object sender, EventArgs e)
        {
            textBoxM2AsynchD.Text = trackBarM2AsynchD.Value.ToString();
        }

        private void TrackBarM2Asynchx1_Scroll(object sender, EventArgs e)
        {
            textBoxM2Asynchx1.Text = trackBarM2Asynchx1.Value.ToString();
        }

        private void TrackBarM2Asynchx2_Scroll(object sender, EventArgs e)
        {
            textBoxM2Asynchx2.Text = trackBarM2Asynchx2.Value.ToString();
        }

        private void TrackBarM2Asynchx3_Scroll(object sender, EventArgs e)
        {
            textBoxM2Asynchx3.Text = trackBarM2Asynchx3.Value.ToString();
        }

        private void TrackBarM2BLDCSpeed_Scroll(object sender, EventArgs e)
        {
            textBoxM2BLDCSpeed.Text = trackBarM2BLDCSpeed.Value.ToString();
        }

        private void TrackBarM2BLDCPosition_Scroll(object sender, EventArgs e)
        {
            textBoxM2BLDCPosition.Text = trackBarM2BLDCPosition.Value.ToString();
        }

        private void TrackBarM2BLDCTorque_Scroll(object sender, EventArgs e)
        {
            textBoxM2BLDCTorque.Text = trackBarM2BLDCTorque.Value.ToString();
        }

        private void TrackBarM2BLDCCurrent_Scroll(object sender, EventArgs e)
        {
            textBoxM2BLDCCurrent.Text = trackBarM2BLDCCurrent.Value.ToString();
        }

        private void TrackBarM2BLDCVoltage_Scroll_1(object sender, EventArgs e)
        {
            textBoxM2BLDCVoltage.Text = trackBarM2BLDCVoltage.Value.ToString();
        }

        private void TrackBarM2BLDCP_Scroll(object sender, EventArgs e)
        {
            textBoxM2BLDCP.Text = trackBarM2BLDCP.Value.ToString();
        }

        private void TrackBarM2BLDCI_Scroll(object sender, EventArgs e)
        {
            textBoxM2BLDCI.Text = trackBarM2BLDCI.Value.ToString();
        }

        private void TrackBarM2BLDCD_Scroll(object sender, EventArgs e)
        {
            textBoxM2BLDCD.Text = trackBarM2BLDCD.Value.ToString();
        }

        private void TrackBarM2BLDCx1_Scroll(object sender, EventArgs e)
        {
            textBoxM2BLDCx1.Text = trackBarM2BLDCx1.Value.ToString();
        }

        private void TrackBarM2BLDCx2_Scroll(object sender, EventArgs e)
        {
            textBoxM2BLDCx2.Text = trackBarM2BLDCx2.Value.ToString();
        }

        private void TrackBarM2BLDCx3_Scroll(object sender, EventArgs e)
        {
            textBoxM2BLDCx3.Text = trackBarM2BLDCx3.Value.ToString();
        }

        private void TrackBarM2PMSMSpeed_Scroll(object sender, EventArgs e)
        {
            textBoxM2PMSMSpeed.Text = trackBarM2PMSMSpeed.Value.ToString();
        }

        private void TrackBarM2PMSMPosition_Scroll(object sender, EventArgs e)
        {
            textBoxM2PMSMPosition.Text = trackBarM2PMSMPosition.Value.ToString();
        }

        private void TrackBarM2PMSMTorque_Scroll(object sender, EventArgs e)
        {
            textBoxM2PMSMTorque.Text = trackBarM2PMSMTorque.Value.ToString();
        }

        private void TrackBarM2PMSMCurrent_Scroll(object sender, EventArgs e)
        {
            textBoxM2PMSMCurrent.Text = trackBarM2PMSMCurrent.Value.ToString();
        }

        private void TrackBarM2PMSMVoltage_Scroll(object sender, EventArgs e)
        {
            textBoxM2PMSMVoltage.Text = trackBarM2PMSMVoltage.Value.ToString();
        }

        private void TrackBarM2PMSMP_Scroll(object sender, EventArgs e)
        {
            textBoxM2PMSMP.Text = trackBarM2PMSMP.Value.ToString();
        }

        private void TrackBarM2PMSMI_Scroll(object sender, EventArgs e)
        {
            textBoxM2PMSMI.Text = trackBarM2PMSMI.Value.ToString();
        }

        private void TrackBarM2PMSMD_Scroll(object sender, EventArgs e)
        {
            textBoxM2PMSMD.Text = trackBarM2PMSMD.Value.ToString();
        }

        private void TrackBarM2PMSMx1_Scroll(object sender, EventArgs e)
        {
            textBoxM2PMSMx1.Text = trackBarM2PMSMx1.Value.ToString();
        }

        private void TrackBarM2PMSMx2_Scroll(object sender, EventArgs e)
        {
            textBoxM2PMSMx2.Text = trackBarM2PMSMx2.Value.ToString();
        }

        private void TrackBarM2PMSMx3_Scroll(object sender, EventArgs e)
        {
            textBoxM2PMSMx3.Text = trackBarM2PMSMx3.Value.ToString();
        }

        private void BtnStartM1_Click(object sender, EventArgs e)
        {
            start = true;
            Read();
        }

        private void BtnConfigLoad_Click(object sender, EventArgs e)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\config" + comboBoxLoadConfig.Text + ".xml", FileMode.Open, FileAccess.Read))
                {
                settings = serializer.Deserialize(fs) as Settings;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

        private void RunChart()
        {
            while (start == true)
            {

                //zmienić pola wyoru na combo box i wyświetlać tylko jedną wartość na osi X. 
                //Wtedy w zależności od nazwy pola nazywać serie, wszystkie wartości trzymać w tabeli export żeby można było porównać różne warości po zakończeniu ćwiczenia.
                //zastanowiś się nad sensem pętli while, chyba że trzymać aż będzie dodawać inne wartości. 
            }
        }
        public void LoadReadAddress()
        {
            
        }
        public void Process()
        {
            
            int[] addrvalues = { Convert.ToInt32(settings.AddrM1SpeedR), Convert.ToInt32(settings.AddrM1PositionR), Convert.ToInt32(settings.AddrM1TorqueR), Convert.ToInt32(settings.AddrM1CurrentR), Convert.ToInt32(settings.AddrM1VoltageR), Convert.ToInt32(settings.AddrM1PR), Convert.ToInt32(settings.AddrM1IR), Convert.ToInt32(settings.AddrM1DR), Convert.ToInt32(settings.AddrM1x1R), Convert.ToInt32(settings.AddrM1x2R), Convert.ToInt32(settings.AddrM1x3R), Convert.ToInt32(settings.AddrM2DCSpeedR), Convert.ToInt32(settings.AddrM2DCPositionR), Convert.ToInt32(settings.AddrM2DCTorqueR), Convert.ToInt32(settings.AddrM2DCCurrentR), Convert.ToInt32(settings.AddrM2DCVoltageR), Convert.ToInt32(settings.AddrM2DCPR), Convert.ToInt32(settings.AddrM2DCIR), Convert.ToInt32(settings.AddrM2DCDR), Convert.ToInt32(settings.AddrM2DCx1R), Convert.ToInt32(settings.AddrM2DCx2R), Convert.ToInt32(settings.AddrM2DCx3R), Convert.ToInt32(settings.AddrM2AsynchSpeedR), Convert.ToInt32(settings.AddrM2AsynchPositionR), Convert.ToInt32(settings.AddrM2AsynchTorqueR), Convert.ToInt32(settings.AddrM2AsynchCurrentR), Convert.ToInt32(settings.AddrM2AsynchVoltageR), Convert.ToInt32(settings.AddrM2AsynchPR), Convert.ToInt32(settings.AddrM2AsynchIR), Convert.ToInt32(settings.AddrM2AsynchDR), Convert.ToInt32(settings.AddrM2Asynchx1R), Convert.ToInt32(settings.AddrM2Asynchx2R), Convert.ToInt32(settings.AddrM2Asynchx3R), Convert.ToInt32(settings.AddrM2BLDCSpeedR), Convert.ToInt32(settings.AddrM2BLDCPositionR), Convert.ToInt32(settings.AddrM2BLDCTorqueR), Convert.ToInt32(settings.AddrM2BLDCCurrentR), Convert.ToInt32(settings.AddrM2BLDCVoltageR), Convert.ToInt32(settings.AddrM2BLDCPR), Convert.ToInt32(settings.AddrM2BLDCIR), Convert.ToInt32(settings.AddrM2BLDCDR), Convert.ToInt32(settings.AddrM2BLDCx1R), Convert.ToInt32(settings.AddrM2BLDCx2R), Convert.ToInt32(settings.AddrM2BLDCx3R), Convert.ToInt32(settings.AddrM2PMSMSpeedR), Convert.ToInt32(settings.AddrM2PMSMPositionR), Convert.ToInt32(settings.AddrM2PMSMTorqueR), Convert.ToInt32(settings.AddrM2PMSMCurrentR), Convert.ToInt32(settings.AddrM2PMSMVoltageR), Convert.ToInt32(settings.AddrM2PMSMPR), Convert.ToInt32(settings.AddrM2PMSMIR), Convert.ToInt32(settings.AddrM2PMSMDR), Convert.ToInt32(settings.AddrM2PMSMx1R), Convert.ToInt32(settings.AddrM2PMSMx2R), Convert.ToInt32(settings.AddrM2PMSMx3R) };
            int[] nopvalues = { Convert.ToInt32(settings.AddrM1SpeedRNOP), Convert.ToInt32(settings.AddrM1PositionRNOP), Convert.ToInt32(settings.AddrM1TorqueRNOP), Convert.ToInt32(settings.AddrM1CurrentRNOP), Convert.ToInt32(settings.AddrM1VoltageRNOP), Convert.ToInt32(settings.AddrM1PRNOP), Convert.ToInt32(settings.AddrM1IRNOP), Convert.ToInt32(settings.AddrM1DRNOP), Convert.ToInt32(settings.AddrM1x1RNOP), Convert.ToInt32(settings.AddrM1x2RNOP), Convert.ToInt32(settings.AddrM1x3RNOP), Convert.ToInt32(settings.AddrM2DCSpeedRNOP), Convert.ToInt32(settings.AddrM2DCPositionRNOP), Convert.ToInt32(settings.AddrM2DCTorqueRNOP), Convert.ToInt32(settings.AddrM2DCCurrentRNOP), Convert.ToInt32(settings.AddrM2DCVoltageRNOP), Convert.ToInt32(settings.AddrM2DCPRNOP), Convert.ToInt32(settings.AddrM2DCIRNOP), Convert.ToInt32(settings.AddrM2DCDRNOP), Convert.ToInt32(settings.AddrM2DCx1RNOP), Convert.ToInt32(settings.AddrM2DCx2RNOP), Convert.ToInt32(settings.AddrM2DCx3RNOP), Convert.ToInt32(settings.AddrM2AsynchSpeedRNOP), Convert.ToInt32(settings.AddrM2AsynchPositionRNOP), Convert.ToInt32(settings.AddrM2AsynchTorqueRNOP), Convert.ToInt32(settings.AddrM2AsynchCurrentRNOP), Convert.ToInt32(settings.AddrM2AsynchVoltageRNOP), Convert.ToInt32(settings.AddrM2AsynchPRNOP), Convert.ToInt32(settings.AddrM2AsynchIRNOP), Convert.ToInt32(settings.AddrM2AsynchDRNOP), Convert.ToInt32(settings.AddrM2Asynchx1RNOP), Convert.ToInt32(settings.AddrM2Asynchx2RNOP), Convert.ToInt32(settings.AddrM2Asynchx3RNOP), Convert.ToInt32(settings.AddrM2BLDCSpeedRNOP), Convert.ToInt32(settings.AddrM2BLDCPositionRNOP), Convert.ToInt32(settings.AddrM2BLDCTorqueRNOP), Convert.ToInt32(settings.AddrM2BLDCCurrentRNOP), Convert.ToInt32(settings.AddrM2BLDCVoltageRNOP), Convert.ToInt32(settings.AddrM2BLDCPRNOP), Convert.ToInt32(settings.AddrM2BLDCIRNOP), Convert.ToInt32(settings.AddrM2BLDCDRNOP), Convert.ToInt32(settings.AddrM2BLDCx1RNOP), Convert.ToInt32(settings.AddrM2BLDCx2RNOP), Convert.ToInt32(settings.AddrM2BLDCx3RNOP), Convert.ToInt32(settings.AddrM2PMSMSpeedRNOP), Convert.ToInt32(settings.AddrM2PMSMPositionRNOP), Convert.ToInt32(settings.AddrM2PMSMTorqueRNOP), Convert.ToInt32(settings.AddrM2PMSMCurrentRNOP), Convert.ToInt32(settings.AddrM2PMSMVoltageRNOP), Convert.ToInt32(settings.AddrM2PMSMPRNOP), Convert.ToInt32(settings.AddrM2PMSMIRNOP), Convert.ToInt32(settings.AddrM2PMSMDRNOP), Convert.ToInt32(settings.AddrM2PMSMx1RNOP), Convert.ToInt32(settings.AddrM2PMSMx2RNOP), Convert.ToInt32(settings.AddrM2PMSMx3RNOP) };
            
            for (int i = 0; i < Convert.ToInt32(settings.ReadGenNOP); i ++)
            {

            }
        }
    }
    
}