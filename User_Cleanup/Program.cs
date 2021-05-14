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

            string[] keys       = Registry.LocalMachine.OpenSubKey(profilesRegPath).GetSubKeyNames();
            string[] subFolders = Directory.GetDirectories(@"c:\users");
            
            List<Profile> results = new List<Profile>();

            // REG RETRIEVE 
            foreach (var item in keys)
            {
                Profile profile = new Profile();
                
                profile.registryKeyName = item;
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

            // Checks the profile directory
            foreach (var item in subFolders)
            {
                // Value to determine if the profile is already in the list
                bool cont = true;
                try
                {
                    // iterates throguh profile list, checks the path value of each item
                    foreach(Profile e in results)
                    {
                        if(e.path.ToLower().Equals(item.ToLower()))
                        {
                            // if a matching value is found, continue is set to false
                            cont = false;
                        }
                    }


                    // if continue remains true
                    if (cont == true)
                    {
                        // a new profile item is created.
                        var prof = new Profile();
                        string[] arr = item.Split('\\');
                        
                        // Get the name off the path
                        prof.path = item;
                        prof.name = arr[arr.Length - 1];

                        // add profile to list
                        results.Add(prof);
                    }

                } catch (Exception _e)
                {

                }
            }

            // "Users" subfolder return. 

            return results;
        }

        public static void DeleteProfile(Profile userProfile)
        {

            // Need to add an idication that items are actually deleting
            // Create new progress bar

            var fileSecurity = Directory.GetAccessControl(userProfile.path);
            fileSecurity.AddAccessRule(new FileSystemAccessRule("administrators", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit,
                PropagationFlags.InheritOnly, AccessControlType.Allow));
            fileSecurity.AddAccessRule(new FileSystemAccessRule("administrators", FileSystemRights.FullControl, InheritanceFlags.ObjectInherit,
                PropagationFlags.InheritOnly, AccessControlType.Allow));

            try
            {
                if(userProfile.registryKey != null)
                    Registry.LocalMachine.OpenSubKey(profilesRegPath,true).DeleteSubKey(userProfile.registryKeyName, false);
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show("Subkey Error", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Exception)
            {
                // Do nothing
            }

            List<string> directories = GetDirectoryContents(userProfile.path);
            
            // This gets the files in the directories
            try
            {
                List<string> appended = new List<string>();
                appended.AddRange(directories);
                bool fin = false;
                
                do
                {
                    fin = true;
                    foreach (string directory in directories)
                    {
                        

                        FileAttributes fa = File.GetAttributes(directory);
                        // Clear attributes
                        if (fa.HasFlag(FileAttributes.Hidden))
                        {
                            printf("removing hidden stuff");
                            var info = new DirectoryInfo(directory);
                            info.Attributes &= ~FileAttributes.Hidden;
                        }


                        if (fa.HasFlag(FileAttributes.ReadOnly))
                        {
                            printf("changing to not read only");
                            var info = new DirectoryInfo(directory);
                            info.Attributes &= ~FileAttributes.ReadOnly;
                        }
                        try
                        {
                            appended.AddRange(Directory.GetFiles(directory));
                        }
                        catch (UnauthorizedAccessException _ex)
                        {
                            fin = false;
                            Directory.SetAccessControl(directory, fileSecurity);
                        }
                    }
                } while (!fin);
            }
            catch (UnauthorizedAccessException _ex)
            {
                // ?
            }
            

        }

        static List<string> GetDirectoryContents(string path)
        {
            List<string> files = new List<string>();
            var fileSecurity = Directory.GetAccessControl(path);
            fileSecurity.AddAccessRule(new FileSystemAccessRule("administrators", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit,
                PropagationFlags.InheritOnly, AccessControlType.Allow));
            fileSecurity.AddAccessRule(new FileSystemAccessRule("administrators", FileSystemRights.FullControl, InheritanceFlags.ObjectInherit,
                PropagationFlags.InheritOnly, AccessControlType.Allow));

            // Get All Directories 
            // Parent task
            foreach (string directory in (IEnumerable<string>)Directory.EnumerateDirectories(path))
            {
                //printf(directory);
                // If they are not in the files list, adds the range. 
                if (!files.Contains(directory))
                {
                    files.Add(directory);
                    files.AddRange(Directory.GetDirectories(directory));
                }
            }

            // get the first object in files

            List<string> updated = new List<string>();
            updated.AddRange(files);



            bool fin = false;

            do
            {
                foreach (string subDir in files)
                {
                    fin = true;
                    // Put that one object in a loop
                    // Get the subdirectories of that one object and iterate until unable

                    try
                    {
                        if (Directory.GetDirectories(subDir) != null)
                        {
                            foreach (string dir in Directory.GetDirectories(subDir))
                            {
                                if (!updated.Contains(dir))
                                {
                                    updated.Add(dir);
                                    try
                                    {
                                        updated.AddRange(Directory.GetDirectories(dir));
                                        fin = false;
                                        printf($"ap@: {dir}");
                                    }
                                    catch (UnauthorizedAccessException _ex)
                                    {
                                        printf("setting file security");
                                        Directory.SetAccessControl(dir, fileSecurity);
                                        fin = false;
                                    }
                                    catch (FileNotFoundException _ex)
                                    {
                                        FileAttributes fa = File.GetAttributes(dir);
                                        // Clear attributes
                                        if (fa.HasFlag(FileAttributes.Hidden))
                                        {
                                            printf("removing hidden stuff");
                                            var info = new DirectoryInfo(dir);
                                            info.Attributes &= ~FileAttributes.Hidden;
                                        }


                                        if (fa.HasFlag(FileAttributes.ReadOnly))
                                        {
                                            printf("changing to not read only");
                                            var info = new DirectoryInfo(dir);
                                            info.Attributes &= ~FileAttributes.ReadOnly;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (AccessViolationException _ex)
                    {
                        printf("setting file security");
                        Directory.SetAccessControl(subDir, fileSecurity);
                        fin = false;
                    }

                }
                files = updated;
            } while (!fin);
            printf("files retrieved.");

            return files;
        }

        static void printf(string str)
        {
            Console.WriteLine(str);
        }
    }
}

class Profile
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


