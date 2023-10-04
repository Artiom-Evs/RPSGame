
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
    private readonly Func<string[], int> _pcMoveHandler;
    private readonly Func<string[], int> _playerMoveHandler;

    public GameContext(IDataSigner dataSigner, string[] moveVariants, Func<string[], int> pcMoveHandler, Func<string[], int> playerMoveHandler)
    {
        _dataSigner = dataSigner;
        _moveVariants = moveVariants;
        _playerMoveHandler = playerMoveHandler; ;
        _pcMoveHandler = pcMoveHandler;
    }

    public string[] MoveVariants => _moveVariants;

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
        int roundNumber = 1;
        RoundResults roundResult;

        this.GameStarted?.Invoke(this, new GameStartedEventArgs());

        do
        {
            this.RoundStarted?.Invoke(this, new RoundStartedEventArgs(roundNumber++));
            this.PCMoveStarted?.Invoke(this, new PCMoveStartedEventArgs());
            
            int pcChoice = _pcMoveHandler(_moveVariants);
            var signResult = _dataSigner.SignString(_moveVariants[pcChoice]);
            
            this.PCMoveEnded?.Invoke(this, new PCMoveEndedEventArgs(signResult.Signature));
            this.PlayerMoveStarted?.Invoke(this, new PlayerMoveStartedEventArgs());
            
            int playerChoice = _playerMoveHandler(_moveVariants);
            
            if (playerChoice == MoveVariants.Length)
            {
                roundResult = RoundResults.PlayerCapitulate;
            }
            else
            {
                this.PlayerMoveEnded?.Invoke(this, new PlayerMoveEndedEventArgs(MoveVariants[playerChoice]));
                roundResult = ChoiceComparer.Compare(pcChoice, playerChoice, MoveVariants.Length);
            }

            this.RoundEnded?.Invoke(this, new RoundEndedEventArgs(roundResult, signResult.SecretKey, MoveVariants[pcChoice]));
        } while (roundResult == RoundResults.Draw);

        this.GameEnded?.Invoke(this, new GameEndedEventArgs(roundResult));
    }
}
