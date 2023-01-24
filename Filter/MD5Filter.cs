using System.Security.Cryptography;

namespace Filter
{
    public class SHA256Filter : IFilter
    {
        private string _meta {get;set;}
        //transportationPackage5.png
        List<string> md5signatures = new List<string>() { "4E76B12B377F4BFAAB38FA10648098BDFC49A99FF2B765B96A47F97E02526627" };
        // toscan/3.d.2PrehospitalPracticesUserGuide.pdf
        public string meta
        {
            get { return _meta; }
            set { _meta = value; }
        }
        public bool IsSuspect(Scanee obj) {
            using (var sha256 = SHA256.Create())
            {
                var m = sha256.ComputeHash(obj.byteArray);
                Console.WriteLine("Hash is " + Convert.ToHexString(m) + " for " + obj._filename);
                if(md5signatures.Contains(Convert.ToHexString(m).ToUpper())) {
                    _meta= "suspected by MD5 match";
                    return true;
                };
            }
            return false;
            
        }
    }
}
