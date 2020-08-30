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
using System.Windows.Forms.DataVisualization.Charting;

namespace Praca_Inżynierska
{
    public partial class Form1 : Form
    {

        public SerialPort serialPort = new SerialPort();                           //zarezerwowanie nazwy zmiennej portu seryjnego
        int duration = 0;                                                           //ilość tików czasu = 0
        public bool readStart = false;                                              //zmienna start, domyślnie ustawiona na false.
        public bool writeStart = false;                                         
        public byte slaveAddress = 0;                                               //rezerwuje zmienną dla Adresu slave
        public ushort readStartAddress = 0;                                         //rezerwuje zmienną dla Adresu read
        public ushort writeStartAddress;                                            //rezerwuje zmienną dla Adresu write
        public ushort numberOfPoints = 0;                                           //ilość adresów rejestru, które program ma obsłużyć
        public List<Data> export = new List<Data>();                                //przypisanie zmiennej export do listy klasy Data
        public Settings settings = new Settings();                                  //przypisanie zmiennej settings dla klasy settings
        public Form2 form2 = new Form2();                                           //przypisanie zmiennej form2 do Formularza drugiego
        public bool startM1 = false;                                                //przypisanie zmiennej startu silnika M1
        public bool startM2 = false;                                                //przypisanie zmiennej startu silnika M2
        public bool M2DC = false;                                                   //przypisanie zmiennej startu silnika M2 DC
        public bool M2Asynch = false;                                               //przypisanie zmiennej startu silnika M2 Asynchronicznego
        public bool M2BLDC = false;                                                 //przypisanie zmiennej startu silnika M2 BLDC
        public bool M2PMSM = false;                                                 //przypisanie zmiennej startu silnika M2 PMSM
        public bool chartStart = false;                                             //przypisanie zmiennej startu wykresu



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
                readStart = false;                                             //wyłącza komunikacje poprzed protokół modbus
                string[] serialPorts = SerialPort.GetPortNames();             //pobranie listy dostępnych portów COM
                cBoxComPort.Items.AddRange(serialPorts);                      //pokazanie dostępnych portów w polu wyboru Port COM
                LockSettings();                                                 //wyłącza wszystkie pola przed wgraniem konfiguracji

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void BtnOpenComPort_Click(object sender, EventArgs e)
        {                           
            try
            {
                serialPort.PortName = cBoxComPort.Text;                                             //pobiera nazwę portu z pola
                serialPort.BaudRate = Convert.ToInt32(settings.CPBaudRate);                         //pobiera baud rate zdefiniowany w formularzu 2
                serialPort.DataBits = Convert.ToInt32(settings.CPDataBits);                         //pobiera data bits rate zdefiniowane w formularzu 2
                serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), settings.CPParity);          //pobiera parity rate zdefiniowany w formularzu 2
                serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), settings.CPStopBits);  //pobiera stop bits rate zdefiniowane w formularzu 2
                serialPort.Open();                                                                  //otwiera port com dla wyżej zdefiniowanych wartości
                progressBarComPort.Value = 100;                        //Progress bar zmienia kolor na zielony, jako oznaczenie poprawnego połączenia z portem COM.
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBarComPort.Value = 0;                           //W wypadku wystąpienia błędu, ustawia progress bar na 0
            }
        }

        private void BtnCloseComPort_Click(object sender, EventArgs e)
        {
            try
            {
                Stop();                                                 //kończy komunikację przez protokół modbus
                serialPort.Close();                                     //zamyka port COM
                progressBarComPort.Value = 0;                           //ustawia progress bar na 0
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
        private void ReadTEST()             //metoda odpowiada za odbiór danych z rejestrów protokołu modbus
        {
            try
            {

                if (serialPort.IsOpen)      //sprawdza czy port com jest otwarty
                {
                    int speedSumM1 = 0;
                    int positionSumM1 = 0;
                    int torqueSumM1 = 0;
                    int currentSumM1 = 0;
                    int voltageSumM1 = 0;
                    int PsumM1 = 0;
                    int IsumM1 = 0;
                    int DsumM1 = 0;
                    int x1SumM1 = 0;
                    int x2SumM1 = 0;
                    int x3SumM1 = 0;
                    int speedSumM2 = 0;
                    int positionSumM2 = 0;
                    int torqueSumM2 = 0;
                    int currentSumM2 = 0;
                    int voltageSumM2 = 0;
                    int frequencySumM2 = 0;
                    int PsumM2 = 0;
                    int IsumM2 = 0;
                    int DsumM2 = 0;
                    int x1SumM2 = 0;
                    int x2SumM2 = 0;
                    int x3SumM2 = 0;
                    int ID = 1;
                    slaveAddress = Convert.ToByte(settings.SlaveAddress);           //przypisuje do zmiennej wartość slave adress z klasy settings
                    readStartAddress = Convert.ToUInt16(settings.ReadGenAddr);      //przypisuje do zmiennej wartość read adress z klasy settings
                    numberOfPoints = Convert.ToUInt16(settings.ReadGenNOP);         //przypisuje do zmiennej wartość number of points z klasy settings


                    duration = 0;                                                   //ustawia czas na 0
                    timer.Enabled = true;                                           //odblokowuje timer
                    timer.Start();                                                  // startuje timer
                    ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
                    {
                        IModbusMaster masterRtu = ModbusSerialMaster.CreateRtu(serialPort);     //tworzy połączenie z urządzeniem przez konkretny port com 
                        masterRtu.Transport.Retries = 5000;                                     //ustawia ilość powtarzanych połączeń z urządzeniem przed stratą połączenia
                        masterRtu.Transport.ReadTimeout = 100;                                  //czas do timeoutu       
                        masterRtu.Transport.WaitToRetryMilliseconds = 10;                       //ile bęma czekać na ponowne wysłanei wiadomości przez protokół

                        while (readStart == true)                                               //dopuki readstart m wartość true, będzie wysyłał wiadomość
                        {
                            if (writeStart == false)                                            //sprawdza czy akurat nie zapisujemy rejestrów
                            {
                                ushort[] result1 = masterRtu.ReadHoldingRegisters(slaveAddress, readStartAddress,numberOfPoints);   //wysyła wiadomość z prośbą o odczyt rejestrów
                                if (startM1 == true)                                                                                //sprawdza czy silnik M1 powinien działać
                                {
                                    if (settings.CheckM1SpeedR == true)                                                             //sprawdza czy wartość speed ma zostać zczytana
                                    {   
                                        speedSumM1 = 0;                                                                             //zeruje wartość zmiennej speedSumM1                                                 
                                        for (int i = Convert.ToInt32(settings.AddrM1SpeedR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM1SpeedR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM1SpeedRNOP); i++)
                                        {
                                            speedSumM1 += result1[i];               //dla każdej odpowiedzi dla konkretnych adresów sumuje wartości prędkości
                                        }
                                    }
                                    if (settings.CheckM1PositionR == true)
                                    {
                                        positionSumM1 = 0;
                                        for (int i = Convert.ToInt32(settings.AddrM1PositionR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM1PositionR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM1PositionRNOP); i++)
                                        {
                                            positionSumM1 += result1[i];
                                        }
                                    }
                                    if (settings.CheckM1TorqueR == true)
                                    {
                                        torqueSumM1 = 0;
                                        for (int i = Convert.ToInt32(settings.AddrM1TorqueR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM1TorqueR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM1TorqueRNOP); i++)
                                        {
                                            torqueSumM1 += result1[i];
                                        }
                                    }
                                    if (settings.CheckM1CurrentR == true)
                                    {
                                        currentSumM1 = 0;
                                        for (int i = Convert.ToInt32(settings.AddrM1CurrentR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM1CurrentR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM1CurrentRNOP); i++)
                                        {
                                            currentSumM1 += result1[i];
                                        }
                                    }
                                    if (settings.CheckM1VoltageR == true)
                                    {
                                        voltageSumM1 = 0;
                                        for (int i = Convert.ToInt32(settings.AddrM1VoltageR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM1VoltageR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM1VoltageRNOP); i++)
                                        {
                                            voltageSumM1 += result1[i];
                                        }
                                    }
                                    if (settings.CheckM1PR == true)
                                    {
                                        PsumM1 = 0;
                                        for (int i = Convert.ToInt32(settings.AddrM1PR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM1PR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM1PRNOP); i++)
                                        {
                                            PsumM1 += result1[i];
                                        }
                                    }
                                    if (settings.CheckM1IR == true)
                                    {
                                        IsumM1 = 0;
                                        for (int i = Convert.ToInt32(settings.AddrM1IR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM1IR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM1IRNOP); i++)
                                        {
                                            IsumM1 += result1[i];
                                        }
                                    }
                                    if (settings.CheckM1DR == true)
                                    {
                                        DsumM1 = 0;
                                        for (int i = Convert.ToInt32(settings.AddrM1DR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM1DR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM1DRNOP); i++)
                                        {
                                            DsumM1 += result1[i];
                                        }
                                    }
                                    if (settings.CheckM1x1R == true)
                                    {
                                        x1SumM1 = 0;
                                        for (int i = Convert.ToInt32(settings.AddrM1x1R) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM1x1R) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM1x1RNOP); i++)
                                        {
                                            x1SumM1 += result1[i];
                                        }
                                    }
                                    if (settings.CheckM1x2R == true)
                                    {
                                        x2SumM1 = 0;
                                        for (int i = Convert.ToInt32(settings.AddrM1x2R) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM1x2R) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM1x2RNOP); i++)
                                        {
                                            x2SumM1 += result1[i];
                                        }
                                    }
                                    if (settings.CheckM1x3R == true)
                                    {
                                        x3SumM1 = 0;
                                        for (int i = Convert.ToInt32(settings.AddrM1x3R) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM1x3R) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM1x3RNOP); i++)
                                        {
                                            x3SumM1 += result1[i];
                                        }
                                    }

                                }
                                if (startM2 == true)
                                {
                                    if (M2DC == true)
                                    {
                                        if (settings.CheckM2DCSpeedR == true)
                                        {
                                            speedSumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2DCSpeedR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2DCSpeedR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2DCSpeedRNOP); i++)
                                            {
                                                speedSumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2DCPositionR == true)
                                        {
                                            positionSumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2DCPositionR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2DCPositionR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2DCPositionRNOP); i++)
                                            {
                                                positionSumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2DCTorqueR == true)
                                        {
                                            torqueSumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2DCTorqueR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2DCTorqueR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2DCTorqueRNOP); i++)
                                            {
                                                torqueSumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2DCCurrentR == true)
                                        {
                                            currentSumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2DCCurrentR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2DCCurrentR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2DCCurrentRNOP); i++)
                                            {
                                                currentSumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2DCVoltageR == true)
                                        {
                                            voltageSumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2DCVoltageR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2DCVoltageR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2DCVoltageRNOP); i++)
                                            {
                                                voltageSumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2DCPR == true)
                                        {
                                            PsumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2DCPR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2DCPR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2DCPRNOP); i++)
                                            {
                                                PsumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2DCIR == true)
                                        {
                                            IsumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2DCIR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2DCIR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2DCIRNOP); i++)
                                            {
                                                IsumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2DCDR == true)
                                        {
                                            DsumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2DCDR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2DCDR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2DCDRNOP); i++)
                                            {
                                                DsumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2DCx1R == true)
                                        {
                                            x1SumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2DCx1R) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2DCx1R) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2DCx1RNOP); i++)
                                            {
                                                x1SumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2DCx2R == true)
                                        {
                                            x2SumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2DCx2R) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2DCx2R) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2DCx2RNOP); i++)
                                            {
                                                x2SumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2DCx3R == true)
                                        {
                                            x3SumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2DCx3R) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2DCx3R) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2DCx3RNOP); i++)
                                            {
                                                x3SumM2 += result1[i];
                                            }
                                        }
                                    }
                                    if (M2Asynch == true)
                                    {
                                        if (settings.CheckM2AsynchSpeedR == true)
                                        {
                                            speedSumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2AsynchSpeedR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2AsynchSpeedR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2AsynchSpeedRNOP); i++)
                                            {
                                                speedSumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2AsynchPositionR == true)
                                        {
                                            positionSumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2AsynchPositionR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2AsynchPositionR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2AsynchPositionRNOP); i++)
                                            {
                                                positionSumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2AsynchTorqueR == true)
                                        {
                                            torqueSumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2AsynchTorqueR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2AsynchTorqueR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2AsynchTorqueRNOP); i++)
                                            {
                                                torqueSumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2AsynchCurrentR == true)
                                        {
                                            currentSumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2AsynchCurrentR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2AsynchCurrentR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2AsynchCurrentRNOP); i++)
                                            {
                                                currentSumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2AsynchVoltageR == true)
                                        {
                                            voltageSumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2AsynchVoltageR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2AsynchVoltageR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2AsynchVoltageRNOP); i++)
                                            {
                                                voltageSumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2AsynchFrequencyR == true)
                                        {
                                            frequencySumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2AsynchFrequencyR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2AsynchFrequencyR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2AsynchFrequencyRNOP); i++)
                                            {
                                                frequencySumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2AsynchPR == true)
                                        {
                                            PsumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2AsynchPR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2AsynchPR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2AsynchPRNOP); i++)
                                            {
                                                PsumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2AsynchIR == true)
                                        {
                                            IsumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2AsynchIR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2AsynchIR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2AsynchIRNOP); i++)
                                            {
                                                IsumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2AsynchDR == true)
                                        {
                                            DsumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2AsynchDR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2AsynchDR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2AsynchDRNOP); i++)
                                            {
                                                DsumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2Asynchx1R == true)
                                        {
                                            x1SumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2Asynchx1R) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2Asynchx1R) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2Asynchx1RNOP); i++)
                                            {
                                                x1SumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2Asynchx2R == true)
                                        {
                                            x2SumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2Asynchx2R) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2Asynchx2R) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2Asynchx2RNOP); i++)
                                            {
                                                x2SumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2Asynchx3R == true)
                                        {
                                            x3SumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2Asynchx3R) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2Asynchx3R) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2Asynchx3RNOP); i++)
                                            {
                                                x3SumM2 += result1[i];
                                            }
                                        }
                                    }
                                    if (M2BLDC == true)
                                    {
                                        if (settings.CheckM2BLDCSpeedR == true)
                                        {
                                            speedSumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2BLDCSpeedR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2BLDCSpeedR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2BLDCSpeedRNOP); i++)
                                            {
                                                speedSumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2BLDCPositionR == true)
                                        {
                                            positionSumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2BLDCPositionR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2BLDCPositionR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2BLDCPositionRNOP); i++)
                                            {
                                                positionSumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2BLDCTorqueR == true)
                                        {
                                            torqueSumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2BLDCTorqueR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2BLDCTorqueR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2BLDCTorqueRNOP); i++)
                                            {
                                                torqueSumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2BLDCCurrentR == true)
                                        {
                                            currentSumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2BLDCCurrentR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2BLDCCurrentR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2BLDCCurrentRNOP); i++)
                                            {
                                                currentSumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2BLDCVoltageR == true)
                                        {
                                            voltageSumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2BLDCVoltageR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2BLDCVoltageR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2BLDCVoltageRNOP); i++)
                                            {
                                                voltageSumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2BLDCPR == true)
                                        {
                                            PsumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2BLDCPR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2BLDCPR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2BLDCPRNOP); i++)
                                            {
                                                PsumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2BLDCIR == true)
                                        {
                                            IsumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2BLDCIR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2BLDCIR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2BLDCIRNOP); i++)
                                            {
                                                IsumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2BLDCDR == true)
                                        {
                                            DsumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2BLDCDR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2BLDCDR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2BLDCDRNOP); i++)
                                            {
                                                DsumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2BLDCx1R == true)
                                        {
                                            x1SumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2BLDCx1R) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2BLDCx1R) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2BLDCx1RNOP); i++)
                                            {
                                                x1SumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2BLDCx2R == true)
                                        {
                                            x2SumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2BLDCx2R) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2BLDCx2R) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2BLDCx2RNOP); i++)
                                            {
                                                x2SumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2BLDCx3R == true)
                                        {
                                            x3SumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2BLDCx3R) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2BLDCx3R) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2BLDCx3RNOP); i++)
                                            {
                                                x3SumM2 += result1[i];
                                            }
                                        }
                                    }
                                    if (M2PMSM == true)
                                    {
                                        if (settings.CheckM2PMSMSpeedR == true)
                                        {
                                            speedSumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2PMSMSpeedR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2PMSMSpeedR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2PMSMSpeedRNOP); i++)
                                            {
                                                speedSumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2PMSMPositionR == true)
                                        {
                                            positionSumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2PMSMPositionR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2PMSMPositionR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2PMSMPositionRNOP); i++)
                                            {
                                                positionSumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2PMSMTorqueR == true)
                                        {
                                            torqueSumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2PMSMTorqueR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2PMSMTorqueR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2PMSMTorqueRNOP); i++)
                                            {
                                                torqueSumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2PMSMCurrentR == true)
                                        {
                                            currentSumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2PMSMCurrentR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2PMSMCurrentR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2PMSMCurrentRNOP); i++)
                                            {
                                                currentSumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2PMSMVoltageR == true)
                                        {
                                            voltageSumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2PMSMVoltageR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2PMSMVoltageR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2PMSMVoltageRNOP); i++)
                                            {
                                                voltageSumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2PMSMFrequencyR == true)
                                        {
                                            frequencySumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2PMSMFrequencyR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2PMSMFrequencyR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2PMSMFrequencyRNOP); i++)
                                            {
                                                frequencySumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2PMSMPR == true)
                                        {
                                            PsumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2PMSMPR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2PMSMPR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2PMSMPRNOP); i++)
                                            {
                                                PsumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2PMSMIR == true)
                                        {
                                            IsumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2PMSMIR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2PMSMIR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2PMSMIRNOP); i++)
                                            {
                                                IsumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2PMSMDR == true)
                                        {
                                            DsumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2PMSMDR) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2PMSMDR) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2PMSMDRNOP); i++)
                                            {
                                                DsumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2PMSMx1R == true)
                                        {
                                            x1SumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2PMSMx1R) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2PMSMx1R) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2PMSMx1RNOP); i++)
                                            {
                                                x1SumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2PMSMx2R == true)
                                        {
                                            x2SumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2PMSMx2R) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2PMSMx2R) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2PMSMx2RNOP); i++)
                                            {
                                                x2SumM2 += result1[i];
                                            }
                                        }
                                        if (settings.CheckM2PMSMx3R == true)
                                        {
                                            x3SumM2 = 0;
                                            for (int i = Convert.ToInt32(settings.AddrM2PMSMx3R) - Convert.ToInt32(settings.ReadGenAddr); i < Convert.ToInt32(settings.AddrM2PMSMx3R) - Convert.ToInt32(settings.ReadGenAddr) + Convert.ToInt32(settings.AddrM2PMSMx3RNOP); i++)
                                            {
                                                x3SumM2 += result1[i];
                                            }
                                        }
        
                                    }
                                }
                                export.Add(new Data() { ID = ID, M1Speed = Convert.ToString(speedSumM1), M1Position = Convert.ToString(positionSumM1), M1Torque = Convert.ToString(torqueSumM1), M1Current = Convert.ToString(currentSumM1), M1Voltage = Convert.ToString(voltageSumM1), M1P = Convert.ToString(PsumM1), M1I = Convert.ToString(IsumM1), M1D = Convert.ToString(DsumM1), M1x1 = Convert.ToString(x1SumM1), M1x2 = Convert.ToString(x2SumM1), M1x3 = Convert.ToString(x3SumM1), M2Speed = Convert.ToString(speedSumM2), M2Position = Convert.ToString(positionSumM2), M2Torque = Convert.ToString(torqueSumM2), M2Current = Convert.ToString(currentSumM2), M2Voltage = Convert.ToString(voltageSumM2),M2Frequency = Convert.ToString(frequencySumM2), M2P = Convert.ToString(PsumM2), M2I = Convert.ToString(IsumM2), M2D = Convert.ToString(DsumM2), M2x1 = Convert.ToString(x1SumM2), M2x2 = Convert.ToString(x2SumM2), M2x3 = Convert.ToString(x3SumM2) });
                                //dodajemy wszystkie zsumowane wartości do listy export stworzonej na bazie klasy Data
                                readM1Speed.Invoke(new Action(delegate ()                   //invokuje nowe okna
                                {
                                    readM1Speed.Text = Convert.ToString(speedSumM1);        //wyświetla aktualne wartości zmiennych w polu tekstowym
                                    readM1Position.Text = Convert.ToString(positionSumM1);
                                    readM1Torque.Text = Convert.ToString(torqueSumM1);
                                    readM1Current.Text = Convert.ToString(currentSumM1);
                                    readM1Voltage.Text = Convert.ToString(voltageSumM1);
                                    readM1Power.Text = Convert.ToString(currentSumM1*voltageSumM1);
                                    readM1x1.Text = Convert.ToString(x1SumM1);
                                    readM1x2.Text = Convert.ToString(x2SumM1);
                                    readM1x3.Text = Convert.ToString(x3SumM1);
                                    readM2Speed.Text = Convert.ToString(speedSumM2);
                                    readM2Position.Text = Convert.ToString(positionSumM2);
                                    readM2Torque.Text = Convert.ToString(torqueSumM2);
                                    readM2Current.Text = Convert.ToString(currentSumM2);
                                    readM2Voltage.Text = Convert.ToString(voltageSumM2);
                                    readM2Power.Text = Convert.ToString(currentSumM2 * voltageSumM2);
                                    readM2x1.Text = Convert.ToString(x1SumM2);
                                    readM2x2.Text = Convert.ToString(x2SumM2);
                                    readM2x3.Text = Convert.ToString(x3SumM2);
                                    Runchart();             //uruchamia metodę odpowiedzialną za wykresy
                                }));
                                ID = ID + 1;                // zwiększa ID    
                                Thread.Sleep(20);           //opóźnienie 20min
                            }
                        }
                    }));
                }
                else
                {
                    MessageBox.Show("Port COM jest zamknięty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // wyświetla błąd w wypadku zamkniętego portu com
                }
            }
            catch (Exception ex)
            {
                readStart = false;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Stop()  //metoda zatrzymująca wszystkie działania związane z komunikacją z urządzeniem, nie zamykająca portu com
        {
            readStart = false;
            writeStart = false;
            startM1 = false;
            startM2 = false;
            M2DC = false;
            M2Asynch = false;
            M2BLDC = false;
            M2PMSM = false;
            chartStart = false;
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            form2.ShowDialog();         //otwiera formularz 2
        }
        private void BtnStartM2DC_Click(object sender, EventArgs e) //uruchamia komunikacje przypisaną do silnika M2
        {
            Stop();
            chartStart = true;
            startM1 = true;
            startM2 = true;
            M2DC = true;
            readStart = true;
            ReadTEST();
        }
        private void TrackBarM1Speed_Scroll(object sender, EventArgs e)
        {
            textBoxM1Speed.Text = trackBarM1Speed.Value.ToString();             //w wypadku ruchu suwakiem, zmienia dopisane wartości w polu tekstowym
        }

        private void TrackBarM1Position_Scroll(object sender, EventArgs e)
        {
            textBoxM1Position.Text = trackBarM1Position.Value.ToString();
            TestWrite();
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

            Stop();
            startM1 = true;
            readStart = true;
            chartStart = true;
            ReadTEST();

        }

        private void BtnConfigLoad_Click(object sender, EventArgs e)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));         //tworzy zmienną serializer jako nową klasę XMLSerializer z zmiennymi klasy Settings
                using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\config" + comboBoxLoadConfig.Text + ".xml", FileMode.Open, FileAccess.Read))  //otwiera plik configuracyjny określony w polu comboBoxLoadConfig  
                {
                    settings = serializer.Deserialize(fs) as Settings;          //dodaje wartości z pliku do zmiennych klasy settings
                }

                EnableDisable();                //metoda odpowiedzialna za odblokowywanie i zablokowywanie okien configuracji.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void TestWrite()
        {
            try
            {
                while (writeStart == true)
                {
                    IModbusMaster masterRtu = ModbusSerialMaster.CreateRtu(serialPort);            //tworzy połączenie do protokołu modbus przez port seryjny      
                    masterRtu.Transport.WriteTimeout = 500;
                    masterRtu.Transport.Retries = 5000;
                    masterRtu.Transport.WaitToRetryMilliseconds = 5;

                    ushort[] WriteMulipleRegistersTable = new ushort[Convert.ToInt16(settings.WriteGenNOP)];        //tworzy matrycę WriteMultiple registers z ilością pół określoną w formularzu konfiguracyjnym
                    if (settings.CheckM1SpeedS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM1SpeedS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM1Speed.Text); //nadpisuje konkretne pola wartościami ustawionymi w polach tekstowych
                    }
                    if (settings.CheckM1PositionS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM1PositionS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM1Position.Text);
                    }
                    if (settings.CheckM1TorqueS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM1TorqueS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM1Torque.Text);
                    }
                    if (settings.CheckM1CurrentS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM1CurrentS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM1Current.Text);
                    }
                    if (settings.CheckM1VoltageS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM1VoltageS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM1Voltage.Text);
                    }
                    if (settings.CheckM1PS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM1PS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM1P.Text);
                    }
                    if (settings.CheckM1IS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM1IS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM1I.Text);
                    }
                    if (settings.CheckM1DS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM1DS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM1D.Text);
                    }
                    if (settings.CheckM1x1S == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM1x1S) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM1x1.Text);
                    }
                    if (settings.CheckM1x2S == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM1x2S) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM1x2.Text);
                    }
                    if (settings.CheckM1x3S == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM1x3S) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM1x3.Text);
                    }
                    if (settings.CheckM2DCSpeedS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2DCSpeedS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2DCSpeed.Text);
                    }
                    if (settings.CheckM2DCPositionS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2DCPositionS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2DCPosition.Text);
                    }
                    if (settings.CheckM2DCTorqueS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2DCTorqueS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2DCTorque.Text);
                    }
                    if (settings.CheckM2DCCurrentS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2DCCurrentS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2DCCurrent.Text);
                    }
                    if (settings.CheckM2DCVoltageS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2DCVoltageS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2DCVoltage.Text);
                    }
                    if (settings.CheckM2DCPS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2DCPS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2DCP.Text);
                    }
                    if (settings.CheckM2DCIS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2DCIS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2DCI.Text);
                    }
                    if (settings.CheckM2DCDS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2DCDS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2DCD.Text);
                    }
                    if (settings.CheckM2DCx1S == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2DCx1S) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2DCx1.Text);
                    }
                    if (settings.CheckM2DCx2S == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2DCx2S) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2DCx2.Text);
                    }
                    if (settings.CheckM2DCx3S == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2DCx3S) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2DCx3.Text);
                    }
                    if (settings.CheckM2AsynchSpeedS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2AsynchSpeedS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2AsynchSpeed.Text);
                    }
                    if (settings.CheckM2AsynchPositionS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2AsynchPositionS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2AsynchPosition.Text);
                    }
                    if (settings.CheckM2AsynchTorqueS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2AsynchTorqueS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2AsynchTorque.Text);
                    }
                    if (settings.CheckM2AsynchCurrentS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2AsynchCurrentS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2AsynchCurrent.Text);
                    }
                    if (settings.CheckM2AsynchVoltageS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2AsynchVoltageS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2AsynchVoltage.Text);
                    }
                    if (settings.CheckM2AsynchPS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2AsynchPS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2AsynchP.Text);
                    }
                    if (settings.CheckM2AsynchIS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2AsynchIS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2AsynchI.Text);
                    }
                    if (settings.CheckM2AsynchDS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2AsynchDS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2AsynchD.Text);
                    }
                    if (settings.CheckM2Asynchx1S == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2Asynchx1S) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2Asynchx1.Text);
                    }
                    if (settings.CheckM2Asynchx2S == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2Asynchx2S) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2Asynchx2.Text);
                    }
                    if (settings.CheckM2Asynchx3S == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2Asynchx3S) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2Asynchx3.Text);
                    }
                    if (settings.CheckM2BLDCSpeedS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2BLDCSpeedS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2BLDCSpeed.Text);
                    }
                    if (settings.CheckM2BLDCPositionS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2BLDCPositionS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2BLDCPosition.Text);
                    }
                    if (settings.CheckM2BLDCTorqueS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2BLDCTorqueS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2BLDCTorque.Text);
                    }
                    if (settings.CheckM2BLDCCurrentS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2BLDCCurrentS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2BLDCCurrent.Text);
                    }
                    if (settings.CheckM2BLDCVoltageS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2BLDCVoltageS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2BLDCVoltage.Text);
                    }
                    if (settings.CheckM2BLDCPS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2BLDCPS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2BLDCP.Text);
                    }
                    if (settings.CheckM2BLDCIS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2BLDCIS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2BLDCI.Text);
                    }
                    if (settings.CheckM2BLDCDS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2BLDCDS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2BLDCD.Text);
                    }
                    if (settings.CheckM2BLDCx1S == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2BLDCx1S) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2BLDCx1.Text);
                    }
                    if (settings.CheckM2BLDCx2S == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2BLDCx2S) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2BLDCx2.Text);
                    }
                    if (settings.CheckM2BLDCx3S == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2BLDCx3S) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2BLDCx3.Text);
                    }
                    if (settings.CheckM2PMSMSpeedS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2PMSMSpeedS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2PMSMSpeed.Text);
                    }
                    if (settings.CheckM2PMSMPositionS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2PMSMPositionS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2PMSMPosition.Text);
                    }
                    if (settings.CheckM2PMSMTorqueS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2PMSMTorqueS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2PMSMTorque.Text);
                    }
                    if (settings.CheckM2PMSMCurrentS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2PMSMCurrentS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2PMSMCurrent.Text);
                    }
                    if (settings.CheckM2PMSMVoltageS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2PMSMVoltageS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2PMSMVoltage.Text);
                    }
                    if (settings.CheckM2PMSMPS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2PMSMPS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2PMSMP.Text);
                    }
                    if (settings.CheckM2PMSMIS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2PMSMIS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2PMSMI.Text);
                    }
                    if (settings.CheckM2PMSMDS == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2PMSMDS) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2PMSMD.Text);
                    }
                    if (settings.CheckM2PMSMx1S == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2PMSMx1S) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2PMSMx1.Text);
                    }
                    if (settings.CheckM2PMSMx2S == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2PMSMx2S) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2PMSMx2.Text);
                    }
                    if (settings.CheckM2PMSMx3S == true)
                    {
                        WriteMulipleRegistersTable[Convert.ToInt16(settings.AddrM2PMSMx3S) - Convert.ToInt16(settings.WriteGenAddr)] = Convert.ToUInt16(textBoxM2PMSMx3.Text);
                    }
                    Thread.Sleep(10); // opóźnienie 10ms
                    masterRtu.WriteMultipleRegisters(slaveAddress, Convert.ToUInt16(settings.WriteGenAddr), WriteMulipleRegistersTable);      //wysyła wcześniej zdefiniowane wartości na konkretne adresy urządzenia
                    writeStart = false;         //ustawia writeStart jako false
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Runchart()
        {

            string dataX = "";
            string dataY1 = "";
            string dataY2 = "";
            string dataY3 = "";
            string dataY4 = "";
            if (chartStart == true) 
            {
                chart2.Series.Clear();          //czyści wszystkie dane na wykresie
                Series series1 = new Series();  //generuje nowe serie danych
                Series series2 = new Series();
                Series series3 = new Series();
                Series series4 = new Series();
                series1.Name = "M1" + comboBoxY1chart.Text + "1";   //definiuje nazwe seri
                series1.IsVisibleInLegend = true;                   //seria widoczna w legendzie
                series1.ChartType = SeriesChartType.Spline;         //rodzaj wykresu
                series2.Name = "M2" + comboBoxY1chart.Text + "1";
                series2.IsVisibleInLegend = true;
                series2.IsXValueIndexed = true;
                series2.ChartType = SeriesChartType.Spline;
                series3.Name = "M1" + comboBoxY2chart.Text + "2";
                series3.IsVisibleInLegend = true;
                series3.IsXValueIndexed = true;
                series3.ChartType = SeriesChartType.Spline;
                series4.Name = "M2" + comboBoxY2chart.Text + "2";
                series4.IsVisibleInLegend = true;
                series4.IsXValueIndexed = true;
                series4.ChartType = SeriesChartType.Spline;
                chart2.Series.Add(series1);     //dodaje serie do wykresu.
                chart2.Series.Add(series2);
                chart2.Series.Add(series3);
                chart2.Series.Add(series4);
                chart2.DataSource = export;     //źródło ustawia źródło danych dla wykresu jako export
                    foreach (Data data in export)   
                    {
                        if (comboBoxXchart.Text == "Time")      //sprawdza jakie pole ma wyświetlić, poniżej będą definiowane wartości na osi X
                        {
                        dataX = "x";            // dla czasu dane ustawia jako X, jako że będą definiowane inaczej
                        }
                        if (comboBoxXchart.Text == "ID")
                        {
                            dataX = Convert.ToString(data.ID);  //reszta danych to konkretn nowe pola z listy export

                        }
                        if (comboBoxXchart.Text == "M1Speed")
                        {
                            dataX = data.M1Speed;
                        }
                        if (comboBoxXchart.Text == "M1Position")
                        {
                            dataX = data.M1Position;
                        }
                        if (comboBoxXchart.Text == "M1Torque")
                        {
                            dataX = data.M1Torque;
                        }
                        if (comboBoxXchart.Text == "M1Current")
                        {
                            dataX = data.M1Current;
                        }
                        if (comboBoxXchart.Text == "M1Voltage")
                        {
                            dataX = data.M1Voltage;
                        }
                        if (comboBoxXchart.Text == "M1P")
                        {
                            dataX = data.M1P;
                        }
                        if (comboBoxXchart.Text == "M1I")
                        {
                            dataX = data.M1I;
                        }
                        if (comboBoxXchart.Text == "M1D")
                        {
                            dataX = data.M1D;
                        }
                        if (comboBoxXchart.Text == "M1x1")
                        {
                            dataX = data.M1x1;
                        }
                        if (comboBoxXchart.Text == "M1x2")
                        {
                            dataX = data.M1x2;
                        }
                        if (comboBoxXchart.Text == "M1x3")
                        {
                            dataX = data.M1x3;
                        }
                        if (comboBoxXchart.Text == "M2Speed")
                        {
                            dataX = data.M2Speed;
                        }
                        if (comboBoxXchart.Text == "M2Position")
                        {
                            dataX = data.M2Position;
                        }
                        if (comboBoxXchart.Text == "M2Torque")
                        {
                            dataX = data.M2Torque;
                        }
                        if (comboBoxXchart.Text == "M2Current")
                        {
                            dataX = data.M2Current;
                        }
                        if (comboBoxXchart.Text == "M2Voltage")
                        {
                            dataX = data.M2Voltage;
                        }
                        if (comboBoxXchart.Text == "M2P")
                        {
                            dataX = data.M2P;
                        }
                        if (comboBoxXchart.Text == "M2I")
                        {
                            dataX = data.M2I;
                        }
                        if (comboBoxXchart.Text == "M2D")
                        {
                            dataX = data.M2D;
                        }
                        if (comboBoxXchart.Text == "M2x1")
                        {
                            dataX = data.M2x1;
                        }
                        if (comboBoxXchart.Text == "M2x2")
                        {
                            dataX = data.M2x2;
                        }
                        if (comboBoxXchart.Text == "M2x3")
                        {
                            dataX = data.M2x3;
                        }

                        if (comboBoxY1chart.Text == "Speed") // poniżej będą definiowane wartości na osi Y
                        {
                            dataY1 = data.M1Speed;
                            dataY2 = data.M2Speed;

                        }
                        if (comboBoxY1chart.Text == "Position")
                        {
                            dataY1 = data.M1Position;
                            dataY2 = data.M2Position;
                        }
                        if (comboBoxY1chart.Text == "Torque")
                        {
                            dataY1 = data.M1Torque;
                            dataY2 = data.M2Torque;
                        }
                        if (comboBoxY1chart.Text == "Current")
                        {
                            dataY1 = data.M1Current;
                            dataY2 = data.M2Current;
                        }
                        if (comboBoxY1chart.Text == "Voltage")
                        {
                            dataY1 = data.M1Voltage;
                            dataY2 = data.M2Voltage;
                        }
                        if (comboBoxY1chart.Text == "Frequency")
                        {
                            dataY1 = Convert.ToString(0); //M1 to silnik prądu stałego, więc częstotliwość nie będzie widoczna
                            dataY2 = data.M2Frequency;
                        }
                        if (comboBoxY1chart.Text == "P")
                        {
                            dataY1 = data.M1P;
                            dataY2 = data.M2P;
                        }
                        if (comboBoxY1chart.Text == "I")
                        {
                            dataY1 = data.M1I;
                            dataY2 = data.M2I;
                        }
                        if (comboBoxY1chart.Text == "D")
                        {
                            dataY1 = data.M1D;
                            dataY2 = data.M2D;
                        }
                        if (comboBoxY1chart.Text == "x1")
                        {
                            dataY1 = data.M1x1;
                            dataY2 = data.M2x1;
                        }
                        if (comboBoxY1chart.Text == "x2")
                        {
                            dataY1 = data.M1x2;
                            dataY2 = data.M2x2;
                        }
                        if (comboBoxY1chart.Text == "x3")
                        {
                            dataY1 = data.M1x3;
                            dataY2 = data.M2x3;
                        }

                        if (comboBoxY2chart.Text == "Speed")
                        {
                            dataY3 = data.M1Speed;
                            dataY4 = data.M2Speed;

                        }
                        if (comboBoxY2chart.Text == "Position")
                        {
                            dataY3 = data.M1Position;
                            dataY4 = data.M2Position;
                        }
                        if (comboBoxY2chart.Text == "Torque")
                        {
                            dataY3 = data.M1Speed;
                            dataY4 = data.M2Speed;
                        }
                        if (comboBoxY2chart.Text == "Current")
                        {
                            dataY3 = data.M1Speed;
                            dataY4 = data.M2Speed;
                        }
                        if (comboBoxY2chart.Text == "Voltage")
                        {
                            dataY3 = data.M1Speed;
                            dataY4 = data.M2Speed;
                        }
                        if (comboBoxY2chart.Text == "Frequency")
                        {
                            dataY3 = Convert.ToString(0);
                            dataY4 = data.M2Frequency;
                        }
                        if (comboBoxY2chart.Text == "P")
                        {
                            dataY3 = data.M1P;
                            dataY4 = data.M2P;
                        }
                        if (comboBoxY2chart.Text == "I")
                        {
                            dataY3 = data.M1I;
                            dataY4 = data.M2I;
                        }
                        if (comboBoxY2chart.Text == "D")
                        {
                            dataY3 = data.M1D;
                            dataY4 = data.M2D;
                        }
                        if (comboBoxY2chart.Text == "x1")
                        {
                            dataY3 = data.M1x1;
                            dataY4 = data.M2x1;
                        }
                        if (comboBoxY2chart.Text == "x2")
                        {
                            dataY3 = data.M1x2;
                            dataY4 = data.M2x2;
                        }
                        if (comboBoxY2chart.Text == "x3")
                        {
                            dataY3 = data.M1x3;
                            dataY4 = data.M2x3;
                        }

                    if (dataX == "x")  //ustawia wyjątek gdy dane mają być liczone po czasie
                    {
                        series1.XValueType = ChartValueType.Time;
                        series2.XValueType = ChartValueType.Time;
                        series3.XValueType = ChartValueType.Time;
                        series4.XValueType = ChartValueType.Time;
                        series1.Points.AddY(dataY1);
                        series2.Points.AddY(dataY2);
                        series3.Points.AddY(dataY3);
                        series4.Points.AddY(dataY4);
                    }
                    else   //dane gdy na osi X wartością nie jest czas
                    {
                        series1.Points.AddXY(dataX, dataY1);
                        series2.Points.AddXY(dataX, dataY2);
                        series3.Points.AddXY(dataX, dataY3);
                        series4.Points.AddXY(dataX, dataY4);
                    }
                    }
                    Thread.Sleep(20); // opóźnienie 20ms
            }
        }

        private void btnStartM2Asynch_Click(object sender, EventArgs e) 
        {
            Stop();
            chartStart = true;
            startM1 = true;
            startM2 = true;
            M2Asynch = true;
            readStart = true;
            ReadTEST();
        }

        private void btnStartM2BLDC_Click(object sender, EventArgs e)
        {
            Stop();
            chartStart = true;
            startM1 = true;
            startM2 = true;
            M2BLDC = true;
            readStart = true;
            ReadTEST();
        }

        private void btnStartM2PMSM_Click(object sender, EventArgs e)
        {
            Stop();
            chartStart = true;
            startM1 = true;
            startM2 = true;
            M2PMSM = true;
            readStart = true;
            ReadTEST();
        }
        private void EnableDisable()        //z zalezności od warości klasy settings, umożliwia lub uniemożliwia korzystanie z konkretnych pól
        {
            if (settings.CheckM1SpeedS == false)
            {
                trackBarM1Speed.Enabled = false;
                textBoxM1Speed.Enabled = false;
            }
            else
            {
                trackBarM1Speed.Enabled = true;
                textBoxM1Speed.Enabled = true;
            }
            if (settings.CheckM1PositionS == false)
            {
                trackBarM1Position.Enabled = false;
                textBoxM1Position.Enabled = false;
            }
            else
            {
                trackBarM1Position.Enabled = true;
                textBoxM1Position.Enabled = true;
            }
            if (settings.CheckM1TorqueS == false)
            {
                trackBarM1Torque.Enabled = false;
                textBoxM1Torque.Enabled = false;
            }
            else
            {
                trackBarM1Torque.Enabled = true;
                textBoxM1Torque.Enabled = true;
            }
            if (settings.CheckM1CurrentS == false)
            {
                trackBarM1Current.Enabled = false;
                textBoxM1Current.Enabled = false;
            }
            else
            {
                trackBarM1Current.Enabled = true;
                textBoxM1Current.Enabled = true;
            }
            if (settings.CheckM1VoltageS == false)
            {
                trackBarM1Voltage.Enabled = false;
                textBoxM1Voltage.Enabled = false;
            }
            else
            {
                trackBarM1Voltage.Enabled = true;
                textBoxM1Voltage.Enabled = true;
            }
            if (settings.CheckM1PS == false)
            {
                trackBarM1P.Enabled = false;
                textBoxM1P.Enabled = false;
            }
            else
            {
                trackBarM1P.Enabled = true;
                textBoxM1P.Enabled = true;
            }
            if (settings.CheckM1IS == false)
            {
                trackBarM1I.Enabled = false;
                textBoxM1I.Enabled = false;
            }
            else
            {
                trackBarM1I.Enabled = true;
                textBoxM1I.Enabled = true;
            }
            if (settings.CheckM1DS == false)
            {
                trackBarM1D.Enabled = false;
                textBoxM1D.Enabled = false;
            }
            else
            {
                trackBarM1D.Enabled = true;
                textBoxM1D.Enabled = true;
            }
            if (settings.CheckM1x1S == false)
            {
                trackBarM1x1.Enabled = false;
                textBoxM1x1.Enabled = false;
            }
            else
            {
                trackBarM1x1.Enabled = true;
                textBoxM1x1.Enabled = true;
            }
            if (settings.CheckM1x2S == false)
            {
                trackBarM1x2.Enabled = false;
                textBoxM1x2.Enabled = false;
            }
            else
            {
                trackBarM1x2.Enabled = true;
                textBoxM1x2.Enabled = true;
            }
            if (settings.CheckM1x3S == false)
            {
                trackBarM1x3.Enabled = false;
                textBoxM1x3.Enabled = false;
            }
            else
            {
                trackBarM1x3.Enabled = true;
                textBoxM1x3.Enabled = true;
            }
            if (settings.CheckM2DCSpeedS == false)
            {
                trackBarM2DCSpeed.Enabled = false;
                textBoxM2DCSpeed.Enabled = false;
            }
            else
            {
                trackBarM2DCSpeed.Enabled = true;
                textBoxM2DCSpeed.Enabled = true;
            }
            if (settings.CheckM2DCPositionS == false)
            {
                trackBarM2DCPosition.Enabled = false;
                textBoxM2DCPosition.Enabled = false;
            }
            else
            {
                trackBarM2DCPosition.Enabled = true;
                textBoxM2DCPosition.Enabled = true;
            }
            if (settings.CheckM2DCTorqueS == false)
            {
                trackBarM2DCTorque.Enabled = false;
                textBoxM2DCTorque.Enabled = false;
            }
            else
            {
                trackBarM2DCTorque.Enabled = true;
                textBoxM2DCTorque.Enabled = true;
            }
            if (settings.CheckM2DCCurrentS == false)
            {
                trackBarM2DCCurrent.Enabled = false;
                textBoxM2DCCurrent.Enabled = false;
            }
            else
            {
                trackBarM2DCCurrent.Enabled = true;
                textBoxM2DCCurrent.Enabled = true;
            }
            if (settings.CheckM2DCVoltageS == false)
            {
                trackBarM2DCVoltage.Enabled = false;
                textBoxM2DCVoltage.Enabled = false;
            }
            else
            {
                trackBarM2DCVoltage.Enabled = true;
                textBoxM2DCVoltage.Enabled = true;
            }
            if (settings.CheckM2DCPS == false)
            {
                trackBarM2DCP.Enabled = false;
                textBoxM2DCP.Enabled = false;
            }
            else
            {
                trackBarM2DCP.Enabled = true;
                textBoxM2DCP.Enabled = true;
            }
            if (settings.CheckM2DCIS == false)
            {
                trackBarM2DCI.Enabled = false;
                textBoxM2DCI.Enabled = false;
            }
            else
            {
                trackBarM2DCI.Enabled = true;
                textBoxM2DCI.Enabled = true;
            }
            if (settings.CheckM2DCDS == false)
            {
                trackBarM2DCD.Enabled = false;
                textBoxM2DCD.Enabled = false;
            }
            else
            {
                trackBarM2DCD.Enabled = true;
                textBoxM2DCD.Enabled = true;
            }
            if (settings.CheckM2DCx1S == false)
            {
                trackBarM2DCx1.Enabled = false;
                textBoxM2DCx1.Enabled = false;
            }
            else
            {
                trackBarM2DCx1.Enabled = true;
                textBoxM2DCx1.Enabled = true;
            }
            if (settings.CheckM2DCx2S == false)
            {
                trackBarM2DCx2.Enabled = false;
                textBoxM2DCx2.Enabled = false;
            }
            else
            {
                trackBarM2DCx2.Enabled = true;
                textBoxM2DCx2.Enabled = true;
            }
            if (settings.CheckM2DCx3S == false)
            {
                trackBarM2DCx3.Enabled = false;
                textBoxM2DCx3.Enabled = false;
            }
            else
            {
                trackBarM2DCx3.Enabled = true;
                textBoxM2DCx3.Enabled = true;
            }
            if (settings.CheckM2AsynchSpeedS == false)
            {
                trackBarM2AsynchSpeed.Enabled = false;
                textBoxM2AsynchSpeed.Enabled = false;
            }
            else
            {
                trackBarM2AsynchSpeed.Enabled = true;
                textBoxM2AsynchSpeed.Enabled = true;
            }
            if (settings.CheckM2AsynchPositionS == false)
            {
                trackBarM2AsynchPosition.Enabled = false;
                textBoxM2AsynchPosition.Enabled = false;
            }
            else
            {
                trackBarM2AsynchPosition.Enabled = true;
                textBoxM2AsynchPosition.Enabled = true;
            }
            if (settings.CheckM2AsynchTorqueS == false)
            {
                trackBarM2AsynchTorque.Enabled = false;
                textBoxM2AsynchTorque.Enabled = false;
            }
            else
            {
                trackBarM2AsynchTorque.Enabled = true;
                textBoxM2AsynchTorque.Enabled = true;
            }
            if (settings.CheckM2AsynchCurrentS == false)
            {
                trackBarM2AsynchCurrent.Enabled = false;
                textBoxM2AsynchCurrent.Enabled = false;
            }
            else
            {
                trackBarM2AsynchCurrent.Enabled = true;
                textBoxM2AsynchCurrent.Enabled = true;
            }
            if (settings.CheckM2AsynchVoltageS == false)
            {
                trackBarM2AsynchVoltage.Enabled = false;
                textBoxM2AsynchVoltage.Enabled = false;
            }
            else
            {
                trackBarM2AsynchVoltage.Enabled = true;
                textBoxM2AsynchVoltage.Enabled = true;
            }
            if (settings.CheckM2AsynchPS == false)
            {
                trackBarM2AsynchP.Enabled = false;
                textBoxM2AsynchP.Enabled = false;
            }
            else
            {
                trackBarM2AsynchP.Enabled = true;
                textBoxM2AsynchP.Enabled = true;
            }
            if (settings.CheckM2AsynchIS == false)
            {
                trackBarM2AsynchI.Enabled = false;
                textBoxM2AsynchI.Enabled = false;
            }
            else
            {
                trackBarM2AsynchI.Enabled = true;
                textBoxM2AsynchI.Enabled = true;
            }
            if (settings.CheckM2AsynchDS == false)
            {
                trackBarM2AsynchD.Enabled = false;
                textBoxM2AsynchD.Enabled = false;
            }
            else
            {
                trackBarM2AsynchD.Enabled = true;
                textBoxM2AsynchD.Enabled = true;
            }
            if (settings.CheckM2Asynchx1S == false)
            {
                trackBarM2Asynchx1.Enabled = false;
                textBoxM2Asynchx1.Enabled = false;
            }
            else
            {
                trackBarM2Asynchx1.Enabled = true;
                textBoxM2Asynchx1.Enabled = true;
            }
            if (settings.CheckM2Asynchx2S == false)
            {
                trackBarM2Asynchx2.Enabled = false;
                textBoxM2Asynchx2.Enabled = false;
            }
            else
            {
                trackBarM2Asynchx2.Enabled = true;
                textBoxM2Asynchx2.Enabled = true;
            }
            if (settings.CheckM2Asynchx3S == false)
            {
                trackBarM2Asynchx3.Enabled = false;
                textBoxM2Asynchx3.Enabled = false;
            }
            else
            {
                trackBarM2Asynchx3.Enabled = true;
                textBoxM2Asynchx3.Enabled = true;
            }
            if (settings.CheckM2BLDCSpeedS == false)
            {
                trackBarM2BLDCSpeed.Enabled = false;
                textBoxM2BLDCSpeed.Enabled = false;
            }
            else
            {
                trackBarM2BLDCSpeed.Enabled = true;
                textBoxM2BLDCSpeed.Enabled = true;
            }
            if (settings.CheckM2BLDCPositionS == false)
            {
                trackBarM2BLDCPosition.Enabled = false;
                textBoxM2BLDCPosition.Enabled = false;
            }
            else
            {
                trackBarM2BLDCPosition.Enabled = true;
                textBoxM2BLDCPosition.Enabled = true;
            }
            if (settings.CheckM2BLDCTorqueS == false)
            {
                trackBarM2BLDCTorque.Enabled = false;
                textBoxM2BLDCTorque.Enabled = false;
            }
            else
            {
                trackBarM2BLDCTorque.Enabled = true;
                textBoxM2BLDCTorque.Enabled = true;
            }
            if (settings.CheckM2BLDCCurrentS == false)
            {
                trackBarM2BLDCCurrent.Enabled = false;
                textBoxM2BLDCCurrent.Enabled = false;
            }
            else
            {
                trackBarM2BLDCCurrent.Enabled = true;
                textBoxM2BLDCCurrent.Enabled = true;
            }
            if (settings.CheckM2BLDCVoltageS == false)
            {
                trackBarM2BLDCVoltage.Enabled = false;
                textBoxM2BLDCVoltage.Enabled = false;
            }
            else
            {
                trackBarM2BLDCVoltage.Enabled = true;
                textBoxM2BLDCVoltage.Enabled = true;
            }
            if (settings.CheckM2BLDCPS == false)
            {
                trackBarM2BLDCP.Enabled = false;
                textBoxM2BLDCP.Enabled = false;
            }
            else
            {
                trackBarM2BLDCP.Enabled = true;
                textBoxM2BLDCP.Enabled = true;
            }
            if (settings.CheckM2BLDCIS == false)
            {
                trackBarM2BLDCI.Enabled = false;
                textBoxM2BLDCI.Enabled = false;
            }
            else
            {
                trackBarM2BLDCI.Enabled = true;
                textBoxM2BLDCI.Enabled = true;
            }
            if (settings.CheckM2BLDCDS == false)
            {
                trackBarM2BLDCD.Enabled = false;
                textBoxM2BLDCD.Enabled = false;
            }
            else
            {
                trackBarM2BLDCD.Enabled = true;
                textBoxM2BLDCD.Enabled = true;
            }
            if (settings.CheckM2BLDCx1S == false)
            {
                trackBarM2BLDCx1.Enabled = false;
                textBoxM2BLDCx1.Enabled = false;
            }
            else
            {
                trackBarM2BLDCx1.Enabled = true;
                textBoxM2BLDCx1.Enabled = true;
            }
            if (settings.CheckM2BLDCx2S == false)
            {
                trackBarM2BLDCx2.Enabled = false;
                textBoxM2BLDCx2.Enabled = false;
            }
            else
            {
                trackBarM2BLDCx2.Enabled = true;
                textBoxM2BLDCx2.Enabled = true;
            }
            if (settings.CheckM2BLDCx3S == false)
            {
                trackBarM2BLDCx3.Enabled = false;
                textBoxM2BLDCx3.Enabled = false;
            }
            else
            {
                trackBarM2BLDCx3.Enabled = true;
                textBoxM2BLDCx3.Enabled = true;
            }
            if (settings.CheckM2PMSMSpeedS == false)
            {
                trackBarM2PMSMSpeed.Enabled = false;
                textBoxM2PMSMSpeed.Enabled = false;
            }
            else
            {
                trackBarM2PMSMSpeed.Enabled = true;
                textBoxM2PMSMSpeed.Enabled = true;
            }
            if (settings.CheckM2PMSMPositionS == false)
            {
                trackBarM2PMSMPosition.Enabled = false;
                textBoxM2PMSMPosition.Enabled = false;
            }
            else
            {
                trackBarM2PMSMPosition.Enabled = true;
                textBoxM2PMSMPosition.Enabled = true;
            }
            if (settings.CheckM2PMSMTorqueS == false)
            {
                trackBarM2PMSMTorque.Enabled = false;
                textBoxM2PMSMTorque.Enabled = false;
            }
            else
            {
                trackBarM2PMSMTorque.Enabled = true;
                textBoxM2PMSMTorque.Enabled = true;
            }
            if (settings.CheckM2PMSMCurrentS == false)
            {
                trackBarM2PMSMCurrent.Enabled = false;
                textBoxM2PMSMCurrent.Enabled = false;
            }
            else
            {
                trackBarM2PMSMCurrent.Enabled = true;
                textBoxM2PMSMCurrent.Enabled = true;
            }
            if (settings.CheckM2PMSMVoltageS == false)
            {
                trackBarM2PMSMVoltage.Enabled = false;
                textBoxM2PMSMVoltage.Enabled = false;
            }
            else
            {
                trackBarM2PMSMVoltage.Enabled = true;
                textBoxM2PMSMVoltage.Enabled = true;
            }
            if (settings.CheckM2PMSMPS == false)
            {
                trackBarM2PMSMP.Enabled = false;
                textBoxM2PMSMP.Enabled = false;
            }
            else
            {
                trackBarM2PMSMP.Enabled = true;
                textBoxM2PMSMP.Enabled = true;
            }
            if (settings.CheckM2PMSMIS == false)
            {
                trackBarM2PMSMI.Enabled = false;
                textBoxM2PMSMI.Enabled = false;
            }
            else
            {
                trackBarM2PMSMI.Enabled = true;
                textBoxM2PMSMI.Enabled = true;
            }
            if (settings.CheckM2PMSMDS == false)
            {
                trackBarM2PMSMD.Enabled = false;
                textBoxM2PMSMD.Enabled = false;
            }
            else
            {
                trackBarM2PMSMD.Enabled = true;
                textBoxM2PMSMD.Enabled = true;
            }
            if (settings.CheckM2PMSMx1S == false)
            {
                trackBarM2PMSMx1.Enabled = false;
                textBoxM2PMSMx1.Enabled = false;
            }
            else
            {
                trackBarM2PMSMx1.Enabled = true;
                textBoxM2PMSMx1.Enabled = true;
            }
            if (settings.CheckM2PMSMx2S == false)
            {
                trackBarM2PMSMx2.Enabled = false;
                textBoxM2PMSMx2.Enabled = false;
            }
            else
            {
                trackBarM2PMSMx2.Enabled = true;
                textBoxM2PMSMx2.Enabled = true;
            }
            if (settings.CheckM2PMSMx3S == false)
            {
                trackBarM2PMSMx3.Enabled = false;
                textBoxM2PMSMx3.Enabled = false;
            }
            else
            {
                trackBarM2PMSMx3.Enabled = true;
                textBoxM2PMSMx3.Enabled = true;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            chartStart = true;

            Runchart();
        }

        private void BtnStopM1_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void BtnConfirmM1_Click(object sender, EventArgs e)
        {
            writeStart = true;
            TestWrite();
            writeStart = false;
        }

        private void BtnStopM2DC_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void BtnConfirmM2DC_Click(object sender, EventArgs e)
        {
            writeStart = true;
            TestWrite();
            writeStart = false;
        }

        private void BtnStartM2Asynch_Click_1(object sender, EventArgs e)
        {
            Stop();
            startM1 = true;
            startM2 = true;
            M2Asynch = true;
            readStart = true;
            ReadTEST();
        }

        private void BtnStartM2BLDC_Click_1(object sender, EventArgs e)
        {
            Stop();
            startM1 = true;
            startM2 = true;
            M2BLDC = true;
            readStart = true;
            ReadTEST();
        }

        private void BtnStartM2PMSM_Click_1(object sender, EventArgs e)
        {
            Stop();
            startM1 = true;
            startM2 = true;
            M2PMSM = true;
            readStart = true;
            ReadTEST();
        }

        private void BtnConfirmM2Asynch_Click(object sender, EventArgs e)
        {
            writeStart = true;
            TestWrite();
            writeStart = false;
        }

        private void BtnConfirmM2BLDC_Click(object sender, EventArgs e)
        {
            writeStart = true;
            TestWrite();
            writeStart = false;
        }

        private void BtnConfirmM2PMSM_Click(object sender, EventArgs e)
        {
            writeStart = true;
            TestWrite();
            writeStart = false;
        }
        private void LockSettings()   //blokuje wszystkie pola umożliwiające wprowadzenie danych 
        {
            textBoxM1Speed.Enabled = false;
            trackBarM1Speed.Enabled = false;
            textBoxM1Position.Enabled = false;
            trackBarM1Position.Enabled = false;
            textBoxM1Torque.Enabled = false;
            trackBarM1Torque.Enabled = false;
            textBoxM1Current.Enabled = false;
            trackBarM1Current.Enabled = false;
            textBoxM1Voltage.Enabled = false;
            trackBarM1Voltage.Enabled = false;
            textBoxM1P.Enabled = false;
            trackBarM1P.Enabled = false;
            textBoxM1I.Enabled = false;
            trackBarM1I.Enabled = false;
            textBoxM1D.Enabled = false;
            trackBarM1D.Enabled = false;
            textBoxM1x1.Enabled = false;
            trackBarM1x1.Enabled = false;
            textBoxM1x2.Enabled = false;
            trackBarM1x2.Enabled = false;
            textBoxM1x3.Enabled = false;
            trackBarM1x3.Enabled = false;
            textBoxM2DCSpeed.Enabled = false;
            trackBarM2DCSpeed.Enabled = false;
            textBoxM2DCPosition.Enabled = false;
            trackBarM2DCPosition.Enabled = false;
            textBoxM2DCTorque.Enabled = false;
            trackBarM2DCTorque.Enabled = false;
            textBoxM2DCCurrent.Enabled = false;
            trackBarM2DCCurrent.Enabled = false;
            textBoxM2DCVoltage.Enabled = false;
            trackBarM2DCVoltage.Enabled = false;
            textBoxM2DCP.Enabled = false;
            trackBarM2DCP.Enabled = false;
            textBoxM2DCI.Enabled = false;
            trackBarM2DCI.Enabled = false;
            textBoxM2DCD.Enabled = false;
            trackBarM2DCD.Enabled = false;
            textBoxM2DCx1.Enabled = false;
            trackBarM2DCx1.Enabled = false;
            textBoxM2DCx2.Enabled = false;
            trackBarM2DCx2.Enabled = false;
            textBoxM2DCx3.Enabled = false;
            trackBarM2DCx3.Enabled = false;
            textBoxM2AsynchSpeed.Enabled = false;
            trackBarM2AsynchSpeed.Enabled = false;
            textBoxM2AsynchPosition.Enabled = false;
            trackBarM2AsynchPosition.Enabled = false;
            textBoxM2AsynchTorque.Enabled = false;
            trackBarM2AsynchTorque.Enabled = false;
            textBoxM2AsynchCurrent.Enabled = false;
            trackBarM2AsynchCurrent.Enabled = false;
            textBoxM2AsynchVoltage.Enabled = false;
            trackBarM2AsynchVoltage.Enabled = false;
            textBoxM2AsynchFrequency.Enabled = false;
            trackBarM2AsynchFrequency.Enabled = false;
            textBoxM2AsynchP.Enabled = false;
            trackBarM2AsynchP.Enabled = false;
            textBoxM2AsynchI.Enabled = false;
            trackBarM2AsynchI.Enabled = false;
            textBoxM2AsynchD.Enabled = false;
            trackBarM2AsynchD.Enabled = false;
            textBoxM2Asynchx1.Enabled = false;
            trackBarM2Asynchx1.Enabled = false;
            textBoxM2Asynchx2.Enabled = false;
            trackBarM2Asynchx2.Enabled = false;
            textBoxM2Asynchx3.Enabled = false;
            trackBarM2Asynchx3.Enabled = false;
            textBoxM2BLDCSpeed.Enabled = false;
            trackBarM2BLDCSpeed.Enabled = false;
            textBoxM2BLDCPosition.Enabled = false;
            trackBarM2BLDCPosition.Enabled = false;
            textBoxM2BLDCTorque.Enabled = false;
            trackBarM2BLDCTorque.Enabled = false;
            textBoxM2BLDCCurrent.Enabled = false;
            trackBarM2BLDCCurrent.Enabled = false;
            textBoxM2BLDCVoltage.Enabled = false;
            trackBarM2BLDCVoltage.Enabled = false;
            textBoxM2BLDCP.Enabled = false;
            trackBarM2BLDCP.Enabled = false;
            textBoxM2BLDCI.Enabled = false;
            trackBarM2BLDCI.Enabled = false;
            textBoxM2BLDCD.Enabled = false;
            trackBarM2BLDCD.Enabled = false;
            textBoxM2BLDCx1.Enabled = false;
            trackBarM2BLDCx1.Enabled = false;
            textBoxM2BLDCx2.Enabled = false;
            trackBarM2BLDCx2.Enabled = false;
            textBoxM2BLDCx3.Enabled = false;
            trackBarM2BLDCx3.Enabled = false;
            textBoxM2PMSMSpeed.Enabled = false;
            trackBarM2PMSMSpeed.Enabled = false;
            textBoxM2PMSMPosition.Enabled = false;
            trackBarM2PMSMPosition.Enabled = false;
            textBoxM2PMSMTorque.Enabled = false;
            trackBarM2PMSMTorque.Enabled = false;
            textBoxM2PMSMCurrent.Enabled = false;
            trackBarM2PMSMCurrent.Enabled = false;
            textBoxM2PMSMVoltage.Enabled = false;
            trackBarM2PMSMVoltage.Enabled = false;
            textBoxM2PMSMFrequency.Enabled = false;
            trackBarM2PMSMFrequency.Enabled = false;
            textBoxM2PMSMP.Enabled = false;
            trackBarM2PMSMP.Enabled = false;
            textBoxM2PMSMI.Enabled = false;
            trackBarM2PMSMI.Enabled = false;
            textBoxM2PMSMD.Enabled = false;
            trackBarM2PMSMD.Enabled = false;
            textBoxM2PMSMx1.Enabled = false;
            trackBarM2PMSMx1.Enabled = false;
            textBoxM2PMSMx2.Enabled = false;
            trackBarM2PMSMx2.Enabled = false;
            textBoxM2PMSMx3.Enabled = false;
            trackBarM2PMSMx3.Enabled = false;
        }
    }
}