using FormattedConsole;

namespace DynamicDiff;

internal struct DirectoryData
{
    private readonly FileInfo[] _files;
    private readonly DirectoryInfo[] _directories;

    public readonly string Name;
    public readonly string Location;
    public readonly DateTime Created;
    public readonly DateTime Modified;
    public readonly bool IsHidden;
    public readonly bool IsReadOnly;

    public readonly string Path
        => System.IO.Path.Combine(Location, Name);
    public readonly string[] Files
        => _files.Select(f => f.Name).ToArray();
    public readonly string[] Directories
        => _directories.Select(d => d.Name).ToArray();
    public readonly uint FileCount
        => (uint)Files.Length;
    public readonly uint DirectoryCount
        => (uint)Directories.Length;

    public DirectoryData(string path) : this(new DirectoryInfo(path))
    { }

    public DirectoryData(DirectoryInfo dir)
    {
        Name = dir.Name;
        Location = dir.Parent.FullName;
        Created = dir.CreationTime;
        Modified = dir.LastAccessTime;
        IsHidden = dir.Attributes.HasFlag(FileAttributes.Hidden);
        IsReadOnly = dir.Attributes.HasFlag(FileAttributes.ReadOnly);

        _files = dir.GetFiles();
        _directories = dir.GetDirectories();
    }

    public string[] Compare(DirectoryData target, bool recursive = false)
    {
        List<string> result = new List<string>();
        int s = 0;
        int t = 0;

        while (s < DirectoryCount && t < target.DirectoryCount)
        {
            if (Directories[s] == target.Directories[t])
            {
                result.Add("{fgwhite} " + Directories[s]);

                if (recursive)
                    result.AddRange(new DirectoryData(_directories[s])
                        .Compare(new DirectoryData(target._directories[t]))
                        .Select(d => $"  {d}"));

                s++;
                t++;
            }
            else if (Array.IndexOf(target.Directories[t..], Directories[s]) < 0)
            {
                result.Add("{fgred}-" + Directories[s]);
                
                if (recursive)
                    result.AddRange(ListContents(_directories[s], "-"));
                
                s++;
            }
            else if (Array.IndexOf(Directories[s..], target.Directories[t]) < 0)
            {
                result.Add("{fggreen}+" + target.Directories[t]);

                if (recursive)
                    result.AddRange(ListContents(target._directories[t], "+"));
                
                t++;
            }
        }

        while (s < DirectoryCount)
            result.Add("{fgred}-" + Directories[s++]);

        while (t < target.DirectoryCount)
            result.Add("{fggreen}+" + target.Directories[t++]);

        s = 0;
        t = 0;

        while (s < FileCount && t < target.FileCount)
        {
            if (Files[s] == target.Files[t])
            {
                result.Add("{fgwhite} " + Files[s]);
                s++;
                t++;
            }
            else if (Array.IndexOf(target.Files[t..], Files[s]) < 0)
            {
                result.Add("{fgred}-" + Files[s]);
                s++;
            }
            else if (Array.IndexOf(Files[s..], target.Files[t]) < 0)
            {
                result.Add("{fggreen}+" + target.Files[t]);
                t++;
            }
        }

        while (s < FileCount)
            result.Add("{fgred}-" + Files[s++]);

        while (t < target.FileCount)
            result.Add("{fggreen}+" + target.Files[t++]);

        return result.ToArray();
    }

    private static string[] ListContents(DirectoryInfo directory, string prefix = "")
    {
        List<string> result = new List<string>();

        foreach (DirectoryInfo dir in directory.GetDirectories())
        {
            result.Add($"  {prefix}{dir.Name}");
            result.AddRange(ListContents(dir).Select(d => $"    {prefix}{d}"));
        }

        foreach (FileInfo file in directory.GetFiles())
            result.Add($"  {prefix}{file.Name}");

        return result.ToArray();
    }

    public override string ToString()
        => $"{DirectoryCount} {FileCount} {Created} {Modified} {(IsHidden ? "H" : "")}{(IsReadOnly ? "R" : "")} {Name}";
}
