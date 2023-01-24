using System.Security.Cryptography;

public class Scanee
{
    public Stream stream;
    public byte[] byteArray;
    public string _filename;
    public Scanee(string filename) {
        var stream = File.OpenRead(filename);
        var memoryStream = new MemoryStream();

        stream.CopyTo(memoryStream);
        byteArray = memoryStream.ToArray();
        _filename=filename;
            
        memoryStream.Close();
        stream.Close();
    }
} 