using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace User_Cleanup
{
    public class Profile
    {
        public string name;
        public string path { get; set; }
        public RegistryKey registryKey { get; set; }
        public string registryKeyName { get; set; }


        public Profile()
        {


        }

        public string ToString()
        {
            return name;
        }

        public string ProfileName
        {
            get { return name; }
        }
    }
}
