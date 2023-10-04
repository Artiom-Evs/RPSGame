
using System.Security.Cryptography;

namespace RPSGame.Services;

/// <summary>
/// Provides a random key generation functionality.
/// </summary>
internal class BaseKeyGenerator : IKeyGenerator
{
    public string GenerateKey(int length) =>
        Convert.ToHexString(RandomNumberGenerator.GetBytes(length));
}
