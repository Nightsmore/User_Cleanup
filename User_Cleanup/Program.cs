using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
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
        public static List<string> indexItems = new List<string>();
 
        [STAThread]
        static void Main()
        {
            profileList = RetrieveProfiles();
            foreach(Profile prof in profileList)
            {
                indexItems.Add(prof.path);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ProfileSelect());
        }

        private static List<Profile> RetrieveProfiles()
        {
          
            string[] keys = Registry.Users.GetSubKeyNames();
            List<Profile> results = new List<Profile>();

            foreach (var item in keys)
            {
                Profile profile = new Profile();
                try
                {
                    // retrieve the key
                    profile.registryKey = Registry.LocalMachine.OpenSubKey(profilesRegPath).OpenSubKey(item);

                    object testval = Registry.LocalMachine.OpenSubKey(profilesRegPath).OpenSubKey(item).GetValue("ProfileImagePath");

                    // set the path if it isn't null
                    if (testval != null)
                    {
                        profile.path = testval.ToString();
                    }

                    string[] arr = testval.ToString().Split('\\');
                    profile.name = arr[arr.Length - 1];


                    results.Add(profile);

                } catch (System.NullReferenceException e)
                {

                }
                

            }

            return results;
        }

        public static void DeleteProfile(Profile userProfile)
        {
            foreach(string file in Directory.GetFiles(userProfile.path))
            {
                File.Delete(file);
            }
            
            Registry.LocalMachine.DeleteSubKey(userProfile.registryKey.Name);
        }


    }

    class Profile
    {
        public string name; 
        public string path { get; set; }
        public RegistryKey registryKey { get; set; }

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
