using DynamicDiff;
using FormattedConsole;

if (args.Length != 2)
{
    Console.WriteLine("Usage: DynamicDiff.exe <source_file> <target_file>");
    return;
}

FileData source = new FileData(args[0]);
FileData target = new FileData(args[1]);

if (source.Size != target.Size)
    ANSIConsole.WriteLine("{fgred}Files are different sizes.{reset}");
else if (source.Created != target.Created)
    ANSIConsole.WriteLine("{fgred}Files were created at different times.{reset}");
else if (source.Modified != target.Modified)
    ANSIConsole.WriteLine("{fgred}Files were modified at different times.{reset}");
else if (source.IsHidden != target.IsHidden)
    ANSIConsole.WriteLine("{fgred}Files have different hidden attributes.{reset}");
else if (source.IsReadOnly != target.IsReadOnly)
    ANSIConsole.WriteLine("{fgred}Files have different read-only attributes.{reset}");
else
    ANSIConsole.WriteLine("{fggreen}Files appear the same{reset}");
