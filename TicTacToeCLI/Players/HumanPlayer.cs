using CSharpFunctionalExtensions;
using TicTacToeCLI.Boards;
using TicTacToeCLI.Display;

namespace TicTacToeCLI.Players;

public class HumanPlayer : Player
{
    public override char Icon { get; }
    private IDisplay display;

    public HumanPlayer(char icon, IDisplay display)
    {
        this.Icon = icon;
        this.display = display;
    }

    public HumanPlayer(char icon)
    {
        this.Icon = icon;
        this.display = new ConsoleDisplay();
    }

    public override Result<PlayerMove> GetNextMove()
    {
        display.WriteLine($"Player {Icon} - Enter row (1-3) and column (1-3), separated by a space");
        string? input = display.ReadLine();

        string[]? splittedInput = input?.Split(' ');

        if (int.TryParse(splittedInput?[0], out int targetRow) is false ||
            targetRow < 1 || targetRow > 3)
        {
            return Result.Failure<PlayerMove>("Invalid target cell row must be betwen 1 and 3");
        }

        if (int.TryParse(splittedInput?[1], out int targetColumn) is false ||
            targetColumn < 1 || targetColumn > 3)
        {
            return Result.Failure<PlayerMove>("Invalid target cell column must be betwen 1 and 3");
        }

        return Result.Success(new PlayerMove(targetRow, targetColumn));
    }
}