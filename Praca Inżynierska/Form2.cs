using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Praca_Inżynierska
{
    public partial class Form2 : Form
    {
        public Settings settings = new Settings();
        public string[] value;
        public Form2()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            SaveData();

            using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\config.xml",FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(fs, settings);
                MessageBox.Show("Created");
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\config.xml", FileMode.Open, FileAccess.Read))
            {
                settings = serializer.Deserialize(fs) as Settings;

            }
            LoadData();
        }

        private void BtnSetToDefault_Click(object sender, EventArgs e)
        {

        }
        private void SaveData()
        {
            settings.CPBaudRate = cBoxBaudRate.Text;
            settings.CPDataBits = cBoxDataBits.Text;
            settings.CPParity = cBoxParityBits.Text;
            settings.CPStopBits = cBoxStopBits.Text;
            settings.AddrM1SpeedS = txtBoxM1SpeedS.Text;
            settings.AddrM1SpeedSNOP = txtBoxM1SpeedSNOP.Text;
            settings.AddrM1PositionS = txtBoxM1PositionS.Text;
            settings.AddrM1PositionSNOP = txtBoxM1PositionSNOP.Text;
            settings.AddrM1TorqueS = txtBoxM1TorqueS.Text;
            settings.AddrM1TorqueSNOP = txtBoxM1TorqueSNOP.Text;
            settings.AddrM1PowerS = txtBoxM1PowerS.Text;
            settings.AddrM1PowerSNOP = txtBoxM1PowerSNOP.Text;
            settings.AddrM1PS = txtBoxM1PS.Text;
            settings.AddrM1PSNOP = txtBoxM1PSNOP.Text;
            settings.AddrM1IS = txtBoxM1IS.Text;
            settings.AddrM1ISNOP = txtBoxM1ISNOP.Text;
            settings.AddrM1DS = txtBoxM1DS.Text;
            settings.AddrM1DSNOP = txtBoxM1DSNOP.Text;
            settings.AddrM1x1S = txtBoxM1x1S.Text;
            settings.AddrM1x1SNOP = txtBoxM1x1SNOP.Text;
            settings.AddrM1x2S = txtBoxM1x2S.Text;
            settings.AddrM1x2SNOP = txtBoxM1x2SNOP.Text;
            settings.AddrM1x3S = txtBoxM1x3S.Text;
            settings.AddrM1x3SNOP = txtBoxM1x3SNOP.Text;
            settings.AddrM1SpeedR = txtBoxM1SpeedR.Text;
            settings.AddrM1SpeedRNOP = txtBoxM1SpeedRNOP.Text;
            settings.AddrM1PositionR = txtBoxM1PositionR.Text;
            settings.AddrM1PositionRNOP = txtBoxM1PositionRNOP.Text;
            settings.AddrM1TorqueR = txtBoxM1TorqueR.Text;
            settings.AddrM1TorqueRNOP = txtBoxM1TorqueRNOP.Text;
            settings.AddrM1PowerR = txtBoxM1PowerR.Text;
            settings.AddrM1PowerRNOP = txtBoxM1PowerRNOP.Text;
            settings.AddrM1PR = txtBoxM1PR.Text;
            settings.AddrM1PRNOP = txtBoxM1PRNOP.Text;
            settings.AddrM1IR = txtBoxM1IR.Text;
            settings.AddrM1IRNOP = txtBoxM1IRNOP.Text;
            settings.AddrM1DR = txtBoxM1DR.Text;
            settings.AddrM1DRNOP = txtBoxM1DRNOP.Text;
            settings.AddrM1x1R = txtBoxM1x1R.Text;
            settings.AddrM1x1RNOP = txtBoxM1x1RNOP.Text;
            settings.AddrM1x2R = txtBoxM1x2R.Text;
            settings.AddrM1x2RNOP = txtBoxM1x2RNOP.Text;
            settings.AddrM1x3R = txtBoxM1x3R.Text;
            settings.AddrM1x3RNOP = txtBoxM1x3RNOP.Text;
            settings.AddrM2DCSpeedS = txtBoxM2DCSpeedS.Text;
            settings.AddrM2DCSpeedSNOP = txtBoxM2DCSpeedSNOP.Text;
            settings.AddrM2DCPositionS = txtBoxM2DCPositionS.Text;
            settings.AddrM2DCPositionSNOP = txtBoxM2DCPositionSNOP.Text;
            settings.AddrM2DCTorqueS = txtBoxM2DCTorqueS.Text;
            settings.AddrM2DCTorqueSNOP = txtBoxM2DCTorqueSNOP.Text;
            settings.AddrM2DCPowerS = txtBoxM2DCPowerS.Text;
            settings.AddrM2DCPowerSNOP = txtBoxM2DCPowerSNOP.Text;
            settings.AddrM2DCPS = txtBoxM2DCPS.Text;
            settings.AddrM2DCPSNOP = txtBoxM2DCPSNOP.Text;
            settings.AddrM2DCIS = txtBoxM2DCIS.Text;
            settings.AddrM2DCISNOP = txtBoxM2DCISNOP.Text;
            settings.AddrM2DCDS = txtBoxM2DCDS.Text;
            settings.AddrM2DCDSNOP = txtBoxM2DCDSNOP.Text;
            settings.AddrM2DCx1S = txtBoxM2DCx1S.Text;
            settings.AddrM2DCx1SNOP = txtBoxM2DCx1SNOP.Text;
            settings.AddrM2DCx2S = txtBoxM2DCx2S.Text;
            settings.AddrM2DCx2SNOP = txtBoxM2DCx2SNOP.Text;
            settings.AddrM2DCx3S = txtBoxM2DCx3S.Text;
            settings.AddrM2DCx3SNOP = txtBoxM2DCx3SNOP.Text;
            settings.AddrM2DCSpeedR = txtBoxM2DCSpeedR.Text;
            settings.AddrM2DCSpeedRNOP = txtBoxM2DCSpeedRNOP.Text;
            settings.AddrM2DCPositionR = txtBoxM2DCPositionR.Text;
            settings.AddrM2DCPositionRNOP = txtBoxM2DCPositionRNOP.Text;
            settings.AddrM2DCTorqueR = txtBoxM2DCTorqueR.Text;
            settings.AddrM2DCTorqueRNOP = txtBoxM2DCTorqueRNOP.Text;
            settings.AddrM2DCPowerR = txtBoxM2DCPowerR.Text;
            settings.AddrM2DCPowerRNOP = txtBoxM2DCPowerRNOP.Text;
            settings.AddrM2DCPR = txtBoxM2DCPR.Text;
            settings.AddrM2DCPRNOP = txtBoxM2DCPRNOP.Text;
            settings.AddrM2DCIR = txtBoxM2DCIR.Text;
            settings.AddrM2DCIRNOP = txtBoxM2DCIRNOP.Text;
            settings.AddrM2DCDR = txtBoxM2DCDR.Text;
            settings.AddrM2DCDRNOP = txtBoxM2DCDRNOP.Text;
            settings.AddrM2DCx1R = txtBoxM2DCx1R.Text;
            settings.AddrM2DCx1RNOP = txtBoxM2DCx1RNOP.Text;
            settings.AddrM2DCx2R = txtBoxM2DCx2R.Text;
            settings.AddrM2DCx2RNOP = txtBoxM2DCx2RNOP.Text;
            settings.AddrM2DCx3R = txtBoxM2DCx3R.Text;
            settings.AddrM2DCx3RNOP = txtBoxM2DCx3RNOP.Text;
            settings.AddrM2AsynchSpeedS = txtBoxM2AsynchSpeedS.Text;
            settings.AddrM2AsynchSpeedSNOP = txtBoxM2AsynchSpeedSNOP.Text;
            settings.AddrM2AsynchPositionS = txtBoxM2AsynchPositionS.Text;
            settings.AddrM2AsynchPositionSNOP = txtBoxM2AsynchPositionSNOP.Text;
            settings.AddrM2AsynchTorqueS = txtBoxM2AsynchTorqueS.Text;
            settings.AddrM2AsynchTorqueSNOP = txtBoxM2AsynchTorqueSNOP.Text;
            settings.AddrM2AsynchPowerS = txtBoxM2AsynchPowerS.Text;
            settings.AddrM2AsynchPowerSNOP = txtBoxM2AsynchPowerSNOP.Text;
            settings.AddrM2AsynchPS = txtBoxM2AsynchPS.Text;
            settings.AddrM2AsynchPSNOP = txtBoxM2AsynchPSNOP.Text;
            settings.AddrM2AsynchIS = txtBoxM2AsynchIS.Text;
            settings.AddrM2AsynchISNOP = txtBoxM2AsynchISNOP.Text;
            settings.AddrM2AsynchDS = txtBoxM2AsynchDS.Text;
            settings.AddrM2AsynchDSNOP = txtBoxM2AsynchDSNOP.Text;
            settings.AddrM2Asynchx1S = txtBoxM2Asynchx1S.Text;
            settings.AddrM2Asynchx1SNOP = txtBoxM2Asynchx1SNOP.Text;
            settings.AddrM2Asynchx2S = txtBoxM2Asynchx2S.Text;
            settings.AddrM2Asynchx2SNOP = txtBoxM2Asynchx2SNOP.Text;
            settings.AddrM2Asynchx3S = txtBoxM2Asynchx3S.Text;
            settings.AddrM2Asynchx3SNOP = txtBoxM2Asynchx3SNOP.Text;
            settings.AddrM2AsynchSpeedR = txtBoxM2AsynchSpeedR.Text;
            settings.AddrM2AsynchSpeedRNOP = txtBoxM2AsynchSpeedRNOP.Text;
            settings.AddrM2AsynchPositionR = txtBoxM2AsynchPositionR.Text;
            settings.AddrM2AsynchPositionRNOP = txtBoxM2AsynchPositionRNOP.Text;
            settings.AddrM2AsynchTorqueR = txtBoxM2AsynchTorqueR.Text;
            settings.AddrM2AsynchTorqueRNOP = txtBoxM2AsynchTorqueRNOP.Text;
            settings.AddrM2AsynchPowerR = txtBoxM2AsynchPowerR.Text;
            settings.AddrM2AsynchPowerRNOP = txtBoxM2AsynchPowerRNOP.Text;
            settings.AddrM2AsynchPR = txtBoxM2AsynchPR.Text;
            settings.AddrM2AsynchPRNOP = txtBoxM2AsynchPRNOP.Text;
            settings.AddrM2AsynchIR = txtBoxM2AsynchIR.Text;
            settings.AddrM2AsynchIRNOP = txtBoxM2AsynchIRNOP.Text;
            settings.AddrM2AsynchDR = txtBoxM2AsynchDR.Text;
            settings.AddrM2AsynchDRNOP = txtBoxM2AsynchDRNOP.Text;
            settings.AddrM2Asynchx1R = txtBoxM2Asynchx1R.Text;
            settings.AddrM2Asynchx1RNOP = txtBoxM2Asynchx1RNOP.Text;
            settings.AddrM2Asynchx2R = txtBoxM2Asynchx2R.Text;
            settings.AddrM2Asynchx2RNOP = txtBoxM2Asynchx2RNOP.Text;
            settings.AddrM2Asynchx3R = txtBoxM2Asynchx3R.Text;
            settings.AddrM2Asynchx3RNOP = txtBoxM2Asynchx3RNOP.Text;
            settings.AddrM2BLDCSpeedS = txtBoxM2BLDCSpeedS.Text;
            settings.AddrM2BLDCSpeedSNOP = txtBoxM2BLDCSpeedSNOP.Text;
            settings.AddrM2BLDCPositionS = txtBoxM2BLDCPositionS.Text;
            settings.AddrM2BLDCPositionSNOP = txtBoxM2BLDCPositionSNOP.Text;
            settings.AddrM2BLDCTorqueS = txtBoxM2BLDCTorqueS.Text;
            settings.AddrM2BLDCTorqueSNOP = txtBoxM2BLDCTorqueSNOP.Text;
            settings.AddrM2BLDCPowerS = txtBoxM2BLDCPowerS.Text;
            settings.AddrM2BLDCPowerSNOP = txtBoxM2BLDCPowerSNOP.Text;
            settings.AddrM2BLDCPS = txtBoxM2BLDCPS.Text;
            settings.AddrM2BLDCPSNOP = txtBoxM2BLDCPSNOP.Text;
            settings.AddrM2BLDCIS = txtBoxM2BLDCIS.Text;
            settings.AddrM2BLDCISNOP = txtBoxM2BLDCISNOP.Text;
            settings.AddrM2BLDCDS = txtBoxM2BLDCDS.Text;
            settings.AddrM2BLDCDSNOP = txtBoxM2BLDCDSNOP.Text;
            settings.AddrM2BLDCx1S = txtBoxM2BLDCx1S.Text;
            settings.AddrM2BLDCx1SNOP = txtBoxM2BLDCx1SNOP.Text;
            settings.AddrM2BLDCx2S = txtBoxM2BLDCx2S.Text;
            settings.AddrM2BLDCx2SNOP = txtBoxM2BLDCx2SNOP.Text;
            settings.AddrM2BLDCx3S = txtBoxM2BLDCx3S.Text;
            settings.AddrM2BLDCx3SNOP = txtBoxM2BLDCx3SNOP.Text;
            settings.AddrM2BLDCSpeedR = txtBoxM2BLDCSpeedR.Text;
            settings.AddrM2BLDCSpeedRNOP = txtBoxM2BLDCSpeedRNOP.Text;
            settings.AddrM2BLDCPositionR = txtBoxM2BLDCPositionR.Text;
            settings.AddrM2BLDCPositionRNOP = txtBoxM2BLDCPositionRNOP.Text;
            settings.AddrM2BLDCTorqueR = txtBoxM2BLDCTorqueR.Text;
            settings.AddrM2BLDCTorqueRNOP = txtBoxM2BLDCTorqueRNOP.Text;
            settings.AddrM2BLDCPowerR = txtBoxM2BLDCPowerR.Text;
            settings.AddrM2BLDCPowerRNOP = txtBoxM2BLDCPowerRNOP.Text;
            settings.AddrM2BLDCPR = txtBoxM2BLDCPR.Text;
            settings.AddrM2BLDCPRNOP = txtBoxM2BLDCPRNOP.Text;
            settings.AddrM2BLDCIR = txtBoxM2BLDCIR.Text;
            settings.AddrM2BLDCIRNOP = txtBoxM2BLDCIRNOP.Text;
            settings.AddrM2BLDCDR = txtBoxM2BLDCDR.Text;
            settings.AddrM2BLDCDRNOP = txtBoxM2BLDCDRNOP.Text;
            settings.AddrM2BLDCx1R = txtBoxM2BLDCx1R.Text;
            settings.AddrM2BLDCx1RNOP = txtBoxM2BLDCx1RNOP.Text;
            settings.AddrM2BLDCx2R = txtBoxM2BLDCx2R.Text;
            settings.AddrM2BLDCx2RNOP = txtBoxM2BLDCx2RNOP.Text;
            settings.AddrM2BLDCx3R = txtBoxM2BLDCx3R.Text;
            settings.AddrM2BLDCx3RNOP = txtBoxM2BLDCx3RNOP.Text;
            settings.AddrM2PMSMSpeedS = txtBoxM2PMSMSpeedS.Text;
            settings.AddrM2PMSMSpeedSNOP = txtBoxM2PMSMSpeedSNOP.Text;
            settings.AddrM2PMSMPositionS = txtBoxM2PMSMPositionS.Text;
            settings.AddrM2PMSMPositionSNOP = txtBoxM2PMSMPositionSNOP.Text;
            settings.AddrM2PMSMTorqueS = txtBoxM2PMSMTorqueS.Text;
            settings.AddrM2PMSMTorqueSNOP = txtBoxM2PMSMTorqueSNOP.Text;
            settings.AddrM2PMSMPowerS = txtBoxM2PMSMPowerS.Text;
            settings.AddrM2PMSMPowerSNOP = txtBoxM2PMSMPowerSNOP.Text;
            settings.AddrM2PMSMPS = txtBoxM2PMSMPS.Text;
            settings.AddrM2PMSMPSNOP = txtBoxM2PMSMPSNOP.Text;
            settings.AddrM2PMSMIS = txtBoxM2PMSMIS.Text;
            settings.AddrM2PMSMISNOP = txtBoxM2PMSMISNOP.Text;
            settings.AddrM2PMSMDS = txtBoxM2PMSMDS.Text;
            settings.AddrM2PMSMDSNOP = txtBoxM2PMSMDSNOP.Text;
            settings.AddrM2PMSMx1S = txtBoxM2PMSMx1S.Text;
            settings.AddrM2PMSMx1SNOP = txtBoxM2PMSMx1SNOP.Text;
            settings.AddrM2PMSMx2S = txtBoxM2PMSMx2S.Text;
            settings.AddrM2PMSMx2SNOP = txtBoxM2PMSMx2SNOP.Text;
            settings.AddrM2PMSMx3S = txtBoxM2PMSMx3S.Text;
            settings.AddrM2PMSMx3SNOP = txtBoxM2PMSMx3SNOP.Text;
            settings.AddrM2PMSMSpeedR = txtBoxM2PMSMSpeedR.Text;
            settings.AddrM2PMSMSpeedRNOP = txtBoxM2PMSMSpeedRNOP.Text;
            settings.AddrM2PMSMPositionR = txtBoxM2PMSMPositionR.Text;
            settings.AddrM2PMSMPositionRNOP = txtBoxM2PMSMPositionRNOP.Text;
            settings.AddrM2PMSMTorqueR = txtBoxM2PMSMTorqueR.Text;
            settings.AddrM2PMSMTorqueRNOP = txtBoxM2PMSMTorqueRNOP.Text;
            settings.AddrM2PMSMPowerR = txtBoxM2PMSMPowerR.Text;
            settings.AddrM2PMSMPowerRNOP = txtBoxM2PMSMPowerRNOP.Text;
            settings.AddrM2PMSMPR = txtBoxM2PMSMPR.Text;
            settings.AddrM2PMSMPRNOP = txtBoxM2PMSMPRNOP.Text;
            settings.AddrM2PMSMIR = txtBoxM2PMSMIR.Text;
            settings.AddrM2PMSMIRNOP = txtBoxM2PMSMIRNOP.Text;
            settings.AddrM2PMSMDR = txtBoxM2PMSMDR.Text;
            settings.AddrM2PMSMDRNOP = txtBoxM2PMSMDRNOP.Text;
            settings.AddrM2PMSMx1R = txtBoxM2PMSMx1R.Text;
            settings.AddrM2PMSMx1RNOP = txtBoxM2PMSMx1RNOP.Text;
            settings.AddrM2PMSMx2R = txtBoxM2PMSMx2R.Text;
            settings.AddrM2PMSMx2RNOP = txtBoxM2PMSMx2RNOP.Text;
            settings.AddrM2PMSMx3R = txtBoxM2PMSMx3R.Text;
            settings.AddrM2PMSMx3RNOP = txtBoxM2PMSMx3RNOP.Text;
            if (checkBoxM1SpeedS.Checked)
            {
                settings.CheckM1SpeedS = "Yes";
            }
            else
            {
                settings.CheckM1SpeedS = "No";
            }
            if (checkBoxM1PositionS.Checked)
            {
                settings.CheckM1PositionS = "Yes";
            }
            else
            {
                settings.CheckM1PositionS = "No";
            }
            if (checkBoxM1TorqueS.Checked)
            {
                settings.CheckM1TorqueS = "Yes";
            }
            else
            {
                settings.CheckM1TorqueS = "No";
            }
            if (checkBoxM1PowerS.Checked)
            {
                settings.CheckM1PowerS = "Yes";
            }
            else
            {
                settings.CheckM1PowerS = "No";
            }
            if (checkBoxM1PS.Checked)
            {
                settings.CheckM1PS = "Yes";
            }
            else
            {
                settings.CheckM1PS = "No";
            }
            if (checkBoxM1IS.Checked)
            {
                settings.CheckM1IS = "Yes";
            }
            else
            {
                settings.CheckM1IS = "No";
            }
            if (checkBoxM1DS.Checked)
            {
                settings.CheckM1DS = "Yes";
            }
            else
            {
                settings.CheckM1DS = "No";
            }
            if (checkBoxM1x1S.Checked)
            {
                settings.CheckM1x1S = "Yes";
            }
            else
            {
                settings.CheckM1x1S = "No";
            }
            if (checkBoxM1x2S.Checked)
            {
                settings.CheckM1x2S = "Yes";
            }
            else
            {
                settings.CheckM1x2S = "No";
            }
            if (checkBoxM1x3S.Checked)
            {
                settings.CheckM1x3S = "Yes";
            }
            else
            {
                settings.CheckM1x3S = "No";
            }
            if (checkBoxM1SpeedR.Checked)
            {
                settings.CheckM1SpeedR = "Yes";
            }
            else
            {
                settings.CheckM1SpeedR = "No";
            }
            if (checkBoxM1PositionR.Checked)
            {
                settings.CheckM1PositionR = "Yes";
            }
            else
            {
                settings.CheckM1PositionR = "No";
            }
            if (checkBoxM1TorqueR.Checked)
            {
                settings.CheckM1TorqueR = "Yes";
            }
            else
            {
                settings.CheckM1TorqueR = "No";
            }
            if (checkBoxM1PowerR.Checked)
            {
                settings.CheckM1PowerR = "Yes";
            }
            else
            {
                settings.CheckM1PowerR = "No";
            }
            if (checkBoxM1PR.Checked)
            {
                settings.CheckM1PR = "Yes";
            }
            else
            {
                settings.CheckM1PR = "No";
            }
            if (checkBoxM1IR.Checked)
            {
                settings.CheckM1IR = "Yes";
            }
            else
            {
                settings.CheckM1IR = "No";
            }
            if (checkBoxM1DR.Checked)
            {
                settings.CheckM1DR = "Yes";
            }
            else
            {
                settings.CheckM1DR = "No";
            }
            if (checkBoxM1x1R.Checked)
            {
                settings.CheckM1x1R = "Yes";
            }
            else
            {
                settings.CheckM1x1R = "No";
            }
            if (checkBoxM1x2R.Checked)
            {
                settings.CheckM1x2R = "Yes";
            }
            else
            {
                settings.CheckM1x2R = "No";
            }
            if (checkBoxM1x3R.Checked)
            {
                settings.CheckM1x3R = "Yes";
            }
            else
            {
                settings.CheckM1x3R = "No";
            }
            if (checkBoxM2DCSpeedS.Checked)
            {
                settings.CheckM2DCSpeedS = "Yes";
            }
            else
            {
                settings.CheckM2DCSpeedS = "No";
            }
            if (checkBoxM2DCPositionS.Checked)
            {
                settings.CheckM2DCPositionS = "Yes";
            }
            else
            {
                settings.CheckM2DCPositionS = "No";
            }
            if (checkBoxM2DCTorqueS.Checked)
            {
                settings.CheckM2DCTorqueS = "Yes";
            }
            else
            {
                settings.CheckM2DCTorqueS = "No";
            }
            if (checkBoxM2DCPowerS.Checked)
            {
                settings.CheckM2DCPowerS = "Yes";
            }
            else
            {
                settings.CheckM2DCPowerS = "No";
            }
            if (checkBoxM2DCPS.Checked)
            {
                settings.CheckM2DCPS = "Yes";
            }
            else
            {
                settings.CheckM2DCPS = "No";
            }
            if (checkBoxM2DCIS.Checked)
            {
                settings.CheckM2DCIS = "Yes";
            }
            else
            {
                settings.CheckM2DCIS = "No";
            }
            if (checkBoxM2DCDS.Checked)
            {
                settings.CheckM2DCDS = "Yes";
            }
            else
            {
                settings.CheckM2DCDS = "No";
            }
            if (checkBoxM2DCx1S.Checked)
            {
                settings.CheckM2DCx1S = "Yes";
            }
            else
            {
                settings.CheckM2DCx1S = "No";
            }
            if (checkBoxM2DCx2S.Checked)
            {
                settings.CheckM2DCx2S = "Yes";
            }
            else
            {
                settings.CheckM2DCx2S = "No";
            }
            if (checkBoxM2DCx3S.Checked)
            {
                settings.CheckM2DCx3S = "Yes";
            }
            else
            {
                settings.CheckM2DCx3S = "No";
            }
            if (checkBoxM2DCSpeedR.Checked)
            {
                settings.CheckM2DCSpeedR = "Yes";
            }
            else
            {
                settings.CheckM2DCSpeedR = "No";
            }
            if (checkBoxM2DCPositionR.Checked)
            {
                settings.CheckM2DCPositionR = "Yes";
            }
            else
            {
                settings.CheckM2DCPositionR = "No";
            }
            if (checkBoxM2DCTorqueR.Checked)
            {
                settings.CheckM2DCTorqueR = "Yes";
            }
            else
            {
                settings.CheckM2DCTorqueR = "No";
            }
            if (checkBoxM2DCPowerR.Checked)
            {
                settings.CheckM2DCPowerR = "Yes";
            }
            else
            {
                settings.CheckM2DCPowerR = "No";
            }
            if (checkBoxM2DCPR.Checked)
            {
                settings.CheckM2DCPR = "Yes";
            }
            else
            {
                settings.CheckM2DCPR = "No";
            }
            if (checkBoxM2DCIR.Checked)
            {
                settings.CheckM2DCIR = "Yes";
            }
            else
            {
                settings.CheckM2DCIR = "No";
            }
            if (checkBoxM2DCDR.Checked)
            {
                settings.CheckM2DCDR = "Yes";
            }
            else
            {
                settings.CheckM2DCDR = "No";
            }
            if (checkBoxM2DCx1R.Checked)
            {
                settings.CheckM2DCx1R = "Yes";
            }
            else
            {
                settings.CheckM2DCx1R = "No";
            }
            if (checkBoxM2DCx2R.Checked)
            {
                settings.CheckM2DCx2R = "Yes";
            }
            else
            {
                settings.CheckM2DCx2R = "No";
            }
            if (checkBoxM2DCx3R.Checked)
            {
                settings.CheckM2DCx3R = "Yes";
            }
            else
            {
                settings.CheckM2DCx3R = "No";
            }
            if (checkBoxM2AsynchSpeedS.Checked)
            {
                settings.CheckM2AsynchSpeedS = "Yes";
            }
            else
            {
                settings.CheckM2AsynchSpeedS = "No";
            }
            if (checkBoxM2AsynchPositionS.Checked)
            {
                settings.CheckM2AsynchPositionS = "Yes";
            }
            else
            {
                settings.CheckM2AsynchPositionS = "No";
            }
            if (checkBoxM2AsynchTorqueS.Checked)
            {
                settings.CheckM2AsynchTorqueS = "Yes";
            }
            else
            {
                settings.CheckM2AsynchTorqueS = "No";
            }
            if (checkBoxM2AsynchPowerS.Checked)
            {
                settings.CheckM2AsynchPowerS = "Yes";
            }
            else
            {
                settings.CheckM2AsynchPowerS = "No";
            }
            if (checkBoxM2AsynchPS.Checked)
            {
                settings.CheckM2AsynchPS = "Yes";
            }
            else
            {
                settings.CheckM2AsynchPS = "No";
            }
            if (checkBoxM2AsynchIS.Checked)
            {
                settings.CheckM2AsynchIS = "Yes";
            }
            else
            {
                settings.CheckM2AsynchIS = "No";
            }
            if (checkBoxM2AsynchDS.Checked)
            {
                settings.CheckM2AsynchDS = "Yes";
            }
            else
            {
                settings.CheckM2AsynchDS = "No";
            }
            if (checkBoxM2Asynchx1S.Checked)
            {
                settings.CheckM2Asynchx1S = "Yes";
            }
            else
            {
                settings.CheckM2Asynchx1S = "No";
            }
            if (checkBoxM2Asynchx2S.Checked)
            {
                settings.CheckM2Asynchx2S = "Yes";
            }
            else
            {
                settings.CheckM2Asynchx2S = "No";
            }
            if (checkBoxM2Asynchx3S.Checked)
            {
                settings.CheckM2Asynchx3S = "Yes";
            }
            else
            {
                settings.CheckM2Asynchx3S = "No";
            }
            if (checkBoxM2AsynchSpeedR.Checked)
            {
                settings.CheckM2AsynchSpeedR = "Yes";
            }
            else
            {
                settings.CheckM2AsynchSpeedR = "No";
            }
            if (checkBoxM2AsynchPositionR.Checked)
            {
                settings.CheckM2AsynchPositionR = "Yes";
            }
            else
            {
                settings.CheckM2AsynchPositionR = "No";
            }
            if (checkBoxM2AsynchTorqueR.Checked)
            {
                settings.CheckM2AsynchTorqueR = "Yes";
            }
            else
            {
                settings.CheckM2AsynchTorqueR = "No";
            }
            if (checkBoxM2AsynchPowerR.Checked)
            {
                settings.CheckM2AsynchPowerR = "Yes";
            }
            else
            {
                settings.CheckM2AsynchPowerR = "No";
            }
            if (checkBoxM2AsynchPR.Checked)
            {
                settings.CheckM2AsynchPR = "Yes";
            }
            else
            {
                settings.CheckM2AsynchPR = "No";
            }
            if (checkBoxM2AsynchIR.Checked)
            {
                settings.CheckM2AsynchIR = "Yes";
            }
            else
            {
                settings.CheckM2AsynchIR = "No";
            }
            if (checkBoxM2AsynchDR.Checked)
            {
                settings.CheckM2AsynchDR = "Yes";
            }
            else
            {
                settings.CheckM2AsynchDR = "No";
            }
            if (checkBoxM2Asynchx1R.Checked)
            {
                settings.CheckM2Asynchx1R = "Yes";
            }
            else
            {
                settings.CheckM2Asynchx1R = "No";
            }
            if (checkBoxM2Asynchx2R.Checked)
            {
                settings.CheckM2Asynchx2R = "Yes";
            }
            else
            {
                settings.CheckM2Asynchx2R = "No";
            }
            if (checkBoxM2Asynchx3R.Checked)
            {
                settings.CheckM2Asynchx3R = "Yes";
            }
            else
            {
                settings.CheckM2Asynchx3R = "No";
            }
            if (checkBoxM2BLDCSpeedS.Checked)
            {
                settings.CheckM2BLDCSpeedS = "Yes";
            }
            else
            {
                settings.CheckM2BLDCSpeedS = "No";
            }
            if (checkBoxM2BLDCPositionS.Checked)
            {
                settings.CheckM2BLDCPositionS = "Yes";
            }
            else
            {
                settings.CheckM2BLDCPositionS = "No";
            }
            if (checkBoxM2BLDCTorqueS.Checked)
            {
                settings.CheckM2BLDCTorqueS = "Yes";
            }
            else
            {
                settings.CheckM2BLDCTorqueS = "No";
            }
            if (checkBoxM2BLDCPowerS.Checked)
            {
                settings.CheckM2BLDCPowerS = "Yes";
            }
            else
            {
                settings.CheckM2BLDCPowerS = "No";
            }
            if (checkBoxM2BLDCPS.Checked)
            {
                settings.CheckM2BLDCPS = "Yes";
            }
            else
            {
                settings.CheckM2BLDCPS = "No";
            }
            if (checkBoxM2BLDCIS.Checked)
            {
                settings.CheckM2BLDCIS = "Yes";
            }
            else
            {
                settings.CheckM2BLDCIS = "No";
            }
            if (checkBoxM2BLDCDS.Checked)
            {
                settings.CheckM2BLDCDS = "Yes";
            }
            else
            {
                settings.CheckM2BLDCDS = "No";
            }
            if (checkBoxM2BLDCx1S.Checked)
            {
                settings.CheckM2BLDCx1S = "Yes";
            }
            else
            {
                settings.CheckM2BLDCx1S = "No";
            }
            if (checkBoxM2BLDCx2S.Checked)
            {
                settings.CheckM2BLDCx2S = "Yes";
            }
            else
            {
                settings.CheckM2BLDCx2S = "No";
            }
            if (checkBoxM2BLDCx3S.Checked)
            {
                settings.CheckM2BLDCx3S = "Yes";
            }
            else
            {
                settings.CheckM2BLDCx3S = "No";
            }
            if (checkBoxM2BLDCSpeedR.Checked)
            {
                settings.CheckM2BLDCSpeedR = "Yes";
            }
            else
            {
                settings.CheckM2BLDCSpeedR = "No";
            }
            if (checkBoxM2BLDCPositionR.Checked)
            {
                settings.CheckM2BLDCPositionR = "Yes";
            }
            else
            {
                settings.CheckM2BLDCPositionR = "No";
            }
            if (checkBoxM2BLDCTorqueR.Checked)
            {
                settings.CheckM2BLDCTorqueR = "Yes";
            }
            else
            {
                settings.CheckM2BLDCTorqueR = "No";
            }
            if (checkBoxM2BLDCPowerR.Checked)
            {
                settings.CheckM2BLDCPowerR = "Yes";
            }
            else
            {
                settings.CheckM2BLDCPowerR = "No";
            }
            if (checkBoxM2BLDCPR.Checked)
            {
                settings.CheckM2BLDCPR = "Yes";
            }
            else
            {
                settings.CheckM2BLDCPR = "No";
            }
            if (checkBoxM2BLDCIR.Checked)
            {
                settings.CheckM2BLDCIR = "Yes";
            }
            else
            {
                settings.CheckM2BLDCIR = "No";
            }
            if (checkBoxM2BLDCDR.Checked)
            {
                settings.CheckM2BLDCDR = "Yes";
            }
            else
            {
                settings.CheckM2BLDCDR = "No";
            }
            if (checkBoxM2BLDCx1R.Checked)
            {
                settings.CheckM2BLDCx1R = "Yes";
            }
            else
            {
                settings.CheckM2BLDCx1R = "No";
            }
            if (checkBoxM2BLDCx2R.Checked)
            {
                settings.CheckM2BLDCx2R = "Yes";
            }
            else
            {
                settings.CheckM2BLDCx2R = "No";
            }
            if (checkBoxM2BLDCx3R.Checked)
            {
                settings.CheckM2BLDCx3R = "Yes";
            }
            else
            {
                settings.CheckM2BLDCx3R = "No";
            }
            if (checkBoxM2PMSMSpeedS.Checked)
            {
                settings.CheckM2PMSMSpeedS = "Yes";
            }
            else
            {
                settings.CheckM2PMSMSpeedS = "No";
            }
            if (checkBoxM2PMSMPositionS.Checked)
            {
                settings.CheckM2PMSMPositionS = "Yes";
            }
            else
            {
                settings.CheckM2PMSMPositionS = "No";
            }
            if (checkBoxM2PMSMTorqueS.Checked)
            {
                settings.CheckM2PMSMTorqueS = "Yes";
            }
            else
            {
                settings.CheckM2PMSMTorqueS = "No";
            }
            if (checkBoxM2PMSMPowerS.Checked)
            {
                settings.CheckM2PMSMPowerS = "Yes";
            }
            else
            {
                settings.CheckM2PMSMPowerS = "No";
            }
            if (checkBoxM2PMSMPS.Checked)
            {
                settings.CheckM2PMSMPS = "Yes";
            }
            else
            {
                settings.CheckM2PMSMPS = "No";
            }
            if (checkBoxM2PMSMIS.Checked)
            {
                settings.CheckM2PMSMIS = "Yes";
            }
            else
            {
                settings.CheckM2PMSMIS = "No";
            }
            if (checkBoxM2PMSMDS.Checked)
            {
                settings.CheckM2PMSMDS = "Yes";
            }
            else
            {
                settings.CheckM2PMSMDS = "No";
            }
            if (checkBoxM2PMSMx1S.Checked)
            {
                settings.CheckM2PMSMx1S = "Yes";
            }
            else
            {
                settings.CheckM2PMSMx1S = "No";
            }
            if (checkBoxM2PMSMx2S.Checked)
            {
                settings.CheckM2PMSMx2S = "Yes";
            }
            else
            {
                settings.CheckM2PMSMx2S = "No";
            }
            if (checkBoxM2PMSMx3S.Checked)
            {
                settings.CheckM2PMSMx3S = "Yes";
            }
            else
            {
                settings.CheckM2PMSMx3S = "No";
            }
            if (checkBoxM2PMSMSpeedR.Checked)
            {
                settings.CheckM2PMSMSpeedR = "Yes";
            }
            else
            {
                settings.CheckM2PMSMSpeedR = "No";
            }
            if (checkBoxM2PMSMPositionR.Checked)
            {
                settings.CheckM2PMSMPositionR = "Yes";
            }
            else
            {
                settings.CheckM2PMSMPositionR = "No";
            }
            if (checkBoxM2PMSMTorqueR.Checked)
            {
                settings.CheckM2PMSMTorqueR = "Yes";
            }
            else
            {
                settings.CheckM2PMSMTorqueR = "No";
            }
            if (checkBoxM2PMSMPowerR.Checked)
            {
                settings.CheckM2PMSMPowerR = "Yes";
            }
            else
            {
                settings.CheckM2PMSMPowerR = "No";
            }
            if (checkBoxM2PMSMPR.Checked)
            {
                settings.CheckM2PMSMPR = "Yes";
            }
            else
            {
                settings.CheckM2PMSMPR = "No";
            }
            if (checkBoxM2PMSMIR.Checked)
            {
                settings.CheckM2PMSMIR = "Yes";
            }
            else
            {
                settings.CheckM2PMSMIR = "No";
            }
            if (checkBoxM2PMSMDR.Checked)
            {
                settings.CheckM2PMSMDR = "Yes";
            }
            else
            {
                settings.CheckM2PMSMDR = "No";
            }
            if (checkBoxM2PMSMx1R.Checked)
            {
                settings.CheckM2PMSMx1R = "Yes";
            }
            else
            {
                settings.CheckM2PMSMx1R = "No";
            }
            if (checkBoxM2PMSMx2R.Checked)
            {
                settings.CheckM2PMSMx2R = "Yes";
            }
            else
            {
                settings.CheckM2PMSMx2R = "No";
            }
            if (checkBoxM2PMSMx3R.Checked)
            {
                settings.CheckM2PMSMx3R = "Yes";
            }
            else
            {
                settings.CheckM2PMSMx3R = "No";
            }
        }
        private void LoadData()
        {
            cBoxBaudRate.Text = settings.CPBaudRate;
            cBoxDataBits.Text = settings.CPDataBits;
            cBoxParityBits.Text = settings.CPParity;
            cBoxStopBits.Text = settings.CPStopBits;
            txtBoxM1SpeedS.Text = settings.AddrM1SpeedS;
            txtBoxM1SpeedSNOP.Text = settings.AddrM1SpeedSNOP;
            txtBoxM1PositionS.Text = settings.AddrM1PositionS;
            txtBoxM1PositionSNOP.Text = settings.AddrM1PositionSNOP;
            txtBoxM1TorqueS.Text = settings.AddrM1TorqueS;
            txtBoxM1TorqueSNOP.Text = settings.AddrM1TorqueSNOP;
            txtBoxM1PowerS.Text = settings.AddrM1PowerS;
            txtBoxM1PowerSNOP.Text = settings.AddrM1PowerSNOP;
            txtBoxM1PS.Text = settings.AddrM1PS;
            txtBoxM1PSNOP.Text = settings.AddrM1PSNOP;
            txtBoxM1IS.Text = settings.AddrM1IS;
            txtBoxM1ISNOP.Text = settings.AddrM1ISNOP;
            txtBoxM1DS.Text = settings.AddrM1DS;
            txtBoxM1DSNOP.Text = settings.AddrM1DSNOP;
            txtBoxM1x1S.Text = settings.AddrM1x1S;
            txtBoxM1x1SNOP.Text = settings.AddrM1x1SNOP;
            txtBoxM1x2S.Text = settings.AddrM1x2S;
            txtBoxM1x2SNOP.Text = settings.AddrM1x2SNOP;
            txtBoxM1x3S.Text = settings.AddrM1x3S;
            txtBoxM1x3SNOP.Text = settings.AddrM1x3SNOP;
            txtBoxM1SpeedR.Text = settings.AddrM1SpeedR;
            txtBoxM1SpeedRNOP.Text = settings.AddrM1SpeedRNOP;
            txtBoxM1PositionR.Text = settings.AddrM1PositionR;
            txtBoxM1PositionRNOP.Text = settings.AddrM1PositionRNOP;
            txtBoxM1TorqueR.Text = settings.AddrM1TorqueR;
            txtBoxM1TorqueRNOP.Text = settings.AddrM1TorqueRNOP;
            txtBoxM1PowerR.Text = settings.AddrM1PowerR;
            txtBoxM1PowerRNOP.Text = settings.AddrM1PowerRNOP;
            txtBoxM1PR.Text = settings.AddrM1PR;
            txtBoxM1PRNOP.Text = settings.AddrM1PRNOP;
            txtBoxM1IR.Text = settings.AddrM1IR;
            txtBoxM1IRNOP.Text = settings.AddrM1IRNOP;
            txtBoxM1DR.Text = settings.AddrM1DR;
            txtBoxM1DRNOP.Text = settings.AddrM1DRNOP;
            txtBoxM1x1R.Text = settings.AddrM1x1R;
            txtBoxM1x1RNOP.Text = settings.AddrM1x1RNOP;
            txtBoxM1x2R.Text = settings.AddrM1x2R;
            txtBoxM1x2RNOP.Text = settings.AddrM1x2RNOP;
            txtBoxM1x3R.Text = settings.AddrM1x3R;
            txtBoxM1x3RNOP.Text = settings.AddrM1x3RNOP;
            txtBoxM2DCSpeedS.Text = settings.AddrM2DCSpeedS;
            txtBoxM2DCSpeedSNOP.Text = settings.AddrM2DCSpeedSNOP;
            txtBoxM2DCPositionS.Text = settings.AddrM2DCPositionS;
            txtBoxM2DCPositionSNOP.Text = settings.AddrM2DCPositionSNOP;
            txtBoxM2DCTorqueS.Text = settings.AddrM2DCTorqueS;
            txtBoxM2DCTorqueSNOP.Text = settings.AddrM2DCTorqueSNOP;
            txtBoxM2DCPowerS.Text = settings.AddrM2DCPowerS;
            txtBoxM2DCPowerSNOP.Text = settings.AddrM2DCPowerSNOP;
            txtBoxM2DCPS.Text = settings.AddrM2DCPS;
            txtBoxM2DCPSNOP.Text = settings.AddrM2DCPSNOP;
            txtBoxM2DCIS.Text = settings.AddrM2DCIS;
            txtBoxM2DCISNOP.Text = settings.AddrM2DCISNOP;
            txtBoxM2DCDS.Text = settings.AddrM2DCDS;
            txtBoxM2DCDSNOP.Text = settings.AddrM2DCDSNOP;
            txtBoxM2DCx1S.Text = settings.AddrM2DCx1S;
            txtBoxM2DCx1SNOP.Text = settings.AddrM2DCx1SNOP;
            txtBoxM2DCx2S.Text = settings.AddrM2DCx2S;
            txtBoxM2DCx2SNOP.Text = settings.AddrM2DCx2SNOP;
            txtBoxM2DCx3S.Text = settings.AddrM2DCx3S;
            txtBoxM2DCx3SNOP.Text = settings.AddrM2DCx3SNOP;
            txtBoxM2DCSpeedR.Text = settings.AddrM2DCSpeedR;
            txtBoxM2DCSpeedRNOP.Text = settings.AddrM2DCSpeedRNOP;
            txtBoxM2DCPositionR.Text = settings.AddrM2DCPositionR;
            txtBoxM2DCPositionRNOP.Text = settings.AddrM2DCPositionRNOP;
            txtBoxM2DCTorqueR.Text = settings.AddrM2DCTorqueR;
            txtBoxM2DCTorqueRNOP.Text = settings.AddrM2DCTorqueRNOP;
            txtBoxM2DCPowerR.Text = settings.AddrM2DCPowerR;
            txtBoxM2DCPowerRNOP.Text = settings.AddrM2DCPowerRNOP;
            txtBoxM2DCPR.Text = settings.AddrM2DCPR;
            txtBoxM2DCPRNOP.Text = settings.AddrM2DCPRNOP;
            txtBoxM2DCIR.Text = settings.AddrM2DCIR;
            txtBoxM2DCIRNOP.Text = settings.AddrM2DCIRNOP;
            txtBoxM2DCDR.Text = settings.AddrM2DCDR;
            txtBoxM2DCDRNOP.Text = settings.AddrM2DCDRNOP;
            txtBoxM2DCx1R.Text = settings.AddrM2DCx1R;
            txtBoxM2DCx1RNOP.Text = settings.AddrM2DCx1RNOP;
            txtBoxM2DCx2R.Text = settings.AddrM2DCx2R;
            txtBoxM2DCx2RNOP.Text = settings.AddrM2DCx2RNOP;
            txtBoxM2DCx3R.Text = settings.AddrM2DCx3R;
            txtBoxM2DCx3RNOP.Text = settings.AddrM2DCx3RNOP;
            txtBoxM2AsynchSpeedS.Text = settings.AddrM2AsynchSpeedS;
            txtBoxM2AsynchSpeedSNOP.Text = settings.AddrM2AsynchSpeedSNOP;
            txtBoxM2AsynchPositionS.Text = settings.AddrM2AsynchPositionS;
            txtBoxM2AsynchPositionSNOP.Text = settings.AddrM2AsynchPositionSNOP;
            txtBoxM2AsynchTorqueS.Text = settings.AddrM2AsynchTorqueS;
            txtBoxM2AsynchTorqueSNOP.Text = settings.AddrM2AsynchTorqueSNOP;
            txtBoxM2AsynchPowerS.Text = settings.AddrM2AsynchPowerS;
            txtBoxM2AsynchPowerSNOP.Text = settings.AddrM2AsynchPowerSNOP;
            txtBoxM2AsynchPS.Text = settings.AddrM2AsynchPS;
            txtBoxM2AsynchPSNOP.Text = settings.AddrM2AsynchPSNOP;
            txtBoxM2AsynchIS.Text = settings.AddrM2AsynchIS;
            txtBoxM2AsynchISNOP.Text = settings.AddrM2AsynchISNOP;
            txtBoxM2AsynchDS.Text = settings.AddrM2AsynchDS;
            txtBoxM2AsynchDSNOP.Text = settings.AddrM2AsynchDSNOP;
            txtBoxM2Asynchx1S.Text = settings.AddrM2Asynchx1S;
            txtBoxM2Asynchx1SNOP.Text = settings.AddrM2Asynchx1SNOP;
            txtBoxM2Asynchx2S.Text = settings.AddrM2Asynchx2S;
            txtBoxM2Asynchx2SNOP.Text = settings.AddrM2Asynchx2SNOP;
            txtBoxM2Asynchx3S.Text = settings.AddrM2Asynchx3S;
            txtBoxM2Asynchx3SNOP.Text = settings.AddrM2Asynchx3SNOP;
            txtBoxM2AsynchSpeedR.Text = settings.AddrM2AsynchSpeedR;
            txtBoxM2AsynchSpeedRNOP.Text = settings.AddrM2AsynchSpeedRNOP;
            txtBoxM2AsynchPositionR.Text = settings.AddrM2AsynchPositionR;
            txtBoxM2AsynchPositionRNOP.Text = settings.AddrM2AsynchPositionRNOP;
            txtBoxM2AsynchTorqueR.Text = settings.AddrM2AsynchTorqueR;
            txtBoxM2AsynchTorqueRNOP.Text = settings.AddrM2AsynchTorqueRNOP;
            txtBoxM2AsynchPowerR.Text = settings.AddrM2AsynchPowerR;
            txtBoxM2AsynchPowerRNOP.Text = settings.AddrM2AsynchPowerRNOP;
            txtBoxM2AsynchPR.Text = settings.AddrM2AsynchPR;
            txtBoxM2AsynchPRNOP.Text = settings.AddrM2AsynchPRNOP;
            txtBoxM2AsynchIR.Text = settings.AddrM2AsynchIR;
            txtBoxM2AsynchIRNOP.Text = settings.AddrM2AsynchIRNOP;
            txtBoxM2AsynchDR.Text = settings.AddrM2AsynchDR;
            txtBoxM2AsynchDRNOP.Text = settings.AddrM2AsynchDRNOP;
            txtBoxM2Asynchx1R.Text = settings.AddrM2Asynchx1R;
            txtBoxM2Asynchx1RNOP.Text = settings.AddrM2Asynchx1RNOP;
            txtBoxM2Asynchx2R.Text = settings.AddrM2Asynchx2R;
            txtBoxM2Asynchx2RNOP.Text = settings.AddrM2Asynchx2RNOP;
            txtBoxM2Asynchx3R.Text = settings.AddrM2Asynchx3R;
            txtBoxM2Asynchx3RNOP.Text = settings.AddrM2Asynchx3RNOP;
            txtBoxM2BLDCSpeedS.Text = settings.AddrM2BLDCSpeedS;
            txtBoxM2BLDCSpeedSNOP.Text = settings.AddrM2BLDCSpeedSNOP;
            txtBoxM2BLDCPositionS.Text = settings.AddrM2BLDCPositionS;
            txtBoxM2BLDCPositionSNOP.Text = settings.AddrM2BLDCPositionSNOP;
            txtBoxM2BLDCTorqueS.Text = settings.AddrM2BLDCTorqueS;
            txtBoxM2BLDCTorqueSNOP.Text = settings.AddrM2BLDCTorqueSNOP;
            txtBoxM2BLDCPowerS.Text = settings.AddrM2BLDCPowerS;
            txtBoxM2BLDCPowerSNOP.Text = settings.AddrM2BLDCPowerSNOP;
            txtBoxM2BLDCPS.Text = settings.AddrM2BLDCPS;
            txtBoxM2BLDCPSNOP.Text = settings.AddrM2BLDCPSNOP;
            txtBoxM2BLDCIS.Text = settings.AddrM2BLDCIS;
            txtBoxM2BLDCISNOP.Text = settings.AddrM2BLDCISNOP;
            txtBoxM2BLDCDS.Text = settings.AddrM2BLDCDS;
            txtBoxM2BLDCDSNOP.Text = settings.AddrM2BLDCDSNOP;
            txtBoxM2BLDCx1S.Text = settings.AddrM2BLDCx1S;
            txtBoxM2BLDCx1SNOP.Text = settings.AddrM2BLDCx1SNOP;
            txtBoxM2BLDCx2S.Text = settings.AddrM2BLDCx2S;
            txtBoxM2BLDCx2SNOP.Text = settings.AddrM2BLDCx2SNOP;
            txtBoxM2BLDCx3S.Text = settings.AddrM2BLDCx3S;
            txtBoxM2BLDCx3SNOP.Text = settings.AddrM2BLDCx3SNOP;
            txtBoxM2BLDCSpeedR.Text = settings.AddrM2BLDCSpeedR;
            txtBoxM2BLDCSpeedRNOP.Text = settings.AddrM2BLDCSpeedRNOP;
            txtBoxM2BLDCPositionR.Text = settings.AddrM2BLDCPositionR;
            txtBoxM2BLDCPositionRNOP.Text = settings.AddrM2BLDCPositionRNOP;
            txtBoxM2BLDCTorqueR.Text = settings.AddrM2BLDCTorqueR;
            txtBoxM2BLDCTorqueRNOP.Text = settings.AddrM2BLDCTorqueRNOP;
            txtBoxM2BLDCPowerR.Text = settings.AddrM2BLDCPowerR;
            txtBoxM2BLDCPowerRNOP.Text = settings.AddrM2BLDCPowerRNOP;
            txtBoxM2BLDCPR.Text = settings.AddrM2BLDCPR;
            txtBoxM2BLDCPRNOP.Text = settings.AddrM2BLDCPRNOP;
            txtBoxM2BLDCIR.Text = settings.AddrM2BLDCIR;
            txtBoxM2BLDCIRNOP.Text = settings.AddrM2BLDCIRNOP;
            txtBoxM2BLDCDR.Text = settings.AddrM2BLDCDR;
            txtBoxM2BLDCDRNOP.Text = settings.AddrM2BLDCDRNOP;
            txtBoxM2BLDCx1R.Text = settings.AddrM2BLDCx1R;
            txtBoxM2BLDCx1RNOP.Text = settings.AddrM2BLDCx1RNOP;
            txtBoxM2BLDCx2R.Text = settings.AddrM2BLDCx2R;
            txtBoxM2BLDCx2RNOP.Text = settings.AddrM2BLDCx2RNOP;
            txtBoxM2BLDCx3R.Text = settings.AddrM2BLDCx3R;
            txtBoxM2BLDCx3RNOP.Text = settings.AddrM2BLDCx3RNOP;
            txtBoxM2PMSMSpeedS.Text = settings.AddrM2PMSMSpeedS;
            txtBoxM2PMSMSpeedSNOP.Text = settings.AddrM2PMSMSpeedSNOP;
            txtBoxM2PMSMPositionS.Text = settings.AddrM2PMSMPositionS;
            txtBoxM2PMSMPositionSNOP.Text = settings.AddrM2PMSMPositionSNOP;
            txtBoxM2PMSMTorqueS.Text = settings.AddrM2PMSMTorqueS;
            txtBoxM2PMSMTorqueSNOP.Text = settings.AddrM2PMSMTorqueSNOP;
            txtBoxM2PMSMPowerS.Text = settings.AddrM2PMSMPowerS;
            txtBoxM2PMSMPowerSNOP.Text = settings.AddrM2PMSMPowerSNOP;
            txtBoxM2PMSMPS.Text = settings.AddrM2PMSMPS;
            txtBoxM2PMSMPSNOP.Text = settings.AddrM2PMSMPSNOP;
            txtBoxM2PMSMIS.Text = settings.AddrM2PMSMIS;
            txtBoxM2PMSMISNOP.Text = settings.AddrM2PMSMISNOP;
            txtBoxM2PMSMDS.Text = settings.AddrM2PMSMDS;
            txtBoxM2PMSMDSNOP.Text = settings.AddrM2PMSMDSNOP;
            txtBoxM2PMSMx1S.Text = settings.AddrM2PMSMx1S;
            txtBoxM2PMSMx1SNOP.Text = settings.AddrM2PMSMx1SNOP;
            txtBoxM2PMSMx2S.Text = settings.AddrM2PMSMx2S;
            txtBoxM2PMSMx2SNOP.Text = settings.AddrM2PMSMx2SNOP;
            txtBoxM2PMSMx3S.Text = settings.AddrM2PMSMx3S;
            txtBoxM2PMSMx3SNOP.Text = settings.AddrM2PMSMx3SNOP;
            txtBoxM2PMSMSpeedR.Text = settings.AddrM2PMSMSpeedR;
            txtBoxM2PMSMSpeedRNOP.Text = settings.AddrM2PMSMSpeedRNOP;
            txtBoxM2PMSMPositionR.Text = settings.AddrM2PMSMPositionR;
            txtBoxM2PMSMPositionRNOP.Text = settings.AddrM2PMSMPositionRNOP;
            txtBoxM2PMSMTorqueR.Text = settings.AddrM2PMSMTorqueR;
            txtBoxM2PMSMTorqueRNOP.Text = settings.AddrM2PMSMTorqueRNOP;
            txtBoxM2PMSMPowerR.Text = settings.AddrM2PMSMPowerR;
            txtBoxM2PMSMPowerRNOP.Text = settings.AddrM2PMSMPowerRNOP;
            txtBoxM2PMSMPR.Text = settings.AddrM2PMSMPR;
            txtBoxM2PMSMPRNOP.Text = settings.AddrM2PMSMPRNOP;
            txtBoxM2PMSMIR.Text = settings.AddrM2PMSMIR;
            txtBoxM2PMSMIRNOP.Text = settings.AddrM2PMSMIRNOP;
            txtBoxM2PMSMDR.Text = settings.AddrM2PMSMDR;
            txtBoxM2PMSMDRNOP.Text = settings.AddrM2PMSMDRNOP;
            txtBoxM2PMSMx1R.Text = settings.AddrM2PMSMx1R;
            txtBoxM2PMSMx1RNOP.Text = settings.AddrM2PMSMx1RNOP;
            txtBoxM2PMSMx2R.Text = settings.AddrM2PMSMx2R;
            txtBoxM2PMSMx2RNOP.Text = settings.AddrM2PMSMx2RNOP;
            txtBoxM2PMSMx3R.Text = settings.AddrM2PMSMx3R;
            txtBoxM2PMSMx3RNOP.Text = settings.AddrM2PMSMx3RNOP;
            if (settings.CheckM1SpeedS == ("Yes"))
            {
                checkBoxM1SpeedS.Checked = true;
            }
            else
            {
                checkBoxM1SpeedS.Checked = false;
            }
            if (settings.CheckM1PositionS == ("Yes"))
            {
                checkBoxM1PositionS.Checked = true;
            }
            else
            {
                checkBoxM1PositionS.Checked = false;
            }
            if (settings.CheckM1TorqueS == ("Yes"))
            {
                checkBoxM1TorqueS.Checked = true;
            }
            else
            {
                checkBoxM1TorqueS.Checked = false;
            }
            if (settings.CheckM1PowerS == ("Yes"))
            {
                checkBoxM1PowerS.Checked = true;
            }
            else
            {
                checkBoxM1PowerS.Checked = false;
            }
            if (settings.CheckM1PS == ("Yes"))
            {
                checkBoxM1PS.Checked = true;
            }
            else
            {
                checkBoxM1PS.Checked = false;
            }
            if (settings.CheckM1IS == ("Yes"))
            {
                checkBoxM1IS.Checked = true;
            }
            else
            {
                checkBoxM1IS.Checked = false;
            }
            if (settings.CheckM1DS == ("Yes"))
            {
                checkBoxM1DS.Checked = true;
            }
            else
            {
                checkBoxM1DS.Checked = false;
            }
            if (settings.CheckM1x1S == ("Yes"))
            {
                checkBoxM1x1S.Checked = true;
            }
            else
            {
                checkBoxM1x1S.Checked = false;
            }
            if (settings.CheckM1x2S == ("Yes"))
            {
                checkBoxM1x2S.Checked = true;
            }
            else
            {
                checkBoxM1x2S.Checked = false;
            }
            if (settings.CheckM1x3S == ("Yes"))
            {
                checkBoxM1x3S.Checked = true;
            }
            else
            {
                checkBoxM1x3S.Checked = false;
            }
            if (settings.CheckM1SpeedS == ("Yes"))
            {
                checkBoxM1SpeedS.Checked = true;
            }
            else
            {
                checkBoxM1SpeedR.Checked = false;
            }
            if (settings.CheckM1PositionR == ("Yes"))
            {
                checkBoxM1PositionR.Checked = true;
            }
            else
            {
                checkBoxM1PositionR.Checked = false;
            }
            if (settings.CheckM1TorqueR == ("Yes"))
            {
                checkBoxM1TorqueR.Checked = true;
            }
            else
            {
                checkBoxM1TorqueR.Checked = false;
            }
            if (settings.CheckM1PowerR == ("Yes"))
            {
                checkBoxM1PowerR.Checked = true;
            }
            else
            {
                checkBoxM1PowerR.Checked = false;
            }
            if (settings.CheckM1PR == ("Yes"))
            {
                checkBoxM1PR.Checked = true;
            }
            else
            {
                checkBoxM1PR.Checked = false;
            }
            if (settings.CheckM1IR == ("Yes"))
            {
                checkBoxM1IR.Checked = true;
            }
            else
            {
                checkBoxM1IR.Checked = false;
            }
            if (settings.CheckM1DR == ("Yes"))
            {
                checkBoxM1DR.Checked = true;
            }
            else
            {
                checkBoxM1DR.Checked = false;
            }
            if (settings.CheckM1x1R == ("Yes"))
            {
                checkBoxM1x1R.Checked = true;
            }
            else
            {
                checkBoxM1x1R.Checked = false;
            }
            if (settings.CheckM1x2R == ("Yes"))
            {
                checkBoxM1x2R.Checked = true;
            }
            else
            {
                checkBoxM1x2R.Checked = false;
            }
            if (settings.CheckM1x3R == ("Yes"))
            {
                checkBoxM1x3R.Checked = true;
            }
            else
            {
                checkBoxM1x3R.Checked = false;
            }
            if (settings.CheckM2DCSpeedS == ("Yes"))
            {
                checkBoxM2DCSpeedS.Checked = true;
            }
            else
            {
                checkBoxM2DCSpeedS.Checked = false;
            }
            if (settings.CheckM2DCPositionS == ("Yes"))
            {
                checkBoxM2DCPositionS.Checked = true;
            }
            else
            {
                checkBoxM2DCPositionS.Checked = false;
            }
            if (settings.CheckM2DCTorqueS == ("Yes"))
            {
                checkBoxM2DCTorqueS.Checked = true;
            }
            else
            {
                checkBoxM2DCTorqueS.Checked = false;
            }
            if (settings.CheckM2DCPowerS == ("Yes"))
            {
                checkBoxM2DCPowerS.Checked = true;
            }
            else
            {
                checkBoxM2DCPowerS.Checked = false;
            }
            if (settings.CheckM2DCPS == ("Yes"))
            {
                checkBoxM2DCPS.Checked = true;
            }
            else
            {
                checkBoxM2DCPS.Checked = false;
            }
            if (settings.CheckM2DCIS == ("Yes"))
            {
                checkBoxM2DCIS.Checked = true;
            }
            else
            {
                checkBoxM2DCIS.Checked = false;
            }
            if (settings.CheckM2DCDS == ("Yes"))
            {
                checkBoxM2DCDS.Checked = true;
            }
            else
            {
                checkBoxM2DCDS.Checked = false;
            }
            if (settings.CheckM2DCx1S == ("Yes"))
            {
                checkBoxM2DCx1S.Checked = true;
            }
            else
            {
                checkBoxM2DCx1S.Checked = false;
            }
            if (settings.CheckM2DCx2S == ("Yes"))
            {
                checkBoxM2DCx2S.Checked = true;
            }
            else
            {
                checkBoxM2DCx2S.Checked = false;
            }
            if (settings.CheckM2DCx3S == ("Yes"))
            {
                checkBoxM2DCx3S.Checked = true;
            }
            else
            {
                checkBoxM2DCx3S.Checked = false;
            }
            if (settings.CheckM2DCSpeedS == ("Yes"))
            {
                checkBoxM2DCSpeedS.Checked = true;
            }
            else
            {
                checkBoxM2DCSpeedR.Checked = false;
            }
            if (settings.CheckM2DCPositionR == ("Yes"))
            {
                checkBoxM2DCPositionR.Checked = true;
            }
            else
            {
                checkBoxM2DCPositionR.Checked = false;
            }
            if (settings.CheckM2DCTorqueR == ("Yes"))
            {
                checkBoxM2DCTorqueR.Checked = true;
            }
            else
            {
                checkBoxM2DCTorqueR.Checked = false;
            }
            if (settings.CheckM2DCPowerR == ("Yes"))
            {
                checkBoxM2DCPowerR.Checked = true;
            }
            else
            {
                checkBoxM2DCPowerR.Checked = false;
            }
            if (settings.CheckM2DCPR == ("Yes"))
            {
                checkBoxM2DCPR.Checked = true;
            }
            else
            {
                checkBoxM2DCPR.Checked = false;
            }
            if (settings.CheckM2DCIR == ("Yes"))
            {
                checkBoxM2DCIR.Checked = true;
            }
            else
            {
                checkBoxM2DCIR.Checked = false;
            }
            if (settings.CheckM2DCDR == ("Yes"))
            {
                checkBoxM2DCDR.Checked = true;
            }
            else
            {
                checkBoxM2DCDR.Checked = false;
            }
            if (settings.CheckM2DCx1R == ("Yes"))
            {
                checkBoxM2DCx1R.Checked = true;
            }
            else
            {
                checkBoxM2DCx1R.Checked = false;
            }
            if (settings.CheckM2DCx2R == ("Yes"))
            {
                checkBoxM2DCx2R.Checked = true;
            }
            else
            {
                checkBoxM2DCx2R.Checked = false;
            }
            if (settings.CheckM2DCx3R == ("Yes"))
            {
                checkBoxM2DCx3R.Checked = true;
            }
            else
            {
                checkBoxM2DCx3R.Checked = false;
            }
            if (settings.CheckM2AsynchSpeedS == ("Yes"))
            {
                checkBoxM2AsynchSpeedS.Checked = true;
            }
            else
            {
                checkBoxM2AsynchSpeedS.Checked = false;
            }
            if (settings.CheckM2AsynchPositionS == ("Yes"))
            {
                checkBoxM2AsynchPositionS.Checked = true;
            }
            else
            {
                checkBoxM2AsynchPositionS.Checked = false;
            }
            if (settings.CheckM2AsynchTorqueS == ("Yes"))
            {
                checkBoxM2AsynchTorqueS.Checked = true;
            }
            else
            {
                checkBoxM2AsynchTorqueS.Checked = false;
            }
            if (settings.CheckM2AsynchPowerS == ("Yes"))
            {
                checkBoxM2AsynchPowerS.Checked = true;
            }
            else
            {
                checkBoxM2AsynchPowerS.Checked = false;
            }
            if (settings.CheckM2AsynchPS == ("Yes"))
            {
                checkBoxM2AsynchPS.Checked = true;
            }
            else
            {
                checkBoxM2AsynchPS.Checked = false;
            }
            if (settings.CheckM2AsynchIS == ("Yes"))
            {
                checkBoxM2AsynchIS.Checked = true;
            }
            else
            {
                checkBoxM2AsynchIS.Checked = false;
            }
            if (settings.CheckM2AsynchDS == ("Yes"))
            {
                checkBoxM2AsynchDS.Checked = true;
            }
            else
            {
                checkBoxM2AsynchDS.Checked = false;
            }
            if (settings.CheckM2Asynchx1S == ("Yes"))
            {
                checkBoxM2Asynchx1S.Checked = true;
            }
            else
            {
                checkBoxM2Asynchx1S.Checked = false;
            }
            if (settings.CheckM2Asynchx2S == ("Yes"))
            {
                checkBoxM2Asynchx2S.Checked = true;
            }
            else
            {
                checkBoxM2Asynchx2S.Checked = false;
            }
            if (settings.CheckM2Asynchx3S == ("Yes"))
            {
                checkBoxM2Asynchx3S.Checked = true;
            }
            else
            {
                checkBoxM2Asynchx3S.Checked = false;
            }
            if (settings.CheckM2AsynchSpeedS == ("Yes"))
            {
                checkBoxM2AsynchSpeedS.Checked = true;
            }
            else
            {
                checkBoxM2AsynchSpeedR.Checked = false;
            }
            if (settings.CheckM2AsynchPositionR == ("Yes"))
            {
                checkBoxM2AsynchPositionR.Checked = true;
            }
            else
            {
                checkBoxM2AsynchPositionR.Checked = false;
            }
            if (settings.CheckM2AsynchTorqueR == ("Yes"))
            {
                checkBoxM2AsynchTorqueR.Checked = true;
            }
            else
            {
                checkBoxM2AsynchTorqueR.Checked = false;
            }
            if (settings.CheckM2AsynchPowerR == ("Yes"))
            {
                checkBoxM2AsynchPowerR.Checked = true;
            }
            else
            {
                checkBoxM2AsynchPowerR.Checked = false;
            }
            if (settings.CheckM2AsynchPR == ("Yes"))
            {
                checkBoxM2AsynchPR.Checked = true;
            }
            else
            {
                checkBoxM2AsynchPR.Checked = false;
            }
            if (settings.CheckM2AsynchIR == ("Yes"))
            {
                checkBoxM2AsynchIR.Checked = true;
            }
            else
            {
                checkBoxM2AsynchIR.Checked = false;
            }
            if (settings.CheckM2AsynchDR == ("Yes"))
            {
                checkBoxM2AsynchDR.Checked = true;
            }
            else
            {
                checkBoxM2AsynchDR.Checked = false;
            }
            if (settings.CheckM2Asynchx1R == ("Yes"))
            {
                checkBoxM2Asynchx1R.Checked = true;
            }
            else
            {
                checkBoxM2Asynchx1R.Checked = false;
            }
            if (settings.CheckM2Asynchx2R == ("Yes"))
            {
                checkBoxM2Asynchx2R.Checked = true;
            }
            else
            {
                checkBoxM2Asynchx2R.Checked = false;
            }
            if (settings.CheckM2Asynchx3R == ("Yes"))
            {
                checkBoxM2Asynchx3R.Checked = true;
            }
            else
            {
                checkBoxM2Asynchx3R.Checked = false;
            }
            if (settings.CheckM2BLDCSpeedS == ("Yes"))
            {
                checkBoxM2BLDCSpeedS.Checked = true;
            }
            else
            {
                checkBoxM2BLDCSpeedS.Checked = false;
            }
            if (settings.CheckM2BLDCPositionS == ("Yes"))
            {
                checkBoxM2BLDCPositionS.Checked = true;
            }
            else
            {
                checkBoxM2BLDCPositionS.Checked = false;
            }
            if (settings.CheckM2BLDCTorqueS == ("Yes"))
            {
                checkBoxM2BLDCTorqueS.Checked = true;
            }
            else
            {
                checkBoxM2BLDCTorqueS.Checked = false;
            }
            if (settings.CheckM2BLDCPowerS == ("Yes"))
            {
                checkBoxM2BLDCPowerS.Checked = true;
            }
            else
            {
                checkBoxM2BLDCPowerS.Checked = false;
            }
            if (settings.CheckM2BLDCPS == ("Yes"))
            {
                checkBoxM2BLDCPS.Checked = true;
            }
            else
            {
                checkBoxM2BLDCPS.Checked = false;
            }
            if (settings.CheckM2BLDCIS == ("Yes"))
            {
                checkBoxM2BLDCIS.Checked = true;
            }
            else
            {
                checkBoxM2BLDCIS.Checked = false;
            }
            if (settings.CheckM2BLDCDS == ("Yes"))
            {
                checkBoxM2BLDCDS.Checked = true;
            }
            else
            {
                checkBoxM2BLDCDS.Checked = false;
            }
            if (settings.CheckM2BLDCx1S == ("Yes"))
            {
                checkBoxM2BLDCx1S.Checked = true;
            }
            else
            {
                checkBoxM2BLDCx1S.Checked = false;
            }
            if (settings.CheckM2BLDCx2S == ("Yes"))
            {
                checkBoxM2BLDCx2S.Checked = true;
            }
            else
            {
                checkBoxM2BLDCx2S.Checked = false;
            }
            if (settings.CheckM2BLDCx3S == ("Yes"))
            {
                checkBoxM2BLDCx3S.Checked = true;
            }
            else
            {
                checkBoxM2BLDCx3S.Checked = false;
            }
            if (settings.CheckM2BLDCSpeedS == ("Yes"))
            {
                checkBoxM2BLDCSpeedS.Checked = true;
            }
            else
            {
                checkBoxM2BLDCSpeedR.Checked = false;
            }
            if (settings.CheckM2BLDCPositionR == ("Yes"))
            {
                checkBoxM2BLDCPositionR.Checked = true;
            }
            else
            {
                checkBoxM2BLDCPositionR.Checked = false;
            }
            if (settings.CheckM2BLDCTorqueR == ("Yes"))
            {
                checkBoxM2BLDCTorqueR.Checked = true;
            }
            else
            {
                checkBoxM2BLDCTorqueR.Checked = false;
            }
            if (settings.CheckM2BLDCPowerR == ("Yes"))
            {
                checkBoxM2BLDCPowerR.Checked = true;
            }
            else
            {
                checkBoxM2BLDCPowerR.Checked = false;
            }
            if (settings.CheckM2BLDCPR == ("Yes"))
            {
                checkBoxM2BLDCPR.Checked = true;
            }
            else
            {
                checkBoxM2BLDCPR.Checked = false;
            }
            if (settings.CheckM2BLDCIR == ("Yes"))
            {
                checkBoxM2BLDCIR.Checked = true;
            }
            else
            {
                checkBoxM2BLDCIR.Checked = false;
            }
            if (settings.CheckM2BLDCDR == ("Yes"))
            {
                checkBoxM2BLDCDR.Checked = true;
            }
            else
            {
                checkBoxM2BLDCDR.Checked = false;
            }
            if (settings.CheckM2BLDCx1R == ("Yes"))
            {
                checkBoxM2BLDCx1R.Checked = true;
            }
            else
            {
                checkBoxM2BLDCx1R.Checked = false;
            }
            if (settings.CheckM2BLDCx2R == ("Yes"))
            {
                checkBoxM2BLDCx2R.Checked = true;
            }
            else
            {
                checkBoxM2BLDCx2R.Checked = false;
            }
            if (settings.CheckM2BLDCx3R == ("Yes"))
            {
                checkBoxM2BLDCx3R.Checked = true;
            }
            else
            {
                checkBoxM2BLDCx3R.Checked = false;
            }
            if (settings.CheckM2PMSMSpeedS == ("Yes"))
            {
                checkBoxM2PMSMSpeedS.Checked = true;
            }
            else
            {
                checkBoxM2PMSMSpeedS.Checked = false;
            }
            if (settings.CheckM2PMSMPositionS == ("Yes"))
            {
                checkBoxM2PMSMPositionS.Checked = true;
            }
            else
            {
                checkBoxM2PMSMPositionS.Checked = false;
            }
            if (settings.CheckM2PMSMTorqueS == ("Yes"))
            {
                checkBoxM2PMSMTorqueS.Checked = true;
            }
            else
            {
                checkBoxM2PMSMTorqueS.Checked = false;
            }
            if (settings.CheckM2PMSMPowerS == ("Yes"))
            {
                checkBoxM2PMSMPowerS.Checked = true;
            }
            else
            {
                checkBoxM2PMSMPowerS.Checked = false;
            }
            if (settings.CheckM2PMSMPS == ("Yes"))
            {
                checkBoxM2PMSMPS.Checked = true;
            }
            else
            {
                checkBoxM2PMSMPS.Checked = false;
            }
            if (settings.CheckM2PMSMIS == ("Yes"))
            {
                checkBoxM2PMSMIS.Checked = true;
            }
            else
            {
                checkBoxM2PMSMIS.Checked = false;
            }
            if (settings.CheckM2PMSMDS == ("Yes"))
            {
                checkBoxM2PMSMDS.Checked = true;
            }
            else
            {
                checkBoxM2PMSMDS.Checked = false;
            }
            if (settings.CheckM2PMSMx1S == ("Yes"))
            {
                checkBoxM2PMSMx1S.Checked = true;
            }
            else
            {
                checkBoxM2PMSMx1S.Checked = false;
            }
            if (settings.CheckM2PMSMx2S == ("Yes"))
            {
                checkBoxM2PMSMx2S.Checked = true;
            }
            else
            {
                checkBoxM2PMSMx2S.Checked = false;
            }
            if (settings.CheckM2PMSMx3S == ("Yes"))
            {
                checkBoxM2PMSMx3S.Checked = true;
            }
            else
            {
                checkBoxM2PMSMx3S.Checked = false;
            }
            if (settings.CheckM2PMSMSpeedS == ("Yes"))
            {
                checkBoxM2PMSMSpeedS.Checked = true;
            }
            else
            {
                checkBoxM2PMSMSpeedR.Checked = false;
            }
            if (settings.CheckM2PMSMPositionR == ("Yes"))
            {
                checkBoxM2PMSMPositionR.Checked = true;
            }
            else
            {
                checkBoxM2PMSMPositionR.Checked = false;
            }
            if (settings.CheckM2PMSMTorqueR == ("Yes"))
            {
                checkBoxM2PMSMTorqueR.Checked = true;
            }
            else
            {
                checkBoxM2PMSMTorqueR.Checked = false;
            }
            if (settings.CheckM2PMSMPowerR == ("Yes"))
            {
                checkBoxM2PMSMPowerR.Checked = true;
            }
            else
            {
                checkBoxM2PMSMPowerR.Checked = false;
            }
            if (settings.CheckM2PMSMPR == ("Yes"))
            {
                checkBoxM2PMSMPR.Checked = true;
            }
            else
            {
                checkBoxM2PMSMPR.Checked = false;
            }
            if (settings.CheckM2PMSMIR == ("Yes"))
            {
                checkBoxM2PMSMIR.Checked = true;
            }
            else
            {
                checkBoxM2PMSMIR.Checked = false;
            }
            if (settings.CheckM2PMSMDR == ("Yes"))
            {
                checkBoxM2PMSMDR.Checked = true;
            }
            else
            {
                checkBoxM2PMSMDR.Checked = false;
            }
            if (settings.CheckM2PMSMx1R == ("Yes"))
            {
                checkBoxM2PMSMx1R.Checked = true;
            }
            else
            {
                checkBoxM2PMSMx1R.Checked = false;
            }
            if (settings.CheckM2PMSMx2R == ("Yes"))
            {
                checkBoxM2PMSMx2R.Checked = true;
            }
            else
            {
                checkBoxM2PMSMx2R.Checked = false;
            }
            if (settings.CheckM2PMSMx3R == ("Yes"))
            {
                checkBoxM2PMSMx3R.Checked = true;
            }
            else
            {
                checkBoxM2PMSMx3R.Checked = false;
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            SaveData();

            using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\config_default.xml", FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(fs, settings);
                MessageBox.Show("Created");
            }

        }
    }
}
