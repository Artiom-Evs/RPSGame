using Spectre.Console;
using System.Linq;
using RPSGame.Models;
using Spectre.Console.Rendering;

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

    internal static void PrintArgumentsDuplicatedError() =>
        Console.WriteLine("Arguments cannot be duplicated.\nBye!");

    internal static void PrintGameStartMessage() =>
        AnsiConsole.Write(new Markup("[Yellow]Welcome to Rock-Paper-Scissors like game![/]\n\n"));
    
    internal static void PrintGameEndMessage(GameEndedEventArgs e) => 
        AnsiConsole.Write(new Markup("\n[Pink3]Bye![/]\n"));
    
    internal static void PrintRoundStartMessage(RoundStartedEventArgs e) =>
        AnsiConsole.Write(new Markup($"[Yellow]Round #{e.RoundNumber}[/]\n\n"));
    
    internal static void PrintRoundEndMessage(RoundEndedEventArgs e)
    {
        AnsiConsole.Write(new Markup($"[Blue]PC[/]'s choice: {e.PCMoveChoice}" +
                                     $"\nHMAC key: {e.PCSecretKey}\n\n"));

        switch (e.Result)
        {
            case RoundResults.Draw:
                AnsiConsole.Write(new Markup("[Yellow]Draw! Game continues![/]\n\n"));
                break;
            case RoundResults.PCWin:
                AnsiConsole.Write(new Markup("[Blue]PC[/] won!\n"));
                break;
            case RoundResults.PlayerWin:
                AnsiConsole.Write(new Markup("[Green3]You[/] won!\n"));
                break;
            case RoundResults.PlayerCapitulate:
                AnsiConsole.Write(new Markup("[Grey]You are capitulate![/]\n\n[Blue]PC[/] won.\n"));
                break;
            default:
                break;
        }
    }

    internal static void PrintPCStartMessage() =>
        AnsiConsole.Write(new Markup("[Blue]PC[/] makes his move...\n"));
    
    internal static void PrintPCEndMessage(PCMoveEndedEventArgs e) =>
        AnsiConsole.Write(new Markup($"[Blue]PC[/] has made his move.\nHMAC: {e.ChoiceSignature}\n\n"));
    
    internal static void PrintPlayerMoveStartMessage() =>
        AnsiConsole.Write(new Markup("[Green]Player[/]'s move!\n"));
    
    internal static void PrintPlayerMoveEndMessage(PlayerMoveEndedEventArgs e) =>
        AnsiConsole.Write(new Markup($"[Green3]Your[/] choice: {e.PlayerChoice}\n\n"));
    
    internal static int PrintPlayerMoveSelectionDialog(string[] moveVariants)
    {
        string[] items = [ ..moveVariants, "Exit", "Help" ];
        int choice = -1;

        do
        {
            var selectedItem = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .HighlightStyle(new Style().Foreground(Color.Green3))
                    .Title("Make [Green]your[/] choice!")
                    .MoreChoicesText("[grey](Move up and down to reveal more variants)[/]")
                    .PageSize(10)
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

        table.AddColumn(new TableColumn(new Markup("[Blue]PC[/]\\[Green3]User[/]")));
        foreach (var item in moveVariants)
            table.AddColumn(new TableColumn(new Markup($"[Green3]{item}[/]")));

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

            items.Insert(0, $"[Blue]{item}[/]");

            table.AddRow(items.Select(t => new Markup(t)));
        }

        AnsiConsole.WriteLine();
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }

    private static int IndexOf<T>(this T[] items, T item) => 
        items.ToList().IndexOf(item);

}
