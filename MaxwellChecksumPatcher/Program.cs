using static System.Console;

namespace ChecksumPatcher;

using System.Collections.Generic;
using System.IO;

public class Program
{
    static bool TryGetBios(string path, out Bios bios)
    {
        try
        {
            bios = new Bios(path);
            return true;
        }
        catch (Exception _)
        {
            bios = default;
            return false;
        }
    }

    static bool ShouldUpdate(Bios bios) => bios.linhasDaBios.Any(b => b.ImageChecksum != b.GenerateChecksum());

    static void Update(Bios bios)
    {
        WriteLine("Let's update!");
        foreach (var b in bios.linhasDaBios)
        {
            b.ImageChecksum = b.GenerateChecksum();
        }

        var newPath = bios.caminhoArquivo.Replace(".rom", "_patched.rom");
        try
        {
            bios.EscreverBios(newPath);
            WriteLine($"Bios updated (saved to {newPath}), enjoy!");
        }
        catch (Exception ex)
        {
            WriteLine("Error occured while writing the file, please try again.");
        }
    }
    private static void InteractiveMode()
    {
        static string ReadPath()
        {
            bool isValid;
            var path = string.Empty;
            do
            {
                WriteLine("Please, enter the path of your rom file");
                path = ReadLine();
                isValid = File.Exists(path);
                if (!isValid)
                {
                    WriteLine("File does not exists, try again.");
                }
            } while (!isValid);

            return path;
        }

        static bool IsYesChoice(string choice)
        {
            return choice.Equals("y", StringComparison.CurrentCultureIgnoreCase) ||
                   choice.Equals("yes", StringComparison.CurrentCultureIgnoreCase);
        }

        static bool IsNoChoice(string choice)
        {
            return choice.Equals("n", StringComparison.CurrentCultureIgnoreCase) ||
                   choice.Equals("no", StringComparison.CurrentCultureIgnoreCase);
        }
        var path = ReadPath();
        if (!TryGetBios(path, out var bios))
        {
            WriteLine("error occured during reading of bios file, please check if its not corrupted.");
            ReadLine();
            return;
        }

        var shouldUpdate = bios.linhasDaBios.Any(b => b.ImageChecksum != b.GenerateChecksum());
        if (!shouldUpdate)
        {
            WriteLine("Nothing to do.");
            ReadLine();
            return;
        }

        bool isValidChoice;
        var choice = string.Empty;
        do
        {
            WriteLine("Checksum differs, wanna update? (y/n)");
            choice = ReadLine();
            isValidChoice = IsYesChoice(choice) || IsNoChoice(choice);
            if (!isValidChoice)
            {
                WriteLine("Please, only y/yes/n/no values are allowed.");
            }
        } while (!isValidChoice);

        if (IsNoChoice(choice))
        {
            WriteLine("Pff. Don't be scared.");
            ReadLine();
            return;
        }
        
        Update(bios);
        ReadLine();
    }
    
    public static void Main(string[] args)
    {
        WriteLine("Welcome to Maxwell checksum patcher");
        if (args.Length < 1)
        {
            InteractiveMode();
            return;
        }
        var path = args[0];
        if (!TryGetBios(path, out var bios))
        {
            WriteLine("error occured during reading of bios file, please check if its not corrupted.");
            return;
        }

        if (!ShouldUpdate(bios))
        {
            WriteLine("Nothing to do.");
            return;
        }
        WriteLine("Checksum differs.");
        Update(bios);
    }
}