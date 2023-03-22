namespace DynamicDiff;

internal struct FileData
{
    public readonly string Name;
    public readonly string Location;
    public readonly long Size;
    public readonly DateTime Created;
    public readonly DateTime Modified;
    public readonly bool IsHidden;
    public readonly bool IsReadOnly;

    public readonly string Path
        => System.IO.Path.Combine(Location, Name);

    public FileData(string path) : this(new FileInfo(path))
    { }

    public FileData(FileInfo file)
    {
        Name = file.Name;
        Location = file.DirectoryName;
        Size = file.Length;
        Created = file.CreationTime;
        Modified = file.LastAccessTime;
        IsHidden = file.Attributes.HasFlag(FileAttributes.Hidden);
        IsReadOnly = file.Attributes.HasFlag(FileAttributes.ReadOnly);
    }

    public string[] Read()
        => throw new NotImplementedException();

    public string[] Compare(FileData target)
    {
        if (Size != target.Size)
            return new string[] { "{fgred}Files are different sizes.{reset}" };
        else if (Created != target.Created)
            return new string[] { "{fgyellow}Files were created at different times.{reset}" };
        else if (Modified != target.Modified)
            return new string[] { "{fgorange}Files were modified at different times.{reset}" };
        else if (IsHidden != target.IsHidden)
            return new string[] { "{fgorange}Files have different hidden attributes.{reset}" };
        else if (IsReadOnly != target.IsReadOnly)
            return new string[] { "{fgred}Files have different read-only attributes.{reset}" };
        else
            return new string[] { "{fggreen}Files appear the same{reset}" };
    }

    public override string ToString()
        => $"{Size} {Created} {Modified} {(IsHidden ? "H" : "")}{(IsReadOnly ? "R" : "")} {Name}";
}
