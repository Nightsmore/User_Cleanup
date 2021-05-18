using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace User_Cleanup
{
    public class FileSysTools
    {
        public static List<string> GetDirectoryContents(string path, string profilesRegPath)
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
                                    }
                                    catch (UnauthorizedAccessException _ex)
                                    {
                                        Directory.SetAccessControl(dir, fileSecurity);
                                        fin = false;
                                    }
                                    catch (FileNotFoundException _ex)
                                    {
                                        FileAttributes fa = File.GetAttributes(dir);
                                        // Clear attributes
                                        if (fa.HasFlag(FileAttributes.Hidden))
                                        {
                                            var info = new DirectoryInfo(dir);
                                            info.Attributes &= ~FileAttributes.Hidden;
                                        }


                                        if (fa.HasFlag(FileAttributes.ReadOnly))
                                        {
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
                        Directory.SetAccessControl(subDir, fileSecurity);
                        fin = false;
                    }

                }
                files = updated;
            } while (!fin);

            return files;
        }

        //

        public static async void DeleteProfile(Profile userProfile, string profilesRegPath)
        {

            // This needs to send back some progress indications 

            var fileSecurity = Directory.GetAccessControl(userProfile.path);
            fileSecurity.AddAccessRule(new FileSystemAccessRule("administrators", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit,
                PropagationFlags.InheritOnly, AccessControlType.Allow));
            fileSecurity.AddAccessRule(new FileSystemAccessRule("administrators", FileSystemRights.FullControl, InheritanceFlags.ObjectInherit,
                PropagationFlags.InheritOnly, AccessControlType.Allow));

            try
            {
                if (userProfile.registryKey != null)
                    Registry.LocalMachine.OpenSubKey(profilesRegPath, true).DeleteSubKey(userProfile.registryKeyName, false);
            }
            catch (System.ArgumentException)
            {
                // msg output
            }
            catch (System.Exception)
            {
                // Do nothing
            }

            List<string> directories = GetDirectoryContents(userProfile.path, profilesRegPath);

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
                            var info = new DirectoryInfo(directory);
                            info.Attributes &= ~FileAttributes.Hidden;
                        }


                        if (fa.HasFlag(FileAttributes.ReadOnly))
                        {
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

        // Retrieve profiles on this machine
        // 
        public static List<Profile> RetrieveProfiles(string profilesRegPath)
        {

            string[] keys = Registry.LocalMachine.OpenSubKey(profilesRegPath).GetSubKeyNames();
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

                }
                catch (System.NullReferenceException e)
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
                    foreach (Profile e in results)
                    {
                        if (e.path.ToLower().Equals(item.ToLower()))
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

                }
                catch (Exception _e)
                {

                }
            }

            // "Users" subfolder return. 

            return results;
        }
    }
}
