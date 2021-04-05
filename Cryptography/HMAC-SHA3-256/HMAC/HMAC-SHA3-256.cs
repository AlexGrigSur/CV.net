using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMAC
{
    class HMAC_SHA3_256
    {
        private const int BLOCKSIZE = 128;
        private SHA_3 sha = new SHA_3();
        byte[] key = Encoding.ASCII.GetBytes("TestKey");
        private byte[] hexStringToByteArray(string hexKey) => Enumerable.Range(0, hexKey.Length / 2).Select(x => Convert.ToByte(hexKey.Substring(x * 2, 2), 16)).ToArray();
        private string byteArrayToHexString(byte[] byteArray) => String.Join("", byteArray.Select(x => Convert.ToString(x, 16).PadLeft(2, '0')));
        private byte[] XOR(byte[] arr1, byte[] arr2)
        {
            int min = (arr1.Length <= arr2.Length) ? arr1.Length : arr2.Length;
            for (int i = 0; i < min; ++i)
                arr1[i] ^= arr2[i];
            return arr1;
        }
        private byte[] ConcatArrays(byte[] arr1, byte[] arr2)
        {
            byte[] result = new byte[arr1.Length + arr2.Length];
            arr1.CopyTo(result, 0);
            arr2.CopyTo(result, arr1.Length);
            return result;
        }
        private void PadKey(ref byte[] key)
        {
            byte[] paddedKey = new byte[BLOCKSIZE];
            key.CopyTo(paddedKey, 0);
            key = paddedKey;
        }
        public byte[] Calc(byte[] message)
        {
            if (key.Length > BLOCKSIZE)
            {
                key = sha.GetHashSHA3(key);
                PadKey(ref key);
            }
            byte[] hexVal36 = new byte[BLOCKSIZE];
            byte[] hexVal5c = new byte[BLOCKSIZE];
            for (int i = 0; i < BLOCKSIZE; ++i)
            {
                hexVal36[i] = 0x36;
                hexVal5c[i] = 0x5c;
            }
            return sha.GetHashSHA3(ConcatArrays(XOR(hexVal5c, key), sha.GetHashSHA3(ConcatArrays(XOR(hexVal36, key), message))));
        }
        public bool Check(string message)
        {
            byte[] result = Calc(Encoding.ASCII.GetBytes(message.Split('.')[0]));
            Console.WriteLine($"Вычисленная подпись для сообщения {message.Split('.')[0]}: {byteArrayToHexString(result)}");
            return (Enumerable.SequenceEqual(result, hexStringToByteArray(message.Split('.')[1])));
        }
    }
}