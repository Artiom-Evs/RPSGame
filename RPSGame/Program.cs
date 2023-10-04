using RPSGame.Infrastructure;
using RPSGame.Services;

namespace RPSGame;

internal static class Program
{
    static GameContext _game = default!;

    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            TUI.PrintZeroArgumentsError();
            return;
        }

        if (args.Length < 3)
        {
            TUI.PrintNumberOfArgumentsLessThenThreeError();
            return;
        }

        if (args.Length % 2 == 0)
        {
            TUI.PrintNumberOfArgumentsIsNotOddError();
            return;
        }

        if (new HashSet<string>(args).Count != args.Length)
        {
            TUI.PrintArgumentsDuplicatedError();
            return;
        }

        IKeyGenerator keyGenerator = new BaseKeyGenerator();
        IDataSigner dataSigner = new HMACSHA2DataSigner(keyGenerator);

        _game = new GameContext(dataSigner, args, PCMoveHandler, PlayerMoveHandler);

        _game.GameStarted += Game_GameStarted;
        _game.GameEnded += Game_GameEnded;
        _game.RoundStarted += Game_RoundStarted;
        _game.RoundEnded += Game_RoundEnded;
        _game.PCMoveStarted += Game_PCMoveStarted;
        _game.PCMoveEnded += Game_PCMoveEnded;
        _game.PlayerMoveStarted += Game_PlayerMoveStarted;
        _game.PlayerMoveEnded += Game_PlayerMoveEnded;

        _game.StartGame();
    }

    private static int PCMoveHandler(string[] moveVariants)
    {
        return Random.Shared.Next(moveVariants.Length);
    }

    private static int PlayerMoveHandler(string[] moveVariants)
    {
        return TUI.PrintPlayerMoveSelectionDialog(moveVariants);
    }

    private static void Game_GameStarted(object? sender, Models.GameStartedEventArgs e)
    {
        TUI.PrintGameStartMessage();
    }

    private static void Game_GameEnded(object? sender, Models.GameEndedEventArgs e)
    {
        TUI.PrintGameEndMessage(e);
    }

    private static void Game_RoundStarted(object? sender, Models.RoundStartedEventArgs e)
    {
        TUI.PrintRoundStartMessage(e);
    }

    private static void Game_RoundEnded(object? sender, Models.RoundEndedEventArgs e)
    {
        TUI.PrintRoundEndMessage(e);
    }

    private static void Game_PCMoveStarted(object? sender, Models.PCMoveStartedEventArgs e)
    {
        TUI.PrintPCStartMessage();
    }

    private static void Game_PCMoveEnded(object? sender, Models.PCMoveEndedEventArgs e)
    {
        TUI.PrintPCEndMessage(e);
    }

    private static void Game_PlayerMoveStarted(object? sender, Models.PlayerMoveStartedEventArgs e)
    {
        TUI.PrintPlayerMoveStartMessage();
    }

    private static void Game_PlayerMoveEnded(object? sender, Models.PlayerMoveEndedEventArgs e)
    {
        TUI.PrintPlayerMoveEndMessage(e);
    }
}
