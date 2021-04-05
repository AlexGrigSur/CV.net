using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMAC
{
    class Program
    {
        static string byteArrayToHexString(byte[] byteArray) => String.Join("", byteArray.Select(x => Convert.ToString(x, 16).PadLeft(2, '0')));
        static void Main(string[] args)
        {
            string accessKey = "";
            string refreshKey = System.Guid.NewGuid().ToString();
            string messageStr = "{ good morning }";
            string fakeMessageStr = "{ goOd morning }";
            //Convert.FromBase64String("key");
            HMAC_SHA3_256 hmac = new HMAC_SHA3_256();
            byte[] sign = hmac.Calc(Encoding.ASCII.GetBytes(messageStr));
            messageStr += '.' + byteArrayToHexString(sign);
            fakeMessageStr += '.' + byteArrayToHexString(sign);
            Console.WriteLine($"Подпись: {messageStr}");
            Console.WriteLine($"Валидность подписи: {hmac.Check(messageStr)}");
            Console.WriteLine("____________________________________________");
            Console.WriteLine($"Fake Подпись: {fakeMessageStr}");
            Console.WriteLine($"Валидность подписи: {hmac.Check(fakeMessageStr)}");

            Console.ReadKey();
        }
    }
}
