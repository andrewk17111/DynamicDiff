using DynamicDiff;

if (args.Length != 2)
{
    Console.WriteLine("Usage: DynamicDiff.exe <source_file> <target_file>");
    return;
}

FileData source = new FileData(args[0]);
FileData target = new FileData(args[1]);

if (source.Size != target.Size)
    Console.WriteLine("\u001b[41mFiles are different sizes.");
else if (source.Created != target.Created)
    Console.WriteLine("Files were created at different times.");
else if (source.Modified != target.Modified)
    Console.WriteLine("Files were modified at different times.");
else if (source.IsHidden != target.IsHidden)
    Console.WriteLine("Files have different hidden attributes.");
else if (source.IsReadOnly != target.IsReadOnly)
    Console.WriteLine("Files have different read-only attributes.");
else
    Console.WriteLine("Files appear the same");
