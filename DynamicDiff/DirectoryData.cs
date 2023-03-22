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

    public override string ToString()
        => $"{DirectoryCount} {FileCount} {Created} {Modified} {(IsHidden ? "H" : "")}{(IsReadOnly ? "R" : "")} {Name}";
}
