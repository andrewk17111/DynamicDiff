using DynamicDiff;
using FormattedConsole;

if (args.Length < 2)
{
    Console.WriteLine("Usage: DynamicDiff.exe <source> <target>");
    return;
}

bool recursive = false;

if (args.Length > 2)
{
    foreach (string arg in args)
    {
        if (arg.StartsWith("--"))
        {
            switch (arg[2..])
            {
                case "recursive":
                    recursive = true;
                    break;
            }
        }
        else if (arg.StartsWith("-"))
        {
            foreach (char flag in arg[1..])
            {
                switch (flag)
                {
                    case 'r':
                        recursive = true;
                        break;
                }
            }
        }
    }
}

if (File.Exists(args[0]) && File.Exists(args[1]))
{
    foreach (string line in new FileData(args[0]).Compare(new FileData(args[1])))
        ANSIConsole.WriteLine(line);
}
else if (Directory.Exists(args[0]) && Directory.Exists(args[1]))
{
    foreach (string line in new DirectoryData(args[0]).Compare(new DirectoryData(args[1]), recursive))
        ANSIConsole.WriteLine(line);
}
else
{
    Console.WriteLine($"Error: Please make sure that both '{args[0]}' and '{args[1]}' exist");
}

ANSIConsole.Write("{reset}");
