﻿using System;
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

            using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\config" + comboBoxSaveConfig.Text + ".xml", FileMode.Create, FileAccess.Write))
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
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\config.xml", FileMode.Open, FileAccess.Read))
                {
                    settings = serializer.Deserialize(fs) as Settings;

                }
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            settings.ReadGenAddr = txtBoxReadGenAddress.Text;
            settings.WriteGenAddr = txtBoxWriteGenAddress.Text;
            settings.ReadGenNOP = txtBoxReadGenNOP.Text;
            settings.WriteGenNOP = txtBoxWriteGenNOP.Text;
            settings.SlaveAddress = txtBoxSlaveAddress.Text;
            settings.AddrM1SpeedS = txtBoxM1SpeedS.Text;
            settings.AddrM1PositionS = txtBoxM1PositionS.Text;
            settings.AddrM1TorqueS = txtBoxM1TorqueS.Text;
            settings.AddrM1CurrentS = txtBoxM1CurrentS.Text;
            settings.AddrM1VoltageS = txtBoxM1VoltageS.Text;
            settings.AddrM1PS = txtBoxM1PS.Text;
            settings.AddrM1IS = txtBoxM1IS.Text;
            settings.AddrM1DS = txtBoxM1DS.Text;
            settings.AddrM1x1S = txtBoxM1x1S.Text;
            settings.AddrM1x2S = txtBoxM1x2S.Text;
            settings.AddrM1x3S = txtBoxM1x3S.Text;
            settings.AddrM1SpeedR = txtBoxM1SpeedR.Text;
            settings.AddrM1SpeedRNOP = txtBoxM1SpeedRNOP.Text;
            settings.AddrM1PositionR = txtBoxM1PositionR.Text;
            settings.AddrM1PositionRNOP = txtBoxM1PositionRNOP.Text;
            settings.AddrM1TorqueR = txtBoxM1TorqueR.Text;
            settings.AddrM1TorqueRNOP = txtBoxM1TorqueRNOP.Text;
            settings.AddrM1CurrentR = txtBoxM1CurrentR.Text;
            settings.AddrM1CurrentRNOP = txtBoxM1CurrentRNOP.Text;
            settings.AddrM1VoltageR = txtBoxM1VoltageR.Text;
            settings.AddrM1VoltageRNOP = txtBoxM1VoltageRNOP.Text;
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
            settings.AddrM2DCPositionS = txtBoxM2DCPositionS.Text;
            settings.AddrM2DCTorqueS = txtBoxM2DCTorqueS.Text;
            settings.AddrM2DCCurrentS = txtBoxM2DCCurrentS.Text;
            settings.AddrM2DCVoltageS = txtBoxM2DCVoltageS.Text;
            settings.AddrM2DCPS = txtBoxM2DCPS.Text;
            settings.AddrM2DCIS = txtBoxM2DCIS.Text;
            settings.AddrM2DCDS = txtBoxM2DCDS.Text;
            settings.AddrM2DCx1S = txtBoxM2DCx1S.Text;
            settings.AddrM2DCx2S = txtBoxM2DCx2S.Text;
            settings.AddrM2DCx3S = txtBoxM2DCx3S.Text;
            settings.AddrM2DCSpeedR = txtBoxM2DCSpeedR.Text;
            settings.AddrM2DCSpeedRNOP = txtBoxM2DCSpeedRNOP.Text;
            settings.AddrM2DCPositionR = txtBoxM2DCPositionR.Text;
            settings.AddrM2DCPositionRNOP = txtBoxM2DCPositionRNOP.Text;
            settings.AddrM2DCTorqueR = txtBoxM2DCTorqueR.Text;
            settings.AddrM2DCTorqueRNOP = txtBoxM2DCTorqueRNOP.Text;
            settings.AddrM2DCCurrentR = txtBoxM2DCCurrentR.Text;
            settings.AddrM2DCCurrentRNOP = txtBoxM2DCCurrentRNOP.Text;
            settings.AddrM2DCVoltageR = txtBoxM2DCVoltageR.Text;
            settings.AddrM2DCVoltageRNOP = txtBoxM2DCVoltageRNOP.Text;
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
            settings.AddrM2AsynchPositionS = txtBoxM2AsynchPositionS.Text;
            settings.AddrM2AsynchTorqueS = txtBoxM2AsynchTorqueS.Text;
            settings.AddrM2AsynchCurrentS = txtBoxM2AsynchCurrentS.Text;
            settings.AddrM2AsynchVoltageS = txtBoxM2AsynchVoltageS.Text;
            settings.AddrM2AsynchFrequencyS = txtBoxM2AsynchFrequencyS.Text;
            settings.AddrM2AsynchPS = txtBoxM2AsynchPS.Text;
            settings.AddrM2AsynchIS = txtBoxM2AsynchIS.Text;
            settings.AddrM2AsynchDS = txtBoxM2AsynchDS.Text;
            settings.AddrM2Asynchx1S = txtBoxM2Asynchx1S.Text;
            settings.AddrM2Asynchx2S = txtBoxM2Asynchx2S.Text;
            settings.AddrM2Asynchx3S = txtBoxM2Asynchx3S.Text;
            settings.AddrM2AsynchSpeedR = txtBoxM2AsynchSpeedR.Text;
            settings.AddrM2AsynchSpeedRNOP = txtBoxM2AsynchSpeedRNOP.Text;
            settings.AddrM2AsynchPositionR = txtBoxM2AsynchPositionR.Text;
            settings.AddrM2AsynchPositionRNOP = txtBoxM2AsynchPositionRNOP.Text;
            settings.AddrM2AsynchTorqueR = txtBoxM2AsynchTorqueR.Text;
            settings.AddrM2AsynchTorqueRNOP = txtBoxM2AsynchTorqueRNOP.Text;
            settings.AddrM2AsynchCurrentR = txtBoxM2AsynchCurrentR.Text;
            settings.AddrM2AsynchCurrentRNOP = txtBoxM2AsynchCurrentRNOP.Text;
            settings.AddrM2AsynchVoltageR = txtBoxM2AsynchVoltageR.Text;
            settings.AddrM2AsynchVoltageRNOP = txtBoxM2AsynchVoltageRNOP.Text;
            settings.AddrM2AsynchFrequencyR = txtBoxM2AsynchFrequencyR.Text;
            settings.AddrM2AsynchFrequencyRNOP = txtBoxM2AsynchFrequencyRNOP.Text;
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
            settings.AddrM2BLDCPositionS = txtBoxM2BLDCPositionS.Text;
            settings.AddrM2BLDCTorqueS = txtBoxM2BLDCTorqueS.Text;
            settings.AddrM2BLDCCurrentS = txtBoxM2BLDCCurrentS.Text;
            settings.AddrM2BLDCVoltageS = txtBoxM2BLDCVoltageS.Text;
            settings.AddrM2BLDCPS = txtBoxM2BLDCPS.Text;
            settings.AddrM2BLDCIS = txtBoxM2BLDCIS.Text;
            settings.AddrM2BLDCDS = txtBoxM2BLDCDS.Text;
            settings.AddrM2BLDCx1S = txtBoxM2BLDCx1S.Text;
            settings.AddrM2BLDCx2S = txtBoxM2BLDCx2S.Text;
            settings.AddrM2BLDCx3S = txtBoxM2BLDCx3S.Text;
            settings.AddrM2BLDCSpeedR = txtBoxM2BLDCSpeedR.Text;
            settings.AddrM2BLDCSpeedRNOP = txtBoxM2BLDCSpeedRNOP.Text;
            settings.AddrM2BLDCPositionR = txtBoxM2BLDCPositionR.Text;
            settings.AddrM2BLDCPositionRNOP = txtBoxM2BLDCPositionRNOP.Text;
            settings.AddrM2BLDCTorqueR = txtBoxM2BLDCTorqueR.Text;
            settings.AddrM2BLDCTorqueRNOP = txtBoxM2BLDCTorqueRNOP.Text;
            settings.AddrM2BLDCCurrentR = txtBoxM2BLDCCurrentR.Text;
            settings.AddrM2BLDCCurrentRNOP = txtBoxM2BLDCCurrentRNOP.Text;
            settings.AddrM2BLDCVoltageR = txtBoxM2BLDCVoltageR.Text;
            settings.AddrM2BLDCVoltageRNOP = txtBoxM2BLDCVoltageRNOP.Text;
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
            settings.AddrM2PMSMPositionS = txtBoxM2PMSMPositionS.Text;
            settings.AddrM2PMSMTorqueS = txtBoxM2PMSMTorqueS.Text;
            settings.AddrM2PMSMCurrentS = txtBoxM2PMSMCurrentS.Text;
            settings.AddrM2PMSMVoltageS = txtBoxM2PMSMVoltageS.Text;
            settings.AddrM2PMSMFrequencyS = txtBoxM2PMSMFrequencyS.Text;
            settings.AddrM2PMSMPS = txtBoxM2PMSMPS.Text;
            settings.AddrM2PMSMIS = txtBoxM2PMSMIS.Text;
            settings.AddrM2PMSMDS = txtBoxM2PMSMDS.Text;
            settings.AddrM2PMSMx1S = txtBoxM2PMSMx1S.Text;
            settings.AddrM2PMSMx2S = txtBoxM2PMSMx2S.Text;
            settings.AddrM2PMSMx3S = txtBoxM2PMSMx3S.Text;
            settings.AddrM2PMSMSpeedR = txtBoxM2PMSMSpeedR.Text;
            settings.AddrM2PMSMSpeedRNOP = txtBoxM2PMSMSpeedRNOP.Text;
            settings.AddrM2PMSMPositionR = txtBoxM2PMSMPositionR.Text;
            settings.AddrM2PMSMPositionRNOP = txtBoxM2PMSMPositionRNOP.Text;
            settings.AddrM2PMSMTorqueR = txtBoxM2PMSMTorqueR.Text;
            settings.AddrM2PMSMTorqueRNOP = txtBoxM2PMSMTorqueRNOP.Text;
            settings.AddrM2PMSMCurrentR = txtBoxM2PMSMCurrentR.Text;
            settings.AddrM2PMSMCurrentRNOP = txtBoxM2PMSMCurrentRNOP.Text;
            settings.AddrM2PMSMVoltageR = txtBoxM2PMSMVoltageR.Text;
            settings.AddrM2PMSMVoltageRNOP = txtBoxM2PMSMVoltageRNOP.Text;
            settings.AddrM2PMSMFrequencyR = txtBoxM2PMSMFrequencyR.Text;
            settings.AddrM2PMSMFrequencyRNOP = txtBoxM2PMSMFrequencyRNOP.Text;
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
                settings.CheckM1SpeedS = true;
            }
            else
            {
                settings.CheckM1SpeedS = false;
            }
            if (checkBoxM1PositionS.Checked)
            {
                settings.CheckM1PositionS = true;
            }
            else
            {
                settings.CheckM1PositionS = false;
            }
            if (checkBoxM1TorqueS.Checked)
            {
                settings.CheckM1TorqueS = true;
            }
            else
            {
                settings.CheckM1TorqueS = false;
            }
            if (checkBoxM1CurrentS.Checked)
            {
                settings.CheckM1CurrentS = true;
            }
            else
            {
                settings.CheckM1CurrentS = false;
            }
            if (checkBoxM1VoltageS.Checked)
            {
                settings.CheckM1VoltageS = true;
            }
            else
            {
                settings.CheckM1VoltageS = false;
            }
            if (checkBoxM1PS.Checked)
            {
                settings.CheckM1PS = true;
            }
            else
            {
                settings.CheckM1PS = false;
            }
            if (checkBoxM1IS.Checked)
            {
                settings.CheckM1IS = true;
            }
            else
            {
                settings.CheckM1IS = false;
            }
            if (checkBoxM1DS.Checked)
            {
                settings.CheckM1DS = true;
            }
            else
            {
                settings.CheckM1DS = false;
            }
            if (checkBoxM1x1S.Checked)
            {
                settings.CheckM1x1S = true;
            }
            else
            {
                settings.CheckM1x1S = false;
            }
            if (checkBoxM1x2S.Checked)
            {
                settings.CheckM1x2S = true;
            }
            else
            {
                settings.CheckM1x2S = false;
            }
            if (checkBoxM1x3S.Checked)
            {
                settings.CheckM1x3S = true;
            }
            else
            {
                settings.CheckM1x3S = false;
            }
            if (checkBoxM1SpeedR.Checked)
            {
                settings.CheckM1SpeedR = true;
            }
            else
            {
                settings.CheckM1SpeedR = false;
            }
            if (checkBoxM1PositionR.Checked)
            {
                settings.CheckM1PositionR = true;
            }
            else
            {
                settings.CheckM1PositionR = false;
            }
            if (checkBoxM1TorqueR.Checked)
            {
                settings.CheckM1TorqueR = true;
            }
            else
            {
                settings.CheckM1TorqueR = false;
            }
            if (checkBoxM1CurrentR.Checked)
            {
                settings.CheckM1CurrentR = true;
            }
            else
            {
                settings.CheckM1CurrentR = false;
            }
            if (checkBoxM1VoltageR.Checked)
            {
                settings.CheckM1VoltageR = true;
            }
            else
            {
                settings.CheckM1VoltageR = false;
            }
            if (checkBoxM1PR.Checked)
            {
                settings.CheckM1PR = true;
            }
            else
            {
                settings.CheckM1PR = false;
            }
            if (checkBoxM1IR.Checked)
            {
                settings.CheckM1IR = true;
            }
            else
            {
                settings.CheckM1IR = false;
            }
            if (checkBoxM1DR.Checked)
            {
                settings.CheckM1DR = true;
            }
            else
            {
                settings.CheckM1DR = false;
            }
            if (checkBoxM1x1R.Checked)
            {
                settings.CheckM1x1R = true;
            }
            else
            {
                settings.CheckM1x1R = false;
            }
            if (checkBoxM1x2R.Checked)
            {
                settings.CheckM1x2R = true;
            }
            else
            {
                settings.CheckM1x2R = false;
            }
            if (checkBoxM1x3R.Checked)
            {
                settings.CheckM1x3R = true;
            }
            else
            {
                settings.CheckM1x3R = false;
            }
            if (checkBoxM2DCSpeedS.Checked)
            {
                settings.CheckM2DCSpeedS = true;
            }
            else
            {
                settings.CheckM2DCSpeedS = false;
            }
            if (checkBoxM2DCPositionS.Checked)
            {
                settings.CheckM2DCPositionS = true;
            }
            else
            {
                settings.CheckM2DCPositionS = false;
            }
            if (checkBoxM2DCTorqueS.Checked)
            {
                settings.CheckM2DCTorqueS = true;
            }
            else
            {
                settings.CheckM2DCTorqueS = false;
            }
            if (checkBoxM2DCCurrentS.Checked)
            {
                settings.CheckM2DCCurrentS = true;
            }
            else
            {
                settings.CheckM2DCCurrentS = false;
            }
            if (checkBoxM2DCVoltageS.Checked)
            {
                settings.CheckM2DCVoltageS = true;
            }
            else
            {
                settings.CheckM2DCVoltageS = false;
            }
            if (checkBoxM2DCPS.Checked)
            {
                settings.CheckM2DCPS = true;
            }
            else
            {
                settings.CheckM2DCPS = false;
            }
            if (checkBoxM2DCIS.Checked)
            {
                settings.CheckM2DCIS = true;
            }
            else
            {
                settings.CheckM2DCIS = false;
            }
            if (checkBoxM2DCDS.Checked)
            {
                settings.CheckM2DCDS = true;
            }
            else
            {
                settings.CheckM2DCDS = false;
            }
            if (checkBoxM2DCx1S.Checked)
            {
                settings.CheckM2DCx1S = true;
            }
            else
            {
                settings.CheckM2DCx1S = false;
            }
            if (checkBoxM2DCx2S.Checked)
            {
                settings.CheckM2DCx2S = true;
            }
            else
            {
                settings.CheckM2DCx2S = false;
            }
            if (checkBoxM2DCx3S.Checked)
            {
                settings.CheckM2DCx3S = true;
            }
            else
            {
                settings.CheckM2DCx3S = false;
            }
            if (checkBoxM2DCSpeedR.Checked)
            {
                settings.CheckM2DCSpeedR = true;
            }
            else
            {
                settings.CheckM2DCSpeedR = false;
            }
            if (checkBoxM2DCPositionR.Checked)
            {
                settings.CheckM2DCPositionR = true;
            }
            else
            {
                settings.CheckM2DCPositionR = false;
            }
            if (checkBoxM2DCTorqueR.Checked)
            {
                settings.CheckM2DCTorqueR = true;
            }
            else
            {
                settings.CheckM2DCTorqueR = false;
            }
            if (checkBoxM2DCCurrentR.Checked)
            {
                settings.CheckM2DCCurrentR = true;
            }
            else
            {
                settings.CheckM2DCCurrentR = false;
            }
            if (checkBoxM2DCVoltageR.Checked)
            {
                settings.CheckM2DCVoltageR = true;
            }
            else
            {
                settings.CheckM2DCVoltageR = false;
            }
            if (checkBoxM2DCPR.Checked)
            {
                settings.CheckM2DCPR = true;
            }
            else
            {
                settings.CheckM2DCPR = false;
            }
            if (checkBoxM2DCIR.Checked)
            {
                settings.CheckM2DCIR = true;
            }
            else
            {
                settings.CheckM2DCIR = false;
            }
            if (checkBoxM2DCDR.Checked)
            {
                settings.CheckM2DCDR = true;
            }
            else
            {
                settings.CheckM2DCDR = false;
            }
            if (checkBoxM2DCx1R.Checked)
            {
                settings.CheckM2DCx1R = true;
            }
            else
            {
                settings.CheckM2DCx1R = false;
            }
            if (checkBoxM2DCx2R.Checked)
            {
                settings.CheckM2DCx2R = true;
            }
            else
            {
                settings.CheckM2DCx2R = false;
            }
            if (checkBoxM2DCx3R.Checked)
            {
                settings.CheckM2DCx3R = true;
            }
            else
            {
                settings.CheckM2DCx3R = false;
            }
            if (checkBoxM2AsynchSpeedS.Checked)
            {
                settings.CheckM2AsynchSpeedS = true;
            }
            else
            {
                settings.CheckM2AsynchSpeedS = false;
            }
            if (checkBoxM2AsynchPositionS.Checked)
            {
                settings.CheckM2AsynchPositionS = true;
            }
            else
            {
                settings.CheckM2AsynchPositionS = false;
            }
            if (checkBoxM2AsynchTorqueS.Checked)
            {
                settings.CheckM2AsynchTorqueS = true;
            }
            else
            {
                settings.CheckM2AsynchTorqueS = false;
            }
            if (checkBoxM2AsynchCurrentS.Checked)
            {
                settings.CheckM2AsynchCurrentS = true;
            }
            else
            {
                settings.CheckM2AsynchCurrentS = false;
            }
            if (checkBoxM2AsynchVoltageS.Checked)
            {
                settings.CheckM2AsynchVoltageS = true;
            }
            else
            {
                settings.CheckM2AsynchVoltageS = false;
            }
            if (checkBoxM2AsynchFrequencyS.Checked)
            {
                settings.CheckM2AsynchFrequencyS = true;
            }
            else
            {
                settings.CheckM2AsynchFrequencyS = false;
            }
            if (checkBoxM2AsynchPS.Checked)
            {
                settings.CheckM2AsynchPS = true;
            }
            else
            {
                settings.CheckM2AsynchPS = false;
            }
            if (checkBoxM2AsynchIS.Checked)
            {
                settings.CheckM2AsynchIS = true;
            }
            else
            {
                settings.CheckM2AsynchIS = false;
            }
            if (checkBoxM2AsynchDS.Checked)
            {
                settings.CheckM2AsynchDS = true;
            }
            else
            {
                settings.CheckM2AsynchDS = false;
            }
            if (checkBoxM2Asynchx1S.Checked)
            {
                settings.CheckM2Asynchx1S = true;
            }
            else
            {
                settings.CheckM2Asynchx1S = false;
            }
            if (checkBoxM2Asynchx2S.Checked)
            {
                settings.CheckM2Asynchx2S = true;
            }
            else
            {
                settings.CheckM2Asynchx2S = false;
            }
            if (checkBoxM2Asynchx3S.Checked)
            {
                settings.CheckM2Asynchx3S = true;
            }
            else
            {
                settings.CheckM2Asynchx3S = false;
            }
            if (checkBoxM2AsynchSpeedR.Checked)
            {
                settings.CheckM2AsynchSpeedR = true;
            }
            else
            {
                settings.CheckM2AsynchSpeedR = false;
            }
            if (checkBoxM2AsynchPositionR.Checked)
            {
                settings.CheckM2AsynchPositionR = true;
            }
            else
            {
                settings.CheckM2AsynchPositionR = false;
            }
            if (checkBoxM2AsynchTorqueR.Checked)
            {
                settings.CheckM2AsynchTorqueR = true;
            }
            else
            {
                settings.CheckM2AsynchTorqueR = false;
            }
            if (checkBoxM2AsynchCurrentR.Checked)
            {
                settings.CheckM2AsynchCurrentR = true;
            }
            else
            {
                settings.CheckM2AsynchCurrentR = false;
            }
            if (checkBoxM2AsynchVoltageR.Checked)
            {
                settings.CheckM2AsynchVoltageR = true;
            }
            else
            {
                settings.CheckM2AsynchVoltageR = false;
            }
            if (checkBoxM2AsynchFrequencyR.Checked)
            {
                settings.CheckM2AsynchFrequencyR = true;
            }
            else
            {
                settings.CheckM2AsynchFrequencyR = false;
            }
            if (checkBoxM2AsynchPR.Checked)
            {
                settings.CheckM2AsynchPR = true;
            }
            else
            {
                settings.CheckM2AsynchPR = false;
            }
            if (checkBoxM2AsynchIR.Checked)
            {
                settings.CheckM2AsynchIR = true;
            }
            else
            {
                settings.CheckM2AsynchIR = false;
            }
            if (checkBoxM2AsynchDR.Checked)
            {
                settings.CheckM2AsynchDR = true;
            }
            else
            {
                settings.CheckM2AsynchDR = false;
            }
            if (checkBoxM2Asynchx1R.Checked)
            {
                settings.CheckM2Asynchx1R = true;
            }
            else
            {
                settings.CheckM2Asynchx1R = false;
            }
            if (checkBoxM2Asynchx2R.Checked)
            {
                settings.CheckM2Asynchx2R = true;
            }
            else
            {
                settings.CheckM2Asynchx2R = false;
            }
            if (checkBoxM2Asynchx3R.Checked)
            {
                settings.CheckM2Asynchx3R = true;
            }
            else
            {
                settings.CheckM2Asynchx3R = false;
            }
            if (checkBoxM2BLDCSpeedS.Checked)
            {
                settings.CheckM2BLDCSpeedS = true;
            }
            else
            {
                settings.CheckM2BLDCSpeedS = false;
            }
            if (checkBoxM2BLDCPositionS.Checked)
            {
                settings.CheckM2BLDCPositionS = true;
            }
            else
            {
                settings.CheckM2BLDCPositionS = false;
            }
            if (checkBoxM2BLDCTorqueS.Checked)
            {
                settings.CheckM2BLDCTorqueS = true;
            }
            else
            {
                settings.CheckM2BLDCTorqueS = false;
            }
            if (checkBoxM2BLDCCurrentS.Checked)
            {
                settings.CheckM2BLDCCurrentS = true;
            }
            else
            {
                settings.CheckM2BLDCCurrentS = false;
            }
            if (checkBoxM2BLDCVoltageS.Checked)
            {
                settings.CheckM2BLDCVoltageS = true;
            }
            else
            {
                settings.CheckM2BLDCVoltageS = false;
            }
            if (checkBoxM2BLDCPS.Checked)
            {
                settings.CheckM2BLDCPS = true;
            }
            else
            {
                settings.CheckM2BLDCPS = false;
            }
            if (checkBoxM2BLDCIS.Checked)
            {
                settings.CheckM2BLDCIS = true;
            }
            else
            {
                settings.CheckM2BLDCIS = false;
            }
            if (checkBoxM2BLDCDS.Checked)
            {
                settings.CheckM2BLDCDS = true;
            }
            else
            {
                settings.CheckM2BLDCDS = false;
            }
            if (checkBoxM2BLDCx1S.Checked)
            {
                settings.CheckM2BLDCx1S = true;
            }
            else
            {
                settings.CheckM2BLDCx1S = false;
            }
            if (checkBoxM2BLDCx2S.Checked)
            {
                settings.CheckM2BLDCx2S = true;
            }
            else
            {
                settings.CheckM2BLDCx2S = false;
            }
            if (checkBoxM2BLDCx3S.Checked)
            {
                settings.CheckM2BLDCx3S = true;
            }
            else
            {
                settings.CheckM2BLDCx3S = false;
            }
            if (checkBoxM2BLDCSpeedR.Checked)
            {
                settings.CheckM2BLDCSpeedR = true;
            }
            else
            {
                settings.CheckM2BLDCSpeedR = false;
            }
            if (checkBoxM2BLDCPositionR.Checked)
            {
                settings.CheckM2BLDCPositionR = true;
            }
            else
            {
                settings.CheckM2BLDCPositionR = false;
            }
            if (checkBoxM2BLDCTorqueR.Checked)
            {
                settings.CheckM2BLDCTorqueR = true;
            }
            else
            {
                settings.CheckM2BLDCTorqueR = false;
            }
            if (checkBoxM2BLDCCurrentR.Checked)
            {
                settings.CheckM2BLDCCurrentR = true;
            }
            else
            {
                settings.CheckM2BLDCCurrentR = false;
            }
            if (checkBoxM2BLDCVoltageR.Checked)
            {
                settings.CheckM2BLDCVoltageR = true;
            }
            else
            {
                settings.CheckM2BLDCVoltageR = false;
            }
            if (checkBoxM2BLDCPR.Checked)
            {
                settings.CheckM2BLDCPR = true;
            }
            else
            {
                settings.CheckM2BLDCPR = false;
            }
            if (checkBoxM2BLDCIR.Checked)
            {
                settings.CheckM2BLDCIR = true;
            }
            else
            {
                settings.CheckM2BLDCIR = false;
            }
            if (checkBoxM2BLDCDR.Checked)
            {
                settings.CheckM2BLDCDR = true;
            }
            else
            {
                settings.CheckM2BLDCDR = false;
            }
            if (checkBoxM2BLDCx1R.Checked)
            {
                settings.CheckM2BLDCx1R = true;
            }
            else
            {
                settings.CheckM2BLDCx1R = false;
            }
            if (checkBoxM2BLDCx2R.Checked)
            {
                settings.CheckM2BLDCx2R = true;
            }
            else
            {
                settings.CheckM2BLDCx2R = false;
            }
            if (checkBoxM2BLDCx3R.Checked)
            {
                settings.CheckM2BLDCx3R = true;
            }
            else
            {
                settings.CheckM2BLDCx3R = false;
            }
            if (checkBoxM2PMSMSpeedS.Checked)
            {
                settings.CheckM2PMSMSpeedS = true;
            }
            else
            {
                settings.CheckM2PMSMSpeedS = false;
            }
            if (checkBoxM2PMSMPositionS.Checked)
            {
                settings.CheckM2PMSMPositionS = true;
            }
            else
            {
                settings.CheckM2PMSMPositionS = false;
            }
            if (checkBoxM2PMSMTorqueS.Checked)
            {
                settings.CheckM2PMSMTorqueS = true;
            }
            else
            {
                settings.CheckM2PMSMTorqueS = false;
            }
            if (checkBoxM2PMSMCurrentS.Checked)
            {
                settings.CheckM2PMSMCurrentS = true;
            }
            else
            {
                settings.CheckM2PMSMCurrentS = false;
            }
            if (checkBoxM2PMSMVoltageS.Checked)
            {
                settings.CheckM2PMSMVoltageS = true;
            }
            else
            {
                settings.CheckM2PMSMVoltageS = false;
            }
            if (checkBoxM2PMSMFrequencyS.Checked)
            {
                settings.CheckM2PMSMFrequencyS = true;
            }
            else
            {
                settings.CheckM2PMSMFrequencyS = false;
            }
            if (checkBoxM2PMSMPS.Checked)
            {
                settings.CheckM2PMSMPS = true;
            }
            else
            {
                settings.CheckM2PMSMPS = false;
            }
            if (checkBoxM2PMSMIS.Checked)
            {
                settings.CheckM2PMSMIS = true;
            }
            else
            {
                settings.CheckM2PMSMIS = false;
            }
            if (checkBoxM2PMSMDS.Checked)
            {
                settings.CheckM2PMSMDS = true;
            }
            else
            {
                settings.CheckM2PMSMDS = false;
            }
            if (checkBoxM2PMSMx1S.Checked)
            {
                settings.CheckM2PMSMx1S = true;
            }
            else
            {
                settings.CheckM2PMSMx1S = false;
            }
            if (checkBoxM2PMSMx2S.Checked)
            {
                settings.CheckM2PMSMx2S = true;
            }
            else
            {
                settings.CheckM2PMSMx2S = false;
            }
            if (checkBoxM2PMSMx3S.Checked)
            {
                settings.CheckM2PMSMx3S = true;
            }
            else
            {
                settings.CheckM2PMSMx3S = false;
            }
            if (checkBoxM2PMSMSpeedR.Checked)
            {
                settings.CheckM2PMSMSpeedR = true;
            }
            else
            {
                settings.CheckM2PMSMSpeedR = false;
            }
            if (checkBoxM2PMSMPositionR.Checked)
            {
                settings.CheckM2PMSMPositionR = true;
            }
            else
            {
                settings.CheckM2PMSMPositionR = false;
            }
            if (checkBoxM2PMSMTorqueR.Checked)
            {
                settings.CheckM2PMSMTorqueR = true;
            }
            else
            {
                settings.CheckM2PMSMTorqueR = false;
            }
            if (checkBoxM2PMSMCurrentR.Checked)
            {
                settings.CheckM2PMSMCurrentR = true;
            }
            else
            {
                settings.CheckM2PMSMCurrentR = false;
            }
            if (checkBoxM2PMSMVoltageR.Checked)
            {
                settings.CheckM2PMSMVoltageR = true;
            }
            else
            {
                settings.CheckM2PMSMVoltageR = false;
            }
            if (checkBoxM2PMSMFrequencyR.Checked)
            {
                settings.CheckM2PMSMFrequencyR = true;
            }
            else
            {
                settings.CheckM2PMSMFrequencyR = false;
            }
            if (checkBoxM2PMSMPR.Checked)
            {
                settings.CheckM2PMSMPR = true;
            }
            else
            {
                settings.CheckM2PMSMPR = false;
            }
            if (checkBoxM2PMSMIR.Checked)
            {
                settings.CheckM2PMSMIR = true;
            }
            else
            {
                settings.CheckM2PMSMIR = false;
            }
            if (checkBoxM2PMSMDR.Checked)
            {
                settings.CheckM2PMSMDR = true;
            }
            else
            {
                settings.CheckM2PMSMDR = false;
            }
            if (checkBoxM2PMSMx1R.Checked)
            {
                settings.CheckM2PMSMx1R = true;
            }
            else
            {
                settings.CheckM2PMSMx1R = false;
            }
            if (checkBoxM2PMSMx2R.Checked)
            {
                settings.CheckM2PMSMx2R = true;
            }
            else
            {
                settings.CheckM2PMSMx2R = false;
            }
            if (checkBoxM2PMSMx3R.Checked)
            {
                settings.CheckM2PMSMx3R = true;
            }
            else
            {
                settings.CheckM2PMSMx3R = false;
            }
        }
        private void LoadData()
        {
            cBoxBaudRate.Text = settings.CPBaudRate;
            cBoxDataBits.Text = settings.CPDataBits;
            cBoxParityBits.Text = settings.CPParity;
            cBoxStopBits.Text = settings.CPStopBits;
            txtBoxReadGenAddress.Text = settings.ReadGenAddr;
            txtBoxWriteGenAddress.Text = settings.WriteGenAddr;
            txtBoxReadGenNOP.Text = settings.ReadGenNOP;
            txtBoxWriteGenNOP.Text = settings.WriteGenNOP;
            txtBoxSlaveAddress.Text = settings.SlaveAddress;
            txtBoxM1SpeedS.Text = settings.AddrM1SpeedS;
            txtBoxM1PositionS.Text = settings.AddrM1PositionS;
            txtBoxM1TorqueS.Text = settings.AddrM1TorqueS;
            txtBoxM1CurrentS.Text = settings.AddrM1CurrentS;
            txtBoxM1VoltageS.Text = settings.AddrM1VoltageS;
            txtBoxM1PS.Text = settings.AddrM1PS;
            txtBoxM1IS.Text = settings.AddrM1IS;
            txtBoxM1DS.Text = settings.AddrM1DS;
            txtBoxM1x1S.Text = settings.AddrM1x1S;
            txtBoxM1x2S.Text = settings.AddrM1x2S;
            txtBoxM1x3S.Text = settings.AddrM1x3S;
            txtBoxM1SpeedR.Text = settings.AddrM1SpeedR;
            txtBoxM1SpeedRNOP.Text = settings.AddrM1SpeedRNOP;
            txtBoxM1PositionR.Text = settings.AddrM1PositionR;
            txtBoxM1PositionRNOP.Text = settings.AddrM1PositionRNOP;
            txtBoxM1TorqueR.Text = settings.AddrM1TorqueR;
            txtBoxM1TorqueRNOP.Text = settings.AddrM1TorqueRNOP;
            txtBoxM1CurrentR.Text = settings.AddrM1CurrentR;
            txtBoxM1CurrentRNOP.Text = settings.AddrM1CurrentRNOP;
            txtBoxM1VoltageR.Text = settings.AddrM1VoltageR;
            txtBoxM1VoltageRNOP.Text = settings.AddrM1VoltageRNOP;
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
            txtBoxM2DCPositionS.Text = settings.AddrM2DCPositionS;
            txtBoxM2DCTorqueS.Text = settings.AddrM2DCTorqueS;
            txtBoxM2DCCurrentS.Text = settings.AddrM2DCCurrentS;
            txtBoxM2DCVoltageS.Text = settings.AddrM2DCVoltageS;
            txtBoxM2DCVoltageS.Text = settings.AddrM2DCVoltageS;
            txtBoxM2DCPS.Text = settings.AddrM2DCPS;
            txtBoxM2DCIS.Text = settings.AddrM2DCIS;
            txtBoxM2DCDS.Text = settings.AddrM2DCDS;
            txtBoxM2DCx1S.Text = settings.AddrM2DCx1S;
            txtBoxM2DCx2S.Text = settings.AddrM2DCx2S;
            txtBoxM2DCx3S.Text = settings.AddrM2DCx3S;
            txtBoxM2DCSpeedR.Text = settings.AddrM2DCSpeedR;
            txtBoxM2DCSpeedRNOP.Text = settings.AddrM2DCSpeedRNOP;
            txtBoxM2DCPositionR.Text = settings.AddrM2DCPositionR;
            txtBoxM2DCPositionRNOP.Text = settings.AddrM2DCPositionRNOP;
            txtBoxM2DCTorqueR.Text = settings.AddrM2DCTorqueR;
            txtBoxM2DCTorqueRNOP.Text = settings.AddrM2DCTorqueRNOP;
            txtBoxM2DCCurrentR.Text = settings.AddrM2DCCurrentR;
            txtBoxM2DCCurrentRNOP.Text = settings.AddrM2DCCurrentRNOP;
            txtBoxM2DCVoltageR.Text = settings.AddrM2DCVoltageR;
            txtBoxM2DCVoltageRNOP.Text = settings.AddrM2DCVoltageRNOP;
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
            txtBoxM2AsynchPositionS.Text = settings.AddrM2AsynchPositionS;
            txtBoxM2AsynchTorqueS.Text = settings.AddrM2AsynchTorqueS;
            txtBoxM2AsynchCurrentS.Text = settings.AddrM2AsynchCurrentS;
            txtBoxM2AsynchVoltageS.Text = settings.AddrM2AsynchVoltageS;
            txtBoxM2AsynchFrequencyS.Text = settings.AddrM2AsynchFrequencyS;
            txtBoxM2AsynchPS.Text = settings.AddrM2AsynchPS;
            txtBoxM2AsynchIS.Text = settings.AddrM2AsynchIS;
            txtBoxM2AsynchDS.Text = settings.AddrM2AsynchDS;
            txtBoxM2Asynchx1S.Text = settings.AddrM2Asynchx1S;
            txtBoxM2Asynchx2S.Text = settings.AddrM2Asynchx2S;
            txtBoxM2Asynchx3S.Text = settings.AddrM2Asynchx3S;
            txtBoxM2AsynchSpeedR.Text = settings.AddrM2AsynchSpeedR;
            txtBoxM2AsynchSpeedRNOP.Text = settings.AddrM2AsynchSpeedRNOP;
            txtBoxM2AsynchPositionR.Text = settings.AddrM2AsynchPositionR;
            txtBoxM2AsynchPositionRNOP.Text = settings.AddrM2AsynchPositionRNOP;
            txtBoxM2AsynchTorqueR.Text = settings.AddrM2AsynchTorqueR;
            txtBoxM2AsynchTorqueRNOP.Text = settings.AddrM2AsynchTorqueRNOP;
            txtBoxM2AsynchCurrentR.Text = settings.AddrM2AsynchCurrentR;
            txtBoxM2AsynchCurrentRNOP.Text = settings.AddrM2AsynchCurrentRNOP;
            txtBoxM2AsynchVoltageR.Text = settings.AddrM2AsynchVoltageR;
            txtBoxM2AsynchVoltageRNOP.Text = settings.AddrM2AsynchVoltageRNOP;
            txtBoxM2AsynchFrequencyR.Text = settings.AddrM2AsynchFrequencyR;
            txtBoxM2AsynchFrequencyRNOP.Text = settings.AddrM2AsynchFrequencyRNOP;
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
            txtBoxM2BLDCPositionS.Text = settings.AddrM2BLDCPositionS;
            txtBoxM2BLDCTorqueS.Text = settings.AddrM2BLDCTorqueS;
            txtBoxM2BLDCCurrentS.Text = settings.AddrM2BLDCCurrentS;
            txtBoxM2BLDCVoltageS.Text = settings.AddrM2BLDCVoltageS;
            txtBoxM2BLDCPS.Text = settings.AddrM2BLDCPS;
            txtBoxM2BLDCIS.Text = settings.AddrM2BLDCIS;
            txtBoxM2BLDCDS.Text = settings.AddrM2BLDCDS;
            txtBoxM2BLDCx1S.Text = settings.AddrM2BLDCx1S;
            txtBoxM2BLDCx2S.Text = settings.AddrM2BLDCx2S;
            txtBoxM2BLDCx3S.Text = settings.AddrM2BLDCx3S;
            txtBoxM2BLDCSpeedR.Text = settings.AddrM2BLDCSpeedR;
            txtBoxM2BLDCSpeedRNOP.Text = settings.AddrM2BLDCSpeedRNOP;
            txtBoxM2BLDCPositionR.Text = settings.AddrM2BLDCPositionR;
            txtBoxM2BLDCPositionRNOP.Text = settings.AddrM2BLDCPositionRNOP;
            txtBoxM2BLDCTorqueR.Text = settings.AddrM2BLDCTorqueR;
            txtBoxM2BLDCTorqueRNOP.Text = settings.AddrM2BLDCTorqueRNOP;
            txtBoxM2BLDCCurrentR.Text = settings.AddrM2BLDCCurrentR;
            txtBoxM2BLDCCurrentRNOP.Text = settings.AddrM2BLDCCurrentRNOP;
            txtBoxM2BLDCVoltageR.Text = settings.AddrM2BLDCVoltageR;
            txtBoxM2BLDCVoltageRNOP.Text = settings.AddrM2BLDCVoltageRNOP;
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
            txtBoxM2PMSMPositionS.Text = settings.AddrM2PMSMPositionS;
            txtBoxM2PMSMTorqueS.Text = settings.AddrM2PMSMTorqueS;
            txtBoxM2PMSMCurrentS.Text = settings.AddrM2PMSMCurrentS;
            txtBoxM2PMSMVoltageS.Text = settings.AddrM2PMSMVoltageS;
            txtBoxM2PMSMFrequencyS.Text = settings.AddrM2PMSMFrequencyS;
            txtBoxM2PMSMPS.Text = settings.AddrM2PMSMPS;
            txtBoxM2PMSMIS.Text = settings.AddrM2PMSMIS;
            txtBoxM2PMSMDS.Text = settings.AddrM2PMSMDS;
            txtBoxM2PMSMx1S.Text = settings.AddrM2PMSMx1S;
            txtBoxM2PMSMx2S.Text = settings.AddrM2PMSMx2S;
            txtBoxM2PMSMSpeedR.Text = settings.AddrM2PMSMSpeedR;
            txtBoxM2PMSMSpeedRNOP.Text = settings.AddrM2PMSMSpeedRNOP;
            txtBoxM2PMSMPositionR.Text = settings.AddrM2PMSMPositionR;
            txtBoxM2PMSMPositionRNOP.Text = settings.AddrM2PMSMPositionRNOP;
            txtBoxM2PMSMTorqueR.Text = settings.AddrM2PMSMTorqueR;
            txtBoxM2PMSMTorqueRNOP.Text = settings.AddrM2PMSMTorqueRNOP;
            txtBoxM2PMSMCurrentR.Text = settings.AddrM2PMSMCurrentR;
            txtBoxM2PMSMCurrentRNOP.Text = settings.AddrM2PMSMCurrentRNOP;
            txtBoxM2PMSMVoltageR.Text = settings.AddrM2PMSMVoltageR;
            txtBoxM2PMSMVoltageRNOP.Text = settings.AddrM2PMSMVoltageRNOP;
            txtBoxM2PMSMFrequencyR.Text = settings.AddrM2PMSMFrequencyR;
            txtBoxM2PMSMFrequencyRNOP.Text = settings.AddrM2PMSMFrequencyRNOP;
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

            if (settings.CheckM1SpeedS == true)
            {
                checkBoxM1SpeedS.Checked = true;
            }
            else
            {
                checkBoxM1SpeedS.Checked = false;
            }
            if (settings.CheckM1PositionS == true)
            {
                checkBoxM1PositionS.Checked = true;
            }
            else
            {
                checkBoxM1PositionS.Checked = false;
            }
            if (settings.CheckM1TorqueS == true)
            {
                checkBoxM1TorqueS.Checked = true;
            }
            else
            {
                checkBoxM1TorqueS.Checked = false;
            }
            if (settings.CheckM1CurrentS == true)
            {
                checkBoxM1CurrentS.Checked = true;
            }
            else
            {
                checkBoxM1CurrentS.Checked = false;
            }
            if (settings.CheckM1VoltageS == true)
            {
                checkBoxM1VoltageS.Checked = true;
            }
            else
            {
                checkBoxM1VoltageS.Checked = false;
            }
            if (settings.CheckM1PS == true)
            {
                checkBoxM1PS.Checked = true;
            }
            else
            {
                checkBoxM1PS.Checked = false;
            }
            if (settings.CheckM1IS == true)
            {
                checkBoxM1IS.Checked = true;
            }
            else
            {
                checkBoxM1IS.Checked = false;
            }
            if (settings.CheckM1DS == true)
            {
                checkBoxM1DS.Checked = true;
            }
            else
            {
                checkBoxM1DS.Checked = false;
            }
            if (settings.CheckM1x1S == true)
            {
                checkBoxM1x1S.Checked = true;
            }
            else
            {
                checkBoxM1x1S.Checked = false;
            }
            if (settings.CheckM1x2S == true)
            {
                checkBoxM1x2S.Checked = true;
            }
            else
            {
                checkBoxM1x2S.Checked = false;
            }
            if (settings.CheckM1x3S == true)
            {
                checkBoxM1x3S.Checked = true;
            }
            else
            {
                checkBoxM1x3S.Checked = false;
            }
            if (settings.CheckM1SpeedR == true)
            {
                checkBoxM1SpeedR.Checked = true;
            }
            else
            {
                checkBoxM1SpeedR.Checked = false;
            }
            if (settings.CheckM1PositionR == true)
            {
                checkBoxM1PositionR.Checked = true;
            }
            else
            {
                checkBoxM1PositionR.Checked = false;
            }
            if (settings.CheckM1TorqueR == true)
            {
                checkBoxM1TorqueR.Checked = true;
            }
            else
            {
                checkBoxM1TorqueR.Checked = false;
            }
            if (settings.CheckM1CurrentR == true)
            {
                checkBoxM1CurrentR.Checked = true;
            }
            else
            {
                checkBoxM1CurrentR.Checked = false;
            }
            if (settings.CheckM1VoltageR == true)
            {
                checkBoxM1VoltageR.Checked = true;
            }
            else
            {
                checkBoxM1VoltageR.Checked = false;
            }
            if (settings.CheckM1PR == true)
            {
                checkBoxM1PR.Checked = true;
            }
            else
            {
                checkBoxM1PR.Checked = false;
            }
            if (settings.CheckM1IR == true)
            {
                checkBoxM1IR.Checked = true;
            }
            else
            {
                checkBoxM1IR.Checked = false;
            }
            if (settings.CheckM1DR == true)
            {
                checkBoxM1DR.Checked = true;
            }
            else
            {
                checkBoxM1DR.Checked = false;
            }
            if (settings.CheckM1x1R == true)
            {
                checkBoxM1x1R.Checked = true;
            }
            else
            {
                checkBoxM1x1R.Checked = false;
            }
            if (settings.CheckM1x2R == true)
            {
                checkBoxM1x2R.Checked = true;
            }
            else
            {
                checkBoxM1x2R.Checked = false;
            }
            if (settings.CheckM1x3R == true)
            {
                checkBoxM1x3R.Checked = true;
            }
            else
            {
                checkBoxM1x3R.Checked = false;
            }
            if (settings.CheckM2DCSpeedS == true)
            {
                checkBoxM2DCSpeedS.Checked = true;
            }
            else
            {
                checkBoxM2DCSpeedS.Checked = false;
            }
            if (settings.CheckM2DCPositionS == true)
            {
                checkBoxM2DCPositionS.Checked = true;
            }
            else
            {
                checkBoxM2DCPositionS.Checked = false;
            }
            if (settings.CheckM2DCTorqueS == true)
            {
                checkBoxM2DCTorqueS.Checked = true;
            }
            else
            {
                checkBoxM2DCTorqueS.Checked = false;
            }
            if (settings.CheckM2DCCurrentS == true)
            {
                checkBoxM2DCCurrentS.Checked = true;
            }
            else
            {
                checkBoxM2DCCurrentS.Checked = false;
            }
            if (settings.CheckM2DCVoltageS == true)
            {
                checkBoxM2DCVoltageS.Checked = true;
            }
            else
            {
                checkBoxM2DCVoltageS.Checked = false;
            }
            if (settings.CheckM2DCPS == true)
            {
                checkBoxM2DCPS.Checked = true;
            }
            else
            {
                checkBoxM2DCPS.Checked = false;
            }
            if (settings.CheckM2DCIS == true)
            {
                checkBoxM2DCIS.Checked = true;
            }
            else
            {
                checkBoxM2DCIS.Checked = false;
            }
            if (settings.CheckM2DCDS == true)
            {
                checkBoxM2DCDS.Checked = true;
            }
            else
            {
                checkBoxM2DCDS.Checked = false;
            }
            if (settings.CheckM2DCx1S == true)
            {
                checkBoxM2DCx1S.Checked = true;
            }
            else
            {
                checkBoxM2DCx1S.Checked = false;
            }
            if (settings.CheckM2DCx2S == true)
            {
                checkBoxM2DCx2S.Checked = true;
            }
            else
            {
                checkBoxM2DCx2S.Checked = false;
            }
            if (settings.CheckM2DCx3S == true)
            {
                checkBoxM2DCx3S.Checked = true;
            }
            else
            {
                checkBoxM2DCx3S.Checked = false;
            }
            if (settings.CheckM2DCSpeedS == true)
            {
                checkBoxM2DCSpeedS.Checked = true;
            }
            else
            {
                checkBoxM2DCSpeedR.Checked = false;
            }
            if (settings.CheckM2DCPositionR == true)
            {
                checkBoxM2DCPositionR.Checked = true;
            }
            else
            {
                checkBoxM2DCPositionR.Checked = false;
            }
            if (settings.CheckM2DCTorqueR == true)
            {
                checkBoxM2DCTorqueR.Checked = true;
            }
            else
            {
                checkBoxM2DCTorqueR.Checked = false;
            }
            if (settings.CheckM2DCCurrentR == true)
            {
                checkBoxM2DCCurrentR.Checked = true;
            }
            else
            {
                checkBoxM2DCCurrentR.Checked = false;
            }
            if (settings.CheckM2DCVoltageR == true)
            {
                checkBoxM2DCVoltageR.Checked = true;
            }
            else
            {
                checkBoxM2DCVoltageR.Checked = false;
            }
            if (settings.CheckM2DCPR == true)
            {
                checkBoxM2DCPR.Checked = true;
            }
            else
            {
                checkBoxM2DCPR.Checked = false;
            }
            if (settings.CheckM2DCIR == true)
            {
                checkBoxM2DCIR.Checked = true;
            }
            else
            {
                checkBoxM2DCIR.Checked = false;
            }
            if (settings.CheckM2DCDR == true)
            {
                checkBoxM2DCDR.Checked = true;
            }
            else
            {
                checkBoxM2DCDR.Checked = false;
            }
            if (settings.CheckM2DCx1R == true)
            {
                checkBoxM2DCx1R.Checked = true;
            }
            else
            {
                checkBoxM2DCx1R.Checked = false;
            }
            if (settings.CheckM2DCx2R == true)
            {
                checkBoxM2DCx2R.Checked = true;
            }
            else
            {
                checkBoxM2DCx2R.Checked = false;
            }
            if (settings.CheckM2DCx3R == true)
            {
                checkBoxM2DCx3R.Checked = true;
            }
            else
            {
                checkBoxM2DCx3R.Checked = false;
            }
            if (settings.CheckM2AsynchSpeedS == true)
            {
                checkBoxM2AsynchSpeedS.Checked = true;
            }
            else
            {
                checkBoxM2AsynchSpeedS.Checked = false;
            }
            if (settings.CheckM2AsynchPositionS == true)
            {
                checkBoxM2AsynchPositionS.Checked = true;
            }
            else
            {
                checkBoxM2AsynchPositionS.Checked = false;
            }
            if (settings.CheckM2AsynchTorqueS == true)
            {
                checkBoxM2AsynchTorqueS.Checked = true;
            }
            else
            {
                checkBoxM2AsynchTorqueS.Checked = false;
            }
            if (settings.CheckM2AsynchCurrentS == true)
            {
                checkBoxM2AsynchCurrentS.Checked = true;
            }
            else
            {
                checkBoxM2AsynchCurrentS.Checked = false;
            }
            if (settings.CheckM2AsynchVoltageS == true)
            {
                checkBoxM2AsynchVoltageS.Checked = true;
            }
            else
            {
                checkBoxM2AsynchVoltageS.Checked = false;
            }
            if (settings.CheckM2AsynchFrequencyS == true)
            {
                checkBoxM2AsynchFrequencyS.Checked = true;
            }
            else
            {
                checkBoxM2AsynchFrequencyS.Checked = false;
            }
            if (settings.CheckM2AsynchPS == true)
            {
                checkBoxM2AsynchPS.Checked = true;
            }
            else
            {
                checkBoxM2AsynchPS.Checked = false;
            }
            if (settings.CheckM2AsynchIS == true)
            {
                checkBoxM2AsynchIS.Checked = true;
            }
            else
            {
                checkBoxM2AsynchIS.Checked = false;
            }
            if (settings.CheckM2AsynchDS == true)
            {
                checkBoxM2AsynchDS.Checked = true;
            }
            else
            {
                checkBoxM2AsynchDS.Checked = false;
            }
            if (settings.CheckM2Asynchx1S == true)
            {
                checkBoxM2Asynchx1S.Checked = true;
            }
            else
            {
                checkBoxM2Asynchx1S.Checked = false;
            }
            if (settings.CheckM2Asynchx2S == true)
            {
                checkBoxM2Asynchx2S.Checked = true;
            }
            else
            {
                checkBoxM2Asynchx2S.Checked = false;
            }
            if (settings.CheckM2Asynchx3S == true)
            {
                checkBoxM2Asynchx3S.Checked = true;
            }
            else
            {
                checkBoxM2Asynchx3S.Checked = false;
            }
            if (settings.CheckM2AsynchSpeedS == true)
            {
                checkBoxM2AsynchSpeedS.Checked = true;
            }
            else
            {
                checkBoxM2AsynchSpeedR.Checked = false;
            }
            if (settings.CheckM2AsynchPositionR == true)
            {
                checkBoxM2AsynchPositionR.Checked = true;
            }
            else
            {
                checkBoxM2AsynchPositionR.Checked = false;
            }
            if (settings.CheckM2AsynchTorqueR == true)
            {
                checkBoxM2AsynchTorqueR.Checked = true;
            }
            else
            {
                checkBoxM2AsynchTorqueR.Checked = false;
            }
            if (settings.CheckM2AsynchCurrentR == true)
            {
                checkBoxM2AsynchCurrentR.Checked = true;
            }
            else
            {
                checkBoxM2AsynchCurrentR.Checked = false;
            }
            if (settings.CheckM2AsynchVoltageR == true)
            {
                checkBoxM2AsynchVoltageR.Checked = true;
            }
            else
            {
                checkBoxM2AsynchVoltageR.Checked = false;
            }
            if (settings.CheckM2AsynchFrequencyR == true)
            {
                checkBoxM2AsynchFrequencyR.Checked = true;
            }
            else
            {
                checkBoxM2AsynchFrequencyR.Checked = false;
            }
            if (settings.CheckM2AsynchPR == true)
            {
                checkBoxM2AsynchPR.Checked = true;
            }
            else
            {
                checkBoxM2AsynchPR.Checked = false;
            }
            if (settings.CheckM2AsynchIR == true)
            {
                checkBoxM2AsynchIR.Checked = true;
            }
            else
            {
                checkBoxM2AsynchIR.Checked = false;
            }
            if (settings.CheckM2AsynchDR == true)
            {
                checkBoxM2AsynchDR.Checked = true;
            }
            else
            {
                checkBoxM2AsynchDR.Checked = false;
            }
            if (settings.CheckM2Asynchx1R == true)
            {
                checkBoxM2Asynchx1R.Checked = true;
            }
            else
            {
                checkBoxM2Asynchx1R.Checked = false;
            }
            if (settings.CheckM2Asynchx2R == true)
            {
                checkBoxM2Asynchx2R.Checked = true;
            }
            else
            {
                checkBoxM2Asynchx2R.Checked = false;
            }
            if (settings.CheckM2Asynchx3R == true)
            {
                checkBoxM2Asynchx3R.Checked = true;
            }
            else
            {
                checkBoxM2Asynchx3R.Checked = false;
            }
            if (settings.CheckM2BLDCSpeedS == true)
            {
                checkBoxM2BLDCSpeedS.Checked = true;
            }
            else
            {
                checkBoxM2BLDCSpeedS.Checked = false;
            }
            if (settings.CheckM2BLDCPositionS == true)
            {
                checkBoxM2BLDCPositionS.Checked = true;
            }
            else
            {
                checkBoxM2BLDCPositionS.Checked = false;
            }
            if (settings.CheckM2BLDCTorqueS == true)
            {
                checkBoxM2BLDCTorqueS.Checked = true;
            }
            else
            {
                checkBoxM2BLDCTorqueS.Checked = false;
            }
            if (settings.CheckM2BLDCCurrentS == true)
            {
                checkBoxM2BLDCCurrentS.Checked = true;
            }
            else
            {
                checkBoxM2BLDCCurrentS.Checked = false;
            }
            if (settings.CheckM2BLDCVoltageS == true)
            {
                checkBoxM2BLDCVoltageS.Checked = true;
            }
            else
            {
                checkBoxM2BLDCVoltageS.Checked = false;
            }
            if (settings.CheckM2BLDCPS == true)
            {
                checkBoxM2BLDCPS.Checked = true;
            }
            else
            {
                checkBoxM2BLDCPS.Checked = false;
            }
            if (settings.CheckM2BLDCIS == true)
            {
                checkBoxM2BLDCIS.Checked = true;
            }
            else
            {
                checkBoxM2BLDCIS.Checked = false;
            }
            if (settings.CheckM2BLDCDS == true)
            {
                checkBoxM2BLDCDS.Checked = true;
            }
            else
            {
                checkBoxM2BLDCDS.Checked = false;
            }
            if (settings.CheckM2BLDCx1S == true)
            {
                checkBoxM2BLDCx1S.Checked = true;
            }
            else
            {
                checkBoxM2BLDCx1S.Checked = false;
            }
            if (settings.CheckM2BLDCx2S == true)
            {
                checkBoxM2BLDCx2S.Checked = true;
            }
            else
            {
                checkBoxM2BLDCx2S.Checked = false;
            }
            if (settings.CheckM2BLDCx3S == true)
            {
                checkBoxM2BLDCx3S.Checked = true;
            }
            else
            {
                checkBoxM2BLDCx3S.Checked = false;
            }
            if (settings.CheckM2BLDCSpeedS == true)
            {
                checkBoxM2BLDCSpeedS.Checked = true;
            }
            else
            {
                checkBoxM2BLDCSpeedR.Checked = false;
            }
            if (settings.CheckM2BLDCPositionR == true)
            {
                checkBoxM2BLDCPositionR.Checked = true;
            }
            else
            {
                checkBoxM2BLDCPositionR.Checked = false;
            }
            if (settings.CheckM2BLDCTorqueR == true)
            {
                checkBoxM2BLDCTorqueR.Checked = true;
            }
            else
            {
                checkBoxM2BLDCTorqueR.Checked = false;
            }
            if (settings.CheckM2BLDCCurrentR == true)
            {
                checkBoxM2BLDCCurrentR.Checked = true;
            }
            else
            {
                checkBoxM2BLDCCurrentR.Checked = false;
            }
            if (settings.CheckM2BLDCVoltageR == true)
            {
                checkBoxM2BLDCVoltageR.Checked = true;
            }
            else
            {
                checkBoxM2BLDCVoltageR.Checked = false;
            }
            if (settings.CheckM2BLDCPR == true)
            {
                checkBoxM2BLDCPR.Checked = true;
            }
            else
            {
                checkBoxM2BLDCPR.Checked = false;
            }
            if (settings.CheckM2BLDCIR == true)
            {
                checkBoxM2BLDCIR.Checked = true;
            }
            else
            {
                checkBoxM2BLDCIR.Checked = false;
            }
            if (settings.CheckM2BLDCDR == true)
            {
                checkBoxM2BLDCDR.Checked = true;
            }
            else
            {
                checkBoxM2BLDCDR.Checked = false;
            }
            if (settings.CheckM2BLDCx1R == true)
            {
                checkBoxM2BLDCx1R.Checked = true;
            }
            else
            {
                checkBoxM2BLDCx1R.Checked = false;
            }
            if (settings.CheckM2BLDCx2R == true)
            {
                checkBoxM2BLDCx2R.Checked = true;
            }
            else
            {
                checkBoxM2BLDCx2R.Checked = false;
            }
            if (settings.CheckM2BLDCx3R == true)
            {
                checkBoxM2BLDCx3R.Checked = true;
            }
            else
            {
                checkBoxM2BLDCx3R.Checked = false;
            }
            if (settings.CheckM2PMSMSpeedS == true)
            {
                checkBoxM2PMSMSpeedS.Checked = true;
            }
            else
            {
                checkBoxM2PMSMSpeedS.Checked = false;
            }
            if (settings.CheckM2PMSMPositionS == true)
            {
                checkBoxM2PMSMPositionS.Checked = true;
            }
            else
            {
                checkBoxM2PMSMPositionS.Checked = false;
            }
            if (settings.CheckM2PMSMTorqueS == true)
            {
                checkBoxM2PMSMTorqueS.Checked = true;
            }
            else
            {
                checkBoxM2PMSMTorqueS.Checked = false;
            }
            if (settings.CheckM2PMSMCurrentS == true)
            {
                checkBoxM2PMSMCurrentS.Checked = true;
            }
            else
            {
                checkBoxM2PMSMCurrentS.Checked = false;
            }
            if (settings.CheckM2PMSMVoltageS == true)
            {
                checkBoxM2PMSMVoltageS.Checked = true;
            }
            else
            {
                checkBoxM2PMSMVoltageS.Checked = false;
            }
            if (settings.CheckM2PMSMFrequencyS == true)
            {
                checkBoxM2PMSMFrequencyS.Checked = true;
            }
            else
            {
                checkBoxM2PMSMFrequencyS.Checked = false;
            }
            if (settings.CheckM2PMSMPS == true)
            {
                checkBoxM2PMSMPS.Checked = true;
            }
            else
            {
                checkBoxM2PMSMPS.Checked = false;
            }
            if (settings.CheckM2PMSMIS == true)
            {
                checkBoxM2PMSMIS.Checked = true;
            }
            else
            {
                checkBoxM2PMSMIS.Checked = false;
            }
            if (settings.CheckM2PMSMDS == true)
            {
                checkBoxM2PMSMDS.Checked = true;
            }
            else
            {
                checkBoxM2PMSMDS.Checked = false;
            }
            if (settings.CheckM2PMSMx1S == true)
            {
                checkBoxM2PMSMx1S.Checked = true;
            }
            else
            {
                checkBoxM2PMSMx1S.Checked = false;
            }
            if (settings.CheckM2PMSMx2S == true)
            {
                checkBoxM2PMSMx2S.Checked = true;
            }
            else
            {
                checkBoxM2PMSMx2S.Checked = false;
            }
            if (settings.CheckM2PMSMx3S == true)
            {
                checkBoxM2PMSMx3S.Checked = true;
            }
            else
            {
                checkBoxM2PMSMx3S.Checked = false;
            }
            if (settings.CheckM2PMSMSpeedS == true)
            {
                checkBoxM2PMSMSpeedS.Checked = true;
            }
            else
            {
                checkBoxM2PMSMSpeedR.Checked = false;
            }
            if (settings.CheckM2PMSMPositionR == true)
            {
                checkBoxM2PMSMPositionR.Checked = true;
            }
            else
            {
                checkBoxM2PMSMPositionR.Checked = false;
            }
            if (settings.CheckM2PMSMTorqueR == true)
            {
                checkBoxM2PMSMTorqueR.Checked = true;
            }
            else
            {
                checkBoxM2PMSMTorqueR.Checked = false;
            }
            if (settings.CheckM2PMSMCurrentR == true)
            {
                checkBoxM2PMSMCurrentR.Checked = true;
            }
            else
            {
                checkBoxM2PMSMCurrentR.Checked = false;
            }
            if (settings.CheckM2PMSMVoltageR == true)
            {
                checkBoxM2PMSMVoltageR.Checked = true;
            }
            else
            {
                checkBoxM2PMSMVoltageR.Checked = false;
            }
            if (settings.CheckM2PMSMFrequencyR == true)
            {
                checkBoxM2PMSMFrequencyR.Checked = true;
            }
            else
            {
                checkBoxM2PMSMFrequencyR.Checked = false;
            }
            if (settings.CheckM2PMSMPR == true)
            {
                checkBoxM2PMSMPR.Checked = true;
            }
            else
            {
                checkBoxM2PMSMPR.Checked = false;
            }
            if (settings.CheckM2PMSMIR == true)
            {
                checkBoxM2PMSMIR.Checked = true;
            }
            else
            {
                checkBoxM2PMSMIR.Checked = false;
            }
            if (settings.CheckM2PMSMDR == true)
            {
                checkBoxM2PMSMDR.Checked = true;
            }
            else
            {
                checkBoxM2PMSMDR.Checked = false;
            }
            if (settings.CheckM2PMSMx1R == true)
            {
                checkBoxM2PMSMx1R.Checked = true;
            }
            else
            {
                checkBoxM2PMSMx1R.Checked = false;
            }
            if (settings.CheckM2PMSMx2R == true)
            {
                checkBoxM2PMSMx2R.Checked = true;
            }
            else
            {
                checkBoxM2PMSMx2R.Checked = false;
            }
            if (settings.CheckM2PMSMx3R == true)
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
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                SaveData();

                using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\config_default.xml", FileMode.Create, FileAccess.Write))
                {
                    serializer.Serialize(fs, settings);
                    MessageBox.Show("Created");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ComboBoxSaveConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\config"+comboBoxSaveConfig.Text+".xml", FileMode.Open, FileAccess.Read))
                {
                    settings = serializer.Deserialize(fs) as Settings;

                }
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnConfigSave_Click(object sender, EventArgs e)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                SaveData();

                using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\config" + comboBoxSaveConfig.Text + ".xml", FileMode.Create, FileAccess.Write))
                {
                    serializer.Serialize(fs, settings);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
