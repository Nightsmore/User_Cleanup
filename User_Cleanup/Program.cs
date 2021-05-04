using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace User_Cleanup
{
    static class Program
    {
        // Paths to all relevent items need to be determined
        // before anything can be deleted.
        // Look for copycat profles. e.g. "[username].[DOMAIN]"

        public static string profilesPath = @"c:\Users\";
        public static string root = "HKEY_LOCAL_MACHINE";
        public static string keyName = @"ProfileList";
        public static string profilesRegPath = @"Software\Microsoft\Windows NT\CurrentVersion\ProfileList";
        public static List<Profile> profileList; 
        // = @"Software\Microsoft\Windows NT\CurrentVersion\ProfileList";

        public static string[] indexItems = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            indexItems = RetrieveProfiles().ToArray();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ProfileSelect());
        }

        private static List<string> RetrieveProfiles()
        {
            List<string> results = new List<string>();



            string[] keys = Registry.Users.GetSubKeyNames();
            List<string> names = new List<string>();

            foreach (var item in keys)
            {
                Profile profile = new Profile();
                try
                {
                    // retrieve the path
                    profile.path = Registry.LocalMachine.OpenSubKey(profilesRegPath).OpenSubKey(item).GetValue("ProfileImagePath").ToString();
                    // retrieve the key
                    profile.registryKey = Registry.LocalMachine.OpenSubKey(profilesRegPath).OpenSubKey(item);

                    object testval = Registry.LocalMachine.OpenSubKey(profilesRegPath).OpenSubKey(item).GetValue("ProfileImagePath");
     
                    if (testval != null)
                    {
                        names.Add(testval.ToString());
                    }
                } catch (System.NullReferenceException e)
                {

                }


            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (var item in names)
            {
                sb.Append($"{item}, ");
            }

            //MessageBox.Show(sb.ToString());

            return names;
        }

        static void DeleteProfile(Profile profile)
        {
            Registry.LocalMachine.DeleteSubKey(profile.registryKey.Name);
        }


    }

    class Profile
    { 

        public string path { get; set; }
        public RegistryKey registryKey { get; set; }
    }

}
