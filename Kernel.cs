using System;
using System.IO;
using FSl = Cosmos.System.FileSystem;
using Sys = Cosmos.System;

namespace NeonOS
{
    public class Kernel : Sys.Kernel
    {
        public FSl.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
        public string dirpath = @"0:\";
        protected override void BeforeRun()
        {
            Console.WriteLine("Starting Disk");
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            fs.Initialize(false);

            Console.WriteLine("Neon OS booted successfully");
            Console.WriteLine("Starting OS...");
            Console.Clear();
        }

        protected override void Run()
        {

            
            while (true)
            {

                Console.Write("term@");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(dirpath);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("$");
                string command = Console.ReadLine();
                // Console.WriteLine("     Debug: Command " + command);//Debug line

                string[] commands = command.Split(' ');
                // for (int i = 0; i < commands.Length - 1; i++)//Debug Line
                // {Console.WriteLine("         Debug[" + i + "] = "+commands[i]);}//Debug Line

                string args = command.Replace(commands[0] + " ", "");
                // Console.WriteLine("     Debug: Arguments is: " + args);



                if (commands[0] == "cd")
                {

                    cdcommand(args);

                }
                else if (commands[0] == "dir" || commands[0] == "ls")
                {
                    var director_list = Directory.GetDirectories(dirpath);
                    var directory_list = Directory.GetFiles(dirpath);
                    foreach (var file in director_list)
                    {
                        Console.WriteLine(file);
                    }
                    Console.Write("\n");
                    foreach (var file in directory_list)
                    {
                        Console.WriteLine(file);
                    }
                }
                else if (commands[0] == "echo")
                {

                    Console.WriteLine(args);

                }
                else if (commands[0] == "about" || commands[0] == "version")
                {
                    Console.WriteLine("Neon OS 1.0 class idk what i am saying rigtht now idk");
                }
                else if (commands[0] == "\n" || commands[0] == "" || commands[0] == null)
                {
                    goto End;
                }
                else if (commands[0] == "cls" || commands[0] == "clear")
                {
                    Console.Clear();
                }
                else if (commands[0] == "mkdir")
                {
                    Directory.CreateDirectory(dirpath + args);
                }
                else if (commands[0] == "rmdir")
                {
                    Directory.Delete(dirpath + args, true);
                }
                else if (commands[0] == "mkfile")
                {
                    File.Create(dirpath + args);
                }
                else if (commands[0] == "rm")
                {
                    File.Delete(dirpath + args);
                }
                else if (commands[0] == "disks")
                {
                    fs.Initialize(true);
                }
                else if (commands[0] == "free")
                {
                    string[] first = dirpath.Split('\\');

                    var available_space = fs.GetAvailableFreeSpace(first[0] + "\\");

                    Console.WriteLine("Available Free Space: " + available_space);
                }
                else if (commands[0] == "dtype")
                {
                    string[] first = dirpath.Split('\\');

                    var fs_type = fs.GetFileSystemType(first[0] + "\\");
                    Console.WriteLine("File System Type: " + fs_type);
                }
                else if (commands[0] == "help")
                {
                    Console.WriteLine("Neon OS help\n" +
                        "about - Shows info about system\n" +
                        "ls    - Shows directory contents\n" +
                        "clear - Clear the terminal\n" +
                        "echo[text]  - Print text\n" +
                        "mkdir [dir] - Make directory\n" +
                        "rmdir [dir] - Delete directory\n" +
                        "mkfile[file]- Make file\n" +
                        "rm [file]   - Remove file\n" +
                        "disks       - Show info about disks\n" +
                        "free        - Shows free space on the disks\n" +
                        "dtype       - Print current disk type\n" +
                        "help        - Show Help\n" +
                        "reboot      - reboot");
                }
                else if (commands[0] == "shutdown" || commands[0] == "restart" || commands[0] == "reboot")
                {
                    Sys.Power.Reboot();
                }





                else { Console.WriteLine("Sorry, but this command is invalid."); }


            }
        End:
            Console.Write("");
        }

        public bool cdcommand(string arguments)
        {

            if (arguments == null || arguments == " " || arguments == "")
            {
                Console.WriteLine("Error");
                return false;
            }
            else if (arguments == "..")
            {
                
           

                string[] paths = dirpath.Split('\\');
                dirpath = "";
                for (int i = 0; i < paths.Length - 2; i++)
                {
                    dirpath += paths[i] + "\\";
                }




            }
            else
            {
                if (Directory.Exists(dirpath + arguments))
                {
                    dirpath += arguments + @"\";
                }
            }




            return false;
        }
        public bool dircommand()
        {
            try
            {
                var dirs_list = Directory.GetDirectories(dirpath);
                var directory_list = Directory.GetFiles(dirpath);
                foreach (var file in directory_list)
                {
                    Console.WriteLine(file);
                }
                foreach (var file in dirs_list)
                {
                    Console.WriteLine(file);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
