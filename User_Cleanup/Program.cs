using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Security.AccessControl;

namespace User_Cleanup
{
    static class Program
    {
        // Paths to all relevent items need to be determined
        // before anything can be deleted.
        // Look for copycat profles. e.g. "[username].[DOMAIN]"

        
        public static string profilesPath       = @"c:\Users\";
        public static string root               = "HKEY_LOCAL_MACHINE";
        public static string keyName            = @"ProfileList";
        public static string profilesRegPath    = @"Software\Microsoft\Windows NT\CurrentVersion\ProfileList";
        public static List<Profile> profileList;
        public static List<string> indexItems   = new List<string>();
 
        [STAThread]
        static void Main()
        {

            profileList = FileSysTools.RetrieveProfiles(profilesRegPath);
            foreach(Profile prof in profileList)
            {
                indexItems.Add(prof.path);
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ProfileSelect());
        }


        static void printf(string str)
        {
            // redirect stuff
        }

    }
}
