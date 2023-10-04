
using RPSGame.Models;

namespace RPSGame.Infrastructure;

/// <summary>
/// Implements text user interface.
/// </summary>
internal static class TUI
{
    internal static void PrintZeroArgumentsError() =>
        Console.WriteLine("The game can't start without arguments.\nBye!");

    internal static void PrintNumberOfArgumentsLessThenThreeError() =>
        Console.WriteLine("The number of start arguments cannot be less then 3.\nBye!");

    internal static void PrintNumberOfArgumentsIsNotOddError() =>
        Console.WriteLine("The number of start arguments must be odd.\nBye!");


    internal static void PrintGameStartMessage()
    {
        Console.WriteLine("Welcome to Rock-Paper-Scissors like game!\n");
    }

    internal static void PrintGameEndMessage(GameEndedEventArgs e)
    {
        Console.WriteLine("Bye!");
    }

    internal static void PrintRoundStartMessage(RoundStartedEventArgs e)
    {
        Console.WriteLine($"Round #{e.RoundNumber}\n");
    }

    internal static void PrintRoundEndMessage(RoundEndedEventArgs e)
    {
        Console.WriteLine($"PC's choice: {e.PCMoveChoice}" +
                          $"\nHMAC key: {e.PCSecretKey}\n");

        switch (e.Result)
        {
            case RoundResults.Draw:
                Console.WriteLine("Draw! Game continues!\n");
                break;
            case RoundResults.PCWin:
                Console.WriteLine("PC won!");
                break;
            case RoundResults.PlayerWin:
                Console.WriteLine("You won!");
                break;
            case RoundResults.PlayerCapitulate:
                Console.WriteLine("You are capitulate! PC won.");
                break;
            default:
                break;
        }
    }

    internal static void PrintPCStartMessage()
    {
        Console.WriteLine("PC makes his move...");
    }

    internal static void PrintPCEndMessage(PCMoveEndedEventArgs e)
    {
        Console.WriteLine($"PC has made his move.\nHMAC: {e.ChoiceSignature}\n");
    }

    internal static void PrintPlayerMoveStartMessage()
    {
        Console.WriteLine("Your move!");
    }

    internal static void PrintPlayerMoveEndMessage(PlayerMoveEndedEventArgs e)
    {
        Console.WriteLine($"Your choice: {e.PlayerChoice}\n");
    }

    internal static int PrintPlayerMoveSelectionDialog(string[] moveVariants)
    {
        Console.WriteLine("Available moves:");

        for (int i = 0; i <  moveVariants.Length; i++)
        {
            Console.WriteLine($"{i + 1} - {moveVariants[i]}");
        }

        Console.WriteLine($"{moveVariants.Length + 1} - Exit" +
                          $"\n? - Help");

        int choice = 0;

        do
        {
            Console.Write("Make your choice: ");
            string input = Console.ReadLine();

            if (input == "?")
            {
                TUI.PrintHelpMessage(moveVariants);
                choice = 0;
                continue;
            }
            else if (!int.TryParse(input, out choice) ||
                choice < 1 ||
                choice > moveVariants.Length + 1)
            {
                Console.WriteLine("You entered something wrong!");
                choice = 0;
            }
        } while (choice == 0);

        return  choice - 1;
    }

    private static void PrintHelpMessage(string[] moveVariants)
    {
        throw new NotImplementedException();
    }
}
