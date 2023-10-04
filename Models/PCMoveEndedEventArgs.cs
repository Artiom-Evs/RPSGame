namespace RPSGame.Models;

internal class PCMoveEndedEventArgs : EventArgs
{
    public PCMoveEndedEventArgs(string moveSignature)
    {
        ChoiceSignature = moveSignature;
    }

    public string ChoiceSignature { get; }
}
