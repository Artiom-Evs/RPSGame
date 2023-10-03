
using RPSGame.Models;
using RPSGame.Services;

namespace RPSGame.Infrastructure;

/// <summary>
/// Implements logic of a generalized game Rock-Paper-Scissors. 
/// </summary>
internal class GameContext
{
    private readonly IDataSigner _dataSigner;
    private readonly string[] _moveVariants;

    public GameContext(IDataSigner encryptor, string[] moveVariants)
    {
        _dataSigner = dataSigner;
        _moveVariants = moveVariants;
    }

    public event EventHandler<GameStartedEventArgs>? GameStarted;
    public event EventHandler<GameEndedEventArgs>? GameEnded;
    public event EventHandler<RoundStartedEventArgs>? RoundStarted;
    public event EventHandler<RoundEndedEventArgs>? RoundEnded;
    public event EventHandler<PCMoveStartedEventArgs>? PCMoveStarted;
    public event EventHandler<PCMoveEndedEventArgs>? PCMoveEnded;
    public event EventHandler<PlayerMoveStartedEventArgs>? PlayerMoveStarted;
    public event EventHandler<PlayerMoveEndedEventArgs>? PlayerMoveEnded;
    
    public void StartGame()
    {
        
    }

    public void Capitulate()
    {

    }
}
}
