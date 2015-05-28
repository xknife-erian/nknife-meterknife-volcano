﻿using System.Text;
using NKnife.Utility;
using NPOI.HPSF;

namespace MeterKnife.Common.DataModels
{
    public partial class CareTalking
    {
        private static CareTalking _temp;

        public static CareTalking BuildCareSaying(int gpib, string scpi, bool isReturn = true)
        {
            var careTalking = new CareTalking
            {
                MainCommand = isReturn ? (byte) 0xAA : (byte) 0xAB, 
                SubCommand = 0x00, 
                Scpi = scpi, 
                ScpiBytes = Encoding.ASCII.GetBytes(scpi), 
                GpibAddress = (byte) gpib
            };
            return careTalking;
        }

        // ReSharper disable once InconsistentNaming
        public static CareTalking IDN(int gpib)
        {
            return BuildCareSaying(gpib, "*IDN?");
        }

        // ReSharper disable once InconsistentNaming
        public static CareTalking READ(int gpib)
        {
            return BuildCareSaying(gpib, "READ?");
        }

        // ReSharper disable once InconsistentNaming
        public static CareTalking TEMP()
        {
            if (_temp == null)
            {
                _temp = BuildCareSaying(0, string.Empty);
                _temp.MainCommand = 0xAE;
            }
            return _temp;
        }

        public static byte[] CareGetter(byte subcommand = 0xD1)
        {
            return new byte[] { 0x08, 0x00, 0x02, 0xA0, subcommand };
        }

        public static byte[] CareSetter(byte subcommand, byte[] content)
        {
            var head = new byte[] { 0x08, 0x00, 0x02, 0xB0, subcommand};
            var newbs = UtilityCollection.MergerArray(head, content);
            newbs[2] = (byte)(newbs.Length - 3);
            return newbs;
        }

        public static byte[] CareReset()
        {
            return new byte[] { 0x08, 0x00, 0x02, 0xB1, 0x00 };
        }
    }
}