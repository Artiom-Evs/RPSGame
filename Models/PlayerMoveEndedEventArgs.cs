namespace RPSGame.Models;

internal class PlayerMoveEndedEventArgs : EventArgs
{
    public PlayerMoveEndedEventArgs(string playerChoice)
    {
        PlayerChoice = playerChoice;
    }

    public string PlayerChoice { get; }
}
