using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHWSaveEditor
{
    public static class Checksum
    {
        //needs a cleanup and better buffer management
        static readonly byte[] initArr = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF, 0xFE, 0xDC, 0xBA, 0x98, 0x76, 0x54, 0x32, 0x10, 0xF0, 0xE1, 0xD2, 0xC3 };
        static readonly byte[] staticArr = { 0x00, 0xFF, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x00 };

        public static byte[] Generate(MemoryStream data, uint size)
        {

            byte[] _data = new byte[64];
            byte[] newArray = new byte[320];
            byte[] temp;
            byte[] checkSum = new byte[initArr.Length];
            Array.Copy(initArr, checkSum, initArr.Length);
            uint cycles = size / 64;
            while (cycles > 0)
            {
                data.Read(_data, 0, _data.Length);
                temp = ShuffleStep1(_data, 0);
                Array.Copy(temp, 0, _data, 0, temp.Length);
                temp = ShuffleStep1(_data, 16);
                Array.Copy(temp, 0, _data, 16, temp.Length);
                temp = ShuffleStep1(_data, 32);
                Array.Copy(temp, 0, _data, 32, temp.Length);
                temp = ShuffleStep1(_data, 48);
                Array.Copy(temp, 0, _data, 48, temp.Length);
                Array.Clear(newArray, 0, newArray.Length);
                Array.Copy(_data, newArray, _data.Length);
                ShuffleStep2(newArray);
                ShuffleStep3(newArray, checkSum);
                cycles--;
                //MainForm.progressBar.Value = (int)((cycles * 100) / data.Length);
            }
            uint sizeCheck = size * 8;
            uint sizeCheck1;
            uint sizeCheck2;
            unsafe
            {
                sizeCheck1 = 0; // should be always 0
                sizeCheck2 = (sizeCheck >> 24) + ((sizeCheck >> 8) & 0xFF00) + (((sizeCheck << 16) + (sizeCheck & 0xFF00)) << 8);
            }
            _data = new byte[64];
            data.Read(_data, 0, _data.Length);
            _data[32] = (byte)(_data[32] | 0x80);
            Array.Copy(BitConverter.GetBytes(sizeCheck1), 0, _data, 56, 4);
            Array.Copy(BitConverter.GetBytes(sizeCheck2), 0, _data, 60, 4);
            temp = ShuffleStep1(_data, 0);
            Array.Copy(temp, 0, _data, 0, temp.Length);
            temp = ShuffleStep1(_data, 16);
            Array.Copy(temp, 0, _data, 16, temp.Length);
            temp = ShuffleStep1(_data, 32);
            Array.Copy(temp, 0, _data, 32, temp.Length);
            temp = ShuffleStep1(_data, 48);
            Array.Copy(temp, 0, _data, 48, temp.Length);
            Array.Clear(newArray, 0, newArray.Length);
            Array.Copy(_data, newArray, _data.Length);
            ShuffleStep2(newArray);
            ShuffleStep3(newArray, checkSum);
            return checkSum;
        }

        static byte[] ShuffleStep1(byte[] data, int offset)
        {
            byte[] buff1 = new byte[16];
            byte[] buff2 = new byte[16];
            byte[] buff3 = new byte[16];
            Array.Copy(data, offset, buff1, 0, buff1.Length);
            Array.Copy(staticArr, 0, buff2, 0, buff2.Length);
            PAND(buff1, buff2);
            Array.Copy(data, offset, buff3, 0, buff3.Length);
            PSLLD(buff3, 0x10);
            PADDD(buff1, buff3);
            Array.Copy(data, offset, buff3, 0, buff3.Length);
            Array.Copy(data, offset, buff2, 0, buff2.Length);
            PSLLD(buff1, 0x8);
            PSRLD(buff3, 0x8);
            PSRLD(buff2, 0x18);
            PAND(buff3, staticArr);
            PADDD(buff1, buff3);
            PADDD(buff1, buff2);
            return buff1;
        }

        static void ShuffleStep2(byte[] data)
        {
            int i = 0x40;
            int offset = 0;
            while (i > 0)
            {
                uint temp = BitConverter.ToUInt32(data, 52 + offset);
                temp = temp ^ BitConverter.ToUInt32(data, 32 + offset);
                temp = temp ^ BitConverter.ToUInt32(data, 0 + offset);
                temp = temp ^ BitConverter.ToUInt32(data, 8 + offset);
                temp = ((temp << 1) | (temp >> 31));
                offset += 4;
                Array.Copy(BitConverter.GetBytes(temp), 0, data, 60 + offset, 4);
                i--;
            }
        }

        static void ShuffleStep3(byte[] data, byte[] data2)
        {
            uint A = BitConverter.ToUInt32(data2, 0);
            uint A2 = BitConverter.ToUInt32(data2, 0);
            uint B = BitConverter.ToUInt32(data2, 4);
            uint C = BitConverter.ToUInt32(data2, 8);
            uint D = BitConverter.ToUInt32(data2, 12);
            uint E = BitConverter.ToUInt32(data2, 16);
            uint _temp1;
            uint _temp2;
            uint temp4 = 0;
            int i = 0;
            while (i < 80)
            {
                if (i > 19)
                {
                    if (i <= 39 || i > 59)
                        _temp1 = B ^ C ^ D;
                    else
                        _temp1 = B & C | D & (B | C);
                }
                else
                {
                    _temp1 = (B & C) | (D & (~B));
                }
                if (i > 19)
                {
                    if (i > 39)
                    {
                        _temp2 = 0xC1CF3B18;
                        if (i <= 59)
                            _temp2 = 0x84B64612;
                    }
                    else
                    {
                        _temp2 = 0x6574116F;
                    }
                }
                else
                {
                    _temp2 = 0x512F8357;
                }

                unsafe
                {
                    uint temp3 = BitConverter.ToUInt32(data, i * 4) + ((A2 << 5) | (A2 >> 27));
                    i++;
                    temp4 = E + _temp1 + temp3 + (_temp2 ^ 0xBADFACE);
                    E = D;
                    D = C;
                    C = ((B >> 2) | (B << 30));
                    B = A2;
                    A2 = temp4;
                }
            }
            byte[] temp = BitConverter.GetBytes(BitConverter.ToUInt32(data2, 12) + D);
            Array.Copy(temp, 0, data2, 12, 4);
            temp = BitConverter.GetBytes(BitConverter.ToUInt32(data2, 0) + A2);
            Array.Copy(temp, 0, data2, 0, 4);
            temp = BitConverter.GetBytes(BitConverter.ToUInt32(data2, 4) + B);
            Array.Copy(temp, 0, data2, 4, 4);
            temp = BitConverter.GetBytes(BitConverter.ToUInt32(data2, 8) + C);
            Array.Copy(temp, 0, data2, 8, 4);
            temp = BitConverter.GetBytes(BitConverter.ToUInt32(data2, 16) + E);
            Array.Copy(temp, 0, data2, 16, 4);
        }

        #region Helper
        static void PSLLD(byte[] dat, int n)
        {
            uint a = BitConverter.ToUInt32(dat, 0) << n;
            Array.Copy(BitConverter.GetBytes(a), 0, dat, 0, 4);
            uint b = BitConverter.ToUInt32(dat, 4) << n;
            Array.Copy(BitConverter.GetBytes(b), 0, dat, 4, 4);
            uint c = BitConverter.ToUInt32(dat, 8) << n;
            Array.Copy(BitConverter.GetBytes(c), 0, dat, 8, 4);
            uint d = BitConverter.ToUInt32(dat, 12) << n;
            Array.Copy(BitConverter.GetBytes(d), 0, dat, 12, 4);
        }

        static void PSRLD(byte[] dat, int n)
        {
            uint a = BitConverter.ToUInt32(dat, 0) >> n;
            Array.Copy(BitConverter.GetBytes(a), 0, dat, 0, 4);
            uint b = BitConverter.ToUInt32(dat, 4) >> n;
            Array.Copy(BitConverter.GetBytes(b), 0, dat, 4, 4);
            uint c = BitConverter.ToUInt32(dat, 8) >> n;
            Array.Copy(BitConverter.GetBytes(c), 0, dat, 8, 4);
            uint d = BitConverter.ToUInt32(dat, 12) >> n;
            Array.Copy(BitConverter.GetBytes(d), 0, dat, 12, 4);
        }

        static void PADDD(byte[] data1, byte[] data2)
        {
            unsafe
            {
                uint a = BitConverter.ToUInt32(data1, 0) + BitConverter.ToUInt32(data2, 0);
                Array.Copy(BitConverter.GetBytes(a), 0, data1, 0, 4);
                uint b = BitConverter.ToUInt32(data1, 4) + BitConverter.ToUInt32(data2, 4);
                Array.Copy(BitConverter.GetBytes(b), 0, data1, 4, 4);
                uint c = BitConverter.ToUInt32(data1, 8) + BitConverter.ToUInt32(data2, 8);
                Array.Copy(BitConverter.GetBytes(c), 0, data1, 8, 4);
                uint d = BitConverter.ToUInt32(data1, 12) + BitConverter.ToUInt32(data2, 12);
                Array.Copy(BitConverter.GetBytes(d), 0, data1, 12, 4);
            }
        }


        static void PAND(byte[] arr1, byte[] arr2)
        {
            for (int i = 0; i < arr1.Length; i++)
            {
                arr1[i] = (byte)(arr1[i] & arr2[i]);
            }
        }
        #endregion
    }
}
