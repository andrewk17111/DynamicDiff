namespace DynamicDiff;

internal struct FileData
{
    public string Name { get; }
    public string Location { get; }
    public string Path
        => System.IO.Path.Combine(Location, Name);
    public ulong Size { get; }
    public DateTime Created { get; }
    public DateTime Modified { get; }
    public bool IsHidden { get; }
    public bool IsReadOnly { get; }

    public FileData(string name, string location, ulong size, DateTime created, DateTime modified, bool is_hidden, bool is_read_only)
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
}
