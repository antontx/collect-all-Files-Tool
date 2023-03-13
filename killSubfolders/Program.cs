using System;
using System.IO;

class Program {
    
    static void Main(string[] args)
    {
        string sourceDirectory = @"D:\files\documents\folderA";
        string destinationDirectory = @"D:\files\documents\folderB";

        int count = collectFiles(sourceDirectory, destinationDirectory);
        Console.WriteLine("done. " + count + " files copied to " + destinationDirectory);
    }

    private static int collectFiles(string sourceDirectory, string destinationDirectory)
    {
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
                    filesCopied += collectFiles(item, destinationDirectory);
                }

            }

        }catch (Exception e) { Console.WriteLine(e.Message); }

        return filesCopied;
    }
}
