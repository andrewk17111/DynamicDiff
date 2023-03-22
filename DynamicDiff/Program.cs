using DynamicDiff;
using FormattedConsole;

if (args.Length != 2)
{
    Console.WriteLine("Usage: DynamicDiff.exe <source> <target>");
    return;
}

if (File.Exists(args[0]) && File.Exists(args[1]))
{
    FileData source = new FileData(args[0]);
    FileData target = new FileData(args[1]);

    if (source.Size != target.Size)
        ANSIConsole.WriteLine("{fgred}Files are different sizes.{reset}");
    else if (source.Created != target.Created)
        ANSIConsole.WriteLine("{fgyellow}Files were created at different times.{reset}");
    else if (source.Modified != target.Modified)
        ANSIConsole.WriteLine("{fgorange}Files were modified at different times.{reset}");
    else if (source.IsHidden != target.IsHidden)
        ANSIConsole.WriteLine("{fgorange}Files have different hidden attributes.{reset}");
    else if (source.IsReadOnly != target.IsReadOnly)
        ANSIConsole.WriteLine("{fgred}Files have different read-only attributes.{reset}");
    else
        ANSIConsole.WriteLine("{fggreen}Files appear the same{reset}");
}
else if (Directory.Exists(args[0]) && Directory.Exists(args[1]))
{
    DirectoryData source = new DirectoryData(args[0]);
    DirectoryData target = new DirectoryData(args[1]);
    int s = 0;
    int t = 0;

    while (s < source.DirectoryCount && t < target.DirectoryCount)
    {
        if (source.Directories[s] == target.Directories[t])
        {
            ANSIConsole.WriteLine("{fgwhite} " + source.Directories[s]);
            s++;
            t++;
        }
        else if (Array.IndexOf(target.Directories[t..], source.Directories[s]) < 0)
        {
            ANSIConsole.WriteLine("{fgred}-" + source.Directories[s]);
            s++;
        }
        else if (Array.IndexOf(source.Directories[s..], target.Directories[t]) < 0)
        {
            ANSIConsole.WriteLine("{fggreen}+" + target.Directories[t]);
            t++;
        }
    }

    while (s < source.DirectoryCount)
        ANSIConsole.WriteLine("{fgred}-" + source.Directories[s++]);

    while (t < target.DirectoryCount)
        ANSIConsole.WriteLine("{fggreen}+" + target.Directories[t++]);

    s = 0;
    t = 0;

    while (s < source.FileCount && t < target.FileCount)
    {
        if (source.Files[s] == target.Files[t])
        {
            ANSIConsole.WriteLine("{fgwhite} " + source.Files[s]);
            s++;
            t++;
        }
        else if (Array.IndexOf(target.Files[t..], source.Files[s]) < 0)
        {
            ANSIConsole.WriteLine("{fgred}-" + source.Files[s]);
            s++;
        }
        else if (Array.IndexOf(source.Files[s..], target.Files[t]) < 0)
        {
            ANSIConsole.WriteLine("{fggreen}+" + target.Files[t]);
            t++;
        }
    }

    while (s < source.FileCount)
        ANSIConsole.WriteLine("{fgred}-" + source.Files[s++]);

    while (t < target.FileCount)
        ANSIConsole.WriteLine("{fggreen}+" + target.Files[t++]);
}
else
{
    Console.WriteLine($"Error: Please make sure that both '{args[0]}' and '{args[1]}' exist");
}

ANSIConsole.Write("{reset}");
