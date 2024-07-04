
namespace Cryptographer;
using System.IO;
using System.Text;
using Cryptonix.Security;

public class Cryptographer {
    public static void Crypt(string pass, string inputFullFileName, string outputFullFileName, bool isEncryption)
    {
        byte[] inputFileBytes = File.ReadAllBytes(inputFullFileName);
        byte[] passBytes = Encoding.Unicode.GetBytes(pass);
        byte[] outputFileBytes;
        if(isEncryption) {
            outputFileBytes = Xorer.Xor4Encrypt(inputFileBytes, passBytes);
            outputFileBytes = Spiralizer.Spiralize(outputFileBytes);
        }
        else {
            outputFileBytes = Spiralizer.Despiralize(inputFileBytes);
            outputFileBytes = Xorer.Xor4Dencrypt(outputFileBytes, passBytes);
        }
        File.WriteAllBytes(outputFullFileName, outputFileBytes);
    }
}