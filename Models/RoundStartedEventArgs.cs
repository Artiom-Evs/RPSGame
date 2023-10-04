namespace RPSGame.Models;

internal class RoundStartedEventArgs : EventArgs
{
    public RoundStartedEventArgs(int roundNumber)
    {
        RoundNumber = roundNumber;
    }

    public int RoundNumber { get; }
}
