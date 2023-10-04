
using RPSGame.Models;
using System.Security.Cryptography;
using System.Text;

namespace RPSGame.Services;

/// <summary>
/// Provides a data signing functionality. Signing is based on HMAC SHA3-256 algorithm.
/// </summary>
internal class HMACSHA2DataSigner : IDataSigner
{
    private readonly IKeyGenerator _keyGenerator;

    public HMACSHA2DataSigner(IKeyGenerator keyGenerator)
    {
        _keyGenerator = keyGenerator;
    }

    public SigningResult SignString(string data)
    {
        string keyString = _keyGenerator.GenerateKey(32);
        byte[] secretKey = Convert.FromHexString(keyString);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);

        using HMACSHA256 hasher = new(secretKey);

        byte[] signature = hasher.ComputeHash(dataBytes);
        string signatureString = Convert.ToHexString(signature);

        return new SigningResult(keyString, signatureString);
    }
}
