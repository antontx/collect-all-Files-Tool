using System;
using System.IO;

class Program {
    
    static void Main(string[] args)
    {
        Console.WriteLine("This tool was written & designed by @antontx to recursively copy files from a source directory\n"+
            "to a destination directory while preserving a specified number of directory levels in the destination path.\n");

        Console.Write("Enter the source directory path: ");
        string sourceDirectory = Console.ReadLine();

        Console.Write("Enter the destination directory path: ");
        string destinationDirectory = Console.ReadLine();

        Console.Write("Enter the number of levels to preserve: ");
        int levelsToPreserve = int.Parse(Console.ReadLine());


        int count = collectFiles(sourceDirectory, destinationDirectory,levelsToPreserve,0);
        Console.WriteLine("done. " + count + " files copied to " + destinationDirectory+"\n");

        Environment.Exit(0);
    }

    private static int collectFiles(string sourceDirectory, string destinationDirectory, int levelsToPreserve, int currentLevel){
        int filesCopied = 0;

        try {

            string[] content = Directory.GetFileSystemEntries(sourceDirectory);

            foreach (string item in content) {
                if (File.Exists(item))
                {

                    string fileName = Path.GetFileName(item);
                    File.Copy(item,Path.Combine(destinationDirectory, fileName));
                    filesCopied++;



                } else if (Directory.Exists(item))
                {
                    if (currentLevel >= levelsToPreserve){
                        filesCopied += collectFiles(item, destinationDirectory, levelsToPreserve, currentLevel + 1);
                    }
                    else
                    {
                        string subDestinationDirectory = Path.Combine(destinationDirectory, Path.GetFileName(item));
                        Directory.CreateDirectory(subDestinationDirectory);
                        filesCopied += collectFiles(item, subDestinationDirectory, levelsToPreserve, currentLevel + 1);
                    }


                }

            }

        }catch (Exception e) { Console.WriteLine(e.Message); }

        return filesCopied;
    }
}
