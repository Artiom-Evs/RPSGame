
using RPSGame.Models;

namespace RPSGame.Infrastructure;

internal static class ChoiceComparer
{
    public static RoundResults Compare(int pcChoice, int playerChoice, int choicesCount)
    {
        if (pcChoice == playerChoice)
        {
            return RoundResults.Draw;
        }
        else if (pcChoice < playerChoice && (playerChoice - pcChoice) / (choicesCount / 2) < 2)
        {
            return RoundResults.PCWin;
        }

        return RoundResults.PlayerWin;
    }
}
