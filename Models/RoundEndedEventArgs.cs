namespace RPSGame.Models;

internal class RoundEndedEventArgs : EventArgs
{
    public RoundEndedEventArgs(RoundResults result, string pCSecretKey, string pCMoveChoice)
    {
        Result = result;
        PCSecretKey = pCSecretKey;
        PCMoveChoice = pCMoveChoice;
    }

    public RoundResults Result { get; }
    public string PCSecretKey { get; }
    public string PCMoveChoice { get; }
}
