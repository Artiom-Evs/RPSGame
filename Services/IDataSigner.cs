
using RPSGame.Models;

namespace RPSGame.Services;

/// <summary>
/// Provides a data signing functionality.
/// </summary>
internal interface IDataSigner
{
    SigningResult SignString(string data);
}
