namespace DynamicDiff;

internal struct DirectoryData
{
    public readonly string Name;
    public readonly string Location;
    public readonly DateTime Created;
    public readonly DateTime Modified;
    public readonly bool IsHidden;
    public readonly bool IsReadOnly;
    public readonly string[] Files;
    public readonly string[] Directories;

    public readonly string Path
        => System.IO.Path.Combine(Location, Name);
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
        Files = dir.GetFiles().Select(f => f.FullName).ToArray();
        Directories = dir.GetDirectories().Select(d => d.FullName).ToArray();
    }

    public override string ToString()
        => $"{DirectoryCount} {FileCount} {Created} {Modified} {(IsHidden ? "H" : "")}{(IsReadOnly ? "R" : "")} {Name}";
}
