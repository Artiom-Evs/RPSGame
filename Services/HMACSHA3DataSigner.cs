
using RPSGame.Models;
using System.Security.Cryptography;

namespace RPSGame.Services;

/// <summary>
/// Provides a data signing functionality. Signing is based on HMAC SHA3-256 algorithm.
/// </summary>
internal class HMACSHA3DataSigner : IDataSigner
{
    private readonly IKeyGenerator _keyGenerator;

    public HMACSHA3DataSigner(IKeyGenerator keyGenerator)
    {
        _keyGenerator = keyGenerator;
    }

    public SigningResult SignString(string data)
    {
        string keyString = _keyGenerator.GenerateKey(32);
        byte[] secretKey = Convert.FromBase64String(keyString);
        byte[] dataBytes = Convert.FromBase64String(data);

        using HMACSHA3_256 hasher = new(secretKey);

        byte[] signature = hasher.ComputeHash(dataBytes);
        string signatureString = Convert.ToBase64String(signature);

        return new SigningResult(keyString, signatureString);
    }
}
