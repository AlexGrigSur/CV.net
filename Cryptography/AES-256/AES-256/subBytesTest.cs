using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES_256
{
    class subBytesTest
    {
        private byte[] subBytesTransformTable = new byte[256];
        private byte[] invSubBytesTransformTable = new byte[256];

        private byte[] mult2 = new byte[256];
        private byte[] mult9 = new byte[256];
        private byte[] mult11 = new byte[256];
        private byte[] mult13 = new byte[256];
        private byte[] mult14 = new byte[256];

        public subBytesTest()
        {
            CalcMults();
            CalcSubBytesTransTable();

            for (int i = 0; i < 256; ++i)
            {
                if (i % 16 == 0) Console.WriteLine();
                Console.Write("0x" + Convert.ToString(mult2[i], 16).PadLeft(2, '0') + ",");
            }
            Console.WriteLine();
            for (int i = 0; i < 256; ++i)
            {
                if (i % 16 == 0) Console.WriteLine();
                Console.Write("0x" + Convert.ToString(mult9[i], 16).PadLeft(2, '0') + ",");
            }
            Console.WriteLine();
            for (int i = 0; i < 256; ++i)
            {
                if (i % 16 == 0) Console.WriteLine();
                Console.Write("0x" + Convert.ToString(mult11[i], 16).PadLeft(2, '0') + ",");
            }
            Console.WriteLine();
            for (int i = 0; i < 256; ++i)
            {
                if (i % 16 == 0) Console.WriteLine();
                Console.Write("0x" + Convert.ToString(mult13[i], 16).PadLeft(2, '0') + ",");
            }
            Console.WriteLine();
            for (int i = 0; i < 256; ++i)
            {
                if (i % 16 == 0) Console.WriteLine();
                Console.Write("0x" + Convert.ToString(mult14[i], 16).PadLeft(2, '0') + ",");
            }
        }

        private void CalcMults()
        {
            byte mult(byte value)
            {
                return (value >> 7 == 0) ? (byte)(value << 1) : (byte)(value << 1 ^ 283);
            }
            for (byte i = 0; i < 255; ++i)
            {
                mult2[i] = (byte)mult(i);
                mult9[i] = (byte)(mult(mult(mult(i))) ^ i);
                mult11[i] = (byte)(mult((byte)(mult(mult(i)) ^ i)) ^ i);
                mult13[i] = (byte)(mult(mult((byte)(mult(i) ^ i))) ^ i);
                mult14[i] = mult((byte)(mult((byte)(mult(i) ^ i)) ^ i));
            }
            mult2[255] = (byte)mult(255);
            mult9[255] = (byte)(mult(mult(mult(255))) ^ 255);
            mult11[255] = (byte)(mult((byte)(mult(mult(255)) ^ 255)) ^ 255);
            mult13[255] = (byte)(mult(mult((byte)(mult(255) ^ 255))) ^ 255);
            mult14[255] = mult((byte)(mult((byte)(mult(255) ^ 255)) ^ 255));
        }
        private void CalcSubBytesTransTable() // done
        {
            void CalcGaloisInverted(ref byte?[] GaloisInvertedTable)//Dictionary<byte, byte> GaloisInvertedTable) // done
            {
                // Gx - неприводимый полином степени m ==> Sx = Gx
                // Ax - полиномиальное представление нашего числа ==> Rx = Ax
                byte GaloisInverted(byte baseValue, int irreduciblePoly = 283) // done [4]
                {
                    int deg(int polynom)
                    {
                        for (byte i = 8; i >= 1; --i)
                            if ((polynom >> i & 1) == 1)
                                return i;
                        return 0;
                    }
                    if (baseValue == 0) return 0;
                    int Sx = irreduciblePoly;// x^8 + x^4 + x^3 + x + 1  ==> 100011011
                    int Vx = 0;
                    int Rx = baseValue;
                    int Ux = 1;
                    int delta;
                    int temp;

                    while (Rx >= 2)
                    {
                        delta = deg(Sx) - deg(Rx);
                        if (delta < 0)
                        {
                            temp = Sx;
                            Sx = Rx;
                            Rx = temp;

                            temp = Vx;
                            Vx = Ux;
                            Ux = temp;

                            delta *= -1;
                        }
                        Sx ^= (Rx << delta);
                        Vx ^= (Ux << delta);
                    }
                    return Convert.ToByte(Ux);
                }
                byte result;
                GaloisInvertedTable[0] = 0;
                GaloisInvertedTable[1] = 1;
                for (int i = 2; i < 256; ++i)
                {
                    if (GaloisInvertedTable[i].HasValue)
                        continue;
                    result = GaloisInverted((byte)i);
                    GaloisInvertedTable[i] = result;
                    GaloisInvertedTable[result] = (byte)i;
                }
            }
            byte?[] galoisInvertedTable = new byte?[256];
            CalcGaloisInverted(ref galoisInvertedTable);
            // Ax ==> a(x) = x^4 + x^3 + x^2 + x + 1
            byte[] Ax = new byte[] { 0b10001111,
                                     0b11000111,
                                     0b11100011,
                                     0b11110001,
                                     0b11111000,
                                     0b01111100,
                                     0b00111110,
                                     0b00011111 };
            // Bx ==> b(x) = x^6 + x^5 + x + 1 ==> 63 in base16 ==> 99 in base10
            byte Bx = 0b01100011;
            byte X;
            byte sum;
            for (int i = 0; i <= 255; ++i)
            {
                X = galoisInvertedTable[i].Value;
                byte result = 0;
                byte multiply = 1;
                for (int j = 0; j < 8; ++j)
                {
                    sum = 0;
                    for (int z = 0; z < 8; ++z)
                        sum ^= (byte)(Ax[j] >> (7 - z) & 1 & (X >> z & 1));
                    result += (byte)((sum ^ (Bx >> j & 1)) * multiply);
                    multiply <<= 1;
                }
                subBytesTransformTable[i] = result;
                invSubBytesTransformTable[result] = (byte)i;
            }
        }
    }
}
