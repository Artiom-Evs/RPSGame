
namespace RPSGame.Models;

internal class SigningResult
{
    public SigningResult(string secretKey, string signature)
    {
        SecretKey = secretKey;
        Signature = signature;
    }

    public string SecretKey { get; }
    public string Signature { get; }
}
