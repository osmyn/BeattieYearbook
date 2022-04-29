using System;
using System.Globalization;
using System.IO;

namespace BeattiePics
{
    class Program
    {
        static void Main(string[] args)
        {
            var startFolder = @"D:\BeattiePresentation\2019";
            var grade = "003";
            var outFolder = @"D:\BeattiePresentation\BeattieKids";
            var delimiter = new char[] { '\t' };
            var reader = new StreamReader(Path.Combine(startFolder, "MASTER.TXT"));

            // File columns (0-based)
            var iFolder = 1;
            var iImage = 2;
            var iGrade = 3;
            var iLastName = 4;
            var iFirstName = 5;

            while (!reader.EndOfStream)
            {
                try
                {
                    var line = reader.ReadLine().Split(delimiter);
                    if (line[iGrade] != grade) continue;
                    var first = Titlized(line[iFirstName]);
                    var last = Titlized(line[iLastName]);
                    var picPath = Path.Combine(startFolder, line[iFolder], line[iImage]);
                    var kidFile = $"{last}_{first}_{grade}.jpg";
                    var outPath = Path.Combine(outFolder, kidFile);
                    if(!File.Exists(outPath))
                    {
                        File.Copy(picPath, outPath);
                    }                    
                    Console.WriteLine($"Copied {first} {last}'s image to {outPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Done!");
            Console.ReadKey();
        }

        static string Titlized(string input)
        {
            var ti = new CultureInfo("en-US", false).TextInfo;
            return ti.ToTitleCase(input.ToLower());
        }
    }
}
