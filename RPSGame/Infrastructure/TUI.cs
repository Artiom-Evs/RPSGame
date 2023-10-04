using Spectre.Console;
using System.Linq;
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
        string[] items = [ ..moveVariants, "Exit", "Help" ];
        int choice = -1;

        do
        {
            var selectedItem = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Make your choice!")
                    .MoreChoicesText("[grey](Move up and down to reveal more variants)[/]")
                    .AddChoices(items));

            choice = items.IndexOf(selectedItem);

            if (choice == items.Length - 1)
            {
                TUI.PrintHelpMessage(moveVariants);
                choice = -1;
            }
        } while (choice == -1);

        return  choice;
    }

    private static void PrintHelpMessage(string[] moveVariants)
    {
        var table = new Table();
        table.Border = TableBorder.Ascii2;

        table.AddColumn("PC\\User");
        foreach (var item in moveVariants)
            table.AddColumn(item);

        foreach (var item in moveVariants)
        {
            List<string> items = moveVariants
                .Select(v => 
                    ChoiceComparer.Compare(moveVariants.IndexOf(item), moveVariants.IndexOf(v), moveVariants.Length))
                .Select(r => r switch {
                    RoundResults.PCWin => "Win",
                    RoundResults.PlayerWin => "Lose",
                    RoundResults.Draw => "Draw",
                    _ => throw new ArgumentException() })
                .ToList();

            items.Insert(0, item);

            table.AddRow(items.Select(t => new Markup(t)));
        }

        AnsiConsole.Write(table);
    }

    private static int IndexOf<T>(this T[] items, T item) => 
        items.ToList().IndexOf(item);

}
