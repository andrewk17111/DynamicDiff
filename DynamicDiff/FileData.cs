namespace DynamicDiff;

internal struct FileData
{
    public string Name { get; }
    public string Location { get; }
    public string Path
        => System.IO.Path.Combine(Location, Name);
    public long Size { get; }
    public DateTime Created { get; }
    public DateTime Modified { get; }
    public bool IsHidden { get; }
    public bool IsReadOnly { get; }

    public FileData(string path)
    {
        FileInfo file = new FileInfo(path);

        Name = file.Name;
        Location = file.DirectoryName;
        Size = file.Length;
        Created = file.CreationTime;
        Modified = file.LastAccessTime;
        IsHidden = file.Attributes.HasFlag(FileAttributes.Hidden);
        IsReadOnly = file.Attributes.HasFlag(FileAttributes.ReadOnly);
    }

    private FileData(string name, string location, long size, DateTime created, DateTime modified, bool is_hidden,
        bool is_read_only)
    {
        Name = name;
        Location = location;
        Size = size;
        Created = created;
        Modified = modified;
        IsHidden = is_hidden;
        IsReadOnly = is_read_only;
    }

    public string[] Read()
        => throw new NotImplementedException();

    public override string ToString()
        => $"{Size} {Created} {Modified} {(IsHidden ? "H" : "")}{(IsReadOnly ? "R" : "")} {Name}";
}
