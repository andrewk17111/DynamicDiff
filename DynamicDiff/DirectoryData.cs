namespace DynamicDiff;

internal struct DirectoryData
{
    public string Name { get; }
    public string Location { get; }
    public string Path
        => System.IO.Path.Combine(Location, Name);
    public DateTime Created { get; }
    public DateTime Modified { get; }
    public bool IsHidden { get; }
    public bool IsReadOnly { get; }
    public uint FileCount
        => (uint)Files.Length;
    public uint DirectoryCount
        => (uint)Directories.Length;
    public string[] Files { get; }
    public string[] Directories { get; }

    public DirectoryData(string path)
    {
        DirectoryInfo dir = new DirectoryInfo(path);

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
