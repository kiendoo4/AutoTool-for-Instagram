
using System;
using System.IO;

namespace BT1._5
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter the directory path:");
            string directoryPath = Console.ReadLine();

            if (Directory.Exists(directoryPath))
            {
                string[] files = Directory.GetFiles(directoryPath);

                if (files.Length > 0)
                {
                    Console.WriteLine("Files in the directory:");
                    foreach (string file in files)
                    {
                        Console.WriteLine(Path.GetFileName(file));
                    }
                }
                else
                {
                    Console.WriteLine("No files found in the directory.");
                }
            }
            else
            {
                Console.WriteLine("The specified directory does not exist.");
            }
        }
    }
}