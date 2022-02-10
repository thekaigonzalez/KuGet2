// See https://aka.ms/new-console-template for more information
using System.IO;
using System.IO.Compression;

void Error(string text)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write("error: ");
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.WriteLine(text);
    Environment.Exit(1);
}


if (args.Length == 0 || args[0] == "-help") {
    
    Console.WriteLine("KuGet Software");
    Console.WriteLine("\nKuGet is an application for installing software from ZIP Files\nThere are no current features");
    Console.WriteLine("to install ZIPs from the internet automatically, once installing you can add the path to install them.");
    Environment.Exit(0);
}

Console.WriteLine("Installing from file - " + args[0]);

if (File.Exists(args[0])) {
    Console.WriteLine("Extracing...");
    if (Directory.Exists("cache")) Directory.Delete("cache", true);
    if (Directory.Exists("install")) Directory.Delete("install", true);
    ZipFile.ExtractToDirectory(args[0], "cache");

    Thread.Sleep(1);

    Console.WriteLine("Checking for software/");
    
    if (Directory.Exists("cache/software/")) {
        Console.WriteLine("Found software, installing...");
        string[] farray = Directory.GetFiles("cache/software/");
        
        if (!Directory.Exists("install")) {
            Directory.CreateDirectory("install");
            Thread.Sleep(2);
        }

        var f = "";
        foreach (var item in farray)
        {
            var name = "soft" + new Random().Next(1003, 23445).ToString() + ".exe";
           
            Console.WriteLine("Processing triggers for ... " + item );
            Console.WriteLine("Saving as ... " + name );
            
            Thread.Sleep(new Random().Next(1, 4));

            File.Copy(item, "install/" + name);

            f += item + " - " + name;
        }

        File.WriteAllText("install/registration.txt", f);
    } else {
        Error("could not find 'software'");
    }
} else {
    Error("Could not find file '" + args[0] + "'");
}
