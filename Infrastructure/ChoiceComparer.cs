
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
        // check distance between elements. 
        // if distance equal or less then half of the elements count
        else if (pcChoice < playerChoice && playerChoice - pcChoice <= choicesCount / 2)
        {
            return RoundResults.PCWin;
        }
        // if first element more then second, use inverted logic
        else if (pcChoice > playerChoice && pcChoice - playerChoice > choicesCount / 2)
        {
            return RoundResults.PCWin;
        }       

        return RoundResults.PlayerWin;
    }
}
