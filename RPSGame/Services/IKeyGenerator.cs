
namespace RPSGame.Services;

/// <summary>
/// Provides a random key generation functionality.
/// </summary>
internal interface IKeyGenerator
{
    string GenerateKey(int length);
}
