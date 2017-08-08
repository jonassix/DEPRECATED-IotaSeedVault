using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotaSeedVault.Model
{
    public class IotaSeed
    {
        public int Id { get; set; }
        public string Seed { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }

        public IotaSeed()
        {

        }

        public IotaSeed(int id, string seed, string name, string remark)
        {
            Id = id;
            Seed = seed;
            Name = name;
            Remark = remark;
        }

        public static IotaSeed generateIotaSeed()
        {
            IotaSeed iS = new IotaSeed();

            iS.Seed = generateRandomSeed();

            return iS;
        }

        private static string generateRandomSeed()
        {
            return  "seed";
        }
    }
}
