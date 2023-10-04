namespace RPSGame.Models;

internal class GameEndedEventArgs : EventArgs
{
    public GameEndedEventArgs(RoundResults roundResults)
    {
        RoundResult = roundResults;
    }

    public RoundResults RoundResult { get; }
}
