using CSharpFunctionalExtensions;
using TicTacToeCLI.Boards;

namespace TicTacToeCLI.Players;

public interface IPlayer
{
    public Result<PlayerMove> GetNextMove();
    public char Icon { get; }
}


public abstract class Player : IPlayer
{
    public abstract char Icon { get; }

    public abstract Result<PlayerMove> GetNextMove();

    public override string ToString()
        => $"{this.Icon}";
}
