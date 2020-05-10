using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modbus.Utility;

namespace Praca_Inżynierska
{
    class ModbusUtilityNew
    {
        public static ushort[] ConvertfloatArrayToUshortArray(float[] pValues)
        {
            List<byte> byteList = new List<byte>();
            foreach (float item in pValues)
            {
                byte[] byteArray = BitConverter.GetBytes(item);
                Array.Reverse(byteArray, 0, byteArray.Length);
                byteList.AddRange(byteArray);
            }
            return ModbusUtility.NetworkBytesToHostUInt16(byteList.ToArray());
        }

        public static float[] ConvertUshortArrayToFloatArrat(ushort[] pValues)
        {
            List<float> floatList = new List<float>();
            for (int i = 0; i < pValues.Length; )
            {
                float temp = ModbusUtility.GetSingle(pValues[i++], pValues[i++]);
                floatList.Add(temp);
            }
            return floatList.ToArray();
        }
    }
}
