namespace UtsDB.Domain.Config;

public class DatabaseConfig
{
    public string BaseDir { get; set; }
    public string TableDir => $"{BaseDir}{Path.DirectorySeparatorChar}tables";
    public string ConfigDir => $"{BaseDir}{Path.DirectorySeparatorChar}config";
    public string TableMetaDataFile => $"{BaseDir}{Path.DirectorySeparatorChar}tablemetadata";
}