using IotaSeedVault.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IotaSeedVault.Model
{
    public class IotaSeed
    {
        private SecureString _Seed;

        public string Seed
        {
            get
            {
                return SecureStringController.ConvertToUNSecureString(_Seed);
            }
            set
            {
                _Seed = SecureStringController.ConvertToSecureString(value);
            }
        }
    
        public string Name { get; set; }
        public string Remark { get; set; }

        public IotaSeed()
        {

        }

        public IotaSeed(SecureString seed, string name, string remark)
        {
            _Seed = seed;
            Name = name;
            Remark = remark;            
        }

        public static IotaSeed GenerateIotaSeed(string name)
        {
            IotaSeed iS = new IotaSeed()
            {
                Name = name
            };

            iS._Seed = GenerateRandomSeed();

            return iS;
        }

        private static SecureString GenerateRandomSeed()
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
            return SecureStringController.ConvertToSecureString(result.ToString());
        }
    }
}
