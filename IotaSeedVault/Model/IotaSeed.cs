using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IotaSeedVault.Model
{
    public class IotaSeed
    {        
        public string Seed { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }

        public IotaSeed()
        {

        }

        public IotaSeed(string seed, string name, string remark)
        {            
            Seed = seed;
            Name = name;
            Remark = remark;
        }

        public static IotaSeed generateIotaSeed(string name)
        {
            IotaSeed iS = new IotaSeed()
            {
                Name = name
            };

            iS.Seed = generateRandomSeed();

            return iS;
        }

        private static string generateRandomSeed()
        {

            char[] chars = new char[27];
            chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ9".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[81];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(81);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
