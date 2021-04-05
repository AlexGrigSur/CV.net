using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES_256
{
    class Program
    {
        static void TEST()
        {
            ////string PLAINTEXT = "00112233445566778899aabbccddeeff";
            //byte[] PLAINTEXT = new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff };
            byte[] KEY = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 0x1e, 0x1f };
            byte[] PLAINTEXT = new byte[] { 0x8e, 0xa2, 0xb7, 0xca, 0x51, 0x67, 0x45, 0xbf, 0xea, 0xfc, 0x49, 0x90, 0x4b, 0x49, 0x60, 0x89 };
            //byte[] result = new AES().Decrypt(PLAINTEXT, KEY);
            ////byte[] KEY = new byte[] { 0x60, 0x3d, 0xeb, 0x10, 0x15, 0xca, 0x71, 0xbe, 0x2b, 0x73, 0xae, 0xf0, 0x85, 0x7d, 0x77, 0x81, 0x1f, 0x35, 0x2c, 0x07, 0x3b, 0x61, 0x08, 0xd7, 0x2d, 0x98, 0x10, 0xa3, 0x09, 0x14, 0xdf, 0xf4 };
        }

        static void PerformanceTest()
        {
            Console.WriteLine("____________________________________________");
            Console.WriteLine("AES-256 PERFORMANCE TEST SYSTEM");
            Console.WriteLine("____________________________________________");
            Stopwatch sw = new Stopwatch();
            StringBuilder largeStringB = new StringBuilder();
            int stringSize = 1024 * 1024 * 128;
            largeStringB.Append('A', stringSize);
            
            StringBuilder largeStringKeyB = new StringBuilder();
            largeStringKeyB.Append('A', 512);

            Console.WriteLine($"Base text weight: {stringSize * sizeof(char) / 1024 / 1024 } MB");
            Console.WriteLine("Beginning of Encryption test");
            Console.WriteLine("___________________________");
            long sumTime = 0;
            int rounds = 1;
            AES aes;
            for (int i = 0; i < rounds; ++i)
            {
                sw.Reset();
                sw.Start();
                aes = new AES();
                largeStringB = new StringBuilder(aes.Encrypt(largeStringB.ToString(), largeStringKeyB.ToString()));//Encrypt(largeString, key);
                sw.Stop();
                sumTime += sw.ElapsedMilliseconds;
                Console.WriteLine($"Length: {largeStringB.Length}");
                //Console.WriteLine($"#{i + 1}");
                //Console.WriteLine(sw.ElapsedMilliseconds);
                Console.WriteLine("___________________________");
            }
            Console.WriteLine($" Encrypt speed: {(stringSize * sizeof(char) / 1024 / 1024 * 8) / (sumTime / rounds / 1000)} Mbit/sec (avg {sumTime / rounds} ms )");
            GC.Collect();
            sumTime = 0;
            Console.WriteLine("Beginning of Decryption test");
            Console.WriteLine("___________________________");
            for (int i = 0; i < rounds; ++i)
            {
                sw.Reset();
                sw.Start();
                aes = new AES();
                largeStringB = new StringBuilder(aes.Decrypt(largeStringB.ToString(), largeStringKeyB.ToString()));
                sw.Stop();

                sumTime += sw.ElapsedMilliseconds;
                //Console.WriteLine(sw.ElapsedMilliseconds);
                Console.WriteLine("___________________________");
            }
            Console.WriteLine(largeStringB.ToString());
           Console.WriteLine($" Decrypt speed: {(stringSize * sizeof(char) / 1024 / 1024 * 8) / (sumTime / rounds / 1000)} Mbit/sec (avg {sumTime / rounds} ms )");
        }

        static void FullTest()
        {
            Console.WriteLine("____________________________________________");
            Console.WriteLine("AES-256 TEST SYSTEM");
            Console.WriteLine("____________________________________________");
            Console.WriteLine("Введите текст, который требуется зашифровать");
            string textToEncrypt = Console.ReadLine();
            Console.WriteLine("Введите ключ шифрования");
            string Key = Console.ReadLine();
            Console.WriteLine("____________________________________________");
            Console.WriteLine("Зашифрованный текст:");
            AES aes = new AES();
            string result = aes.Encrypt(textToEncrypt, Key);
            Console.WriteLine(result);
            Console.WriteLine("____________________________________________");
            Console.WriteLine("Расшифрованный текст на базе зашифрованного");
            Console.WriteLine(aes.Decrypt(result, Key));
        }
        static void DecryptTest()
        {
            Console.WriteLine("____________________________________________");
            Console.WriteLine("AES-256 DECRYPTION TEST SYSTEM");
            Console.WriteLine("____________________________________________");
            Console.WriteLine("Введите зашифрованный текст");
            string textToDecrypt = Console.ReadLine();
            Console.WriteLine("Введите ключ шифрования");
            string Key = Console.ReadLine();
            Console.WriteLine(new AES().Decrypt(textToDecrypt, Key));
        }
        static void Main(string[] args)
        {
            //new subBytesTest();
            //PerformanceTest();
            FullTest();
            //DecryptTest();

            Console.ReadKey();
        }
    }
}
