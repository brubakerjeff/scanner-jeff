using System.Security.Cryptography;


    public class Scanee
    {
        public Stream stream;
        byte[] byteArray;
        public string _filename;
        public Scanee(string filename) {
            using (var md5 = MD5.Create())
            {
                var stream = File.OpenRead(filename);
                var memoryStream = new MemoryStream();

                stream.CopyTo(memoryStream);
                byteArray = memoryStream.ToArray();
                _filename=filename;
            }
        }
    } 