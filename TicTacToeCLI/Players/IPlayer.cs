using CSharpFunctionalExtensions;
using TicTacToeCLI.Boards;

namespace TicTacToeCLI.Players;

public interface IPlayer
{
    public Task<Result<PlayerMove>> GetNextMove();
    public char Icon { get; }
}


public abstract class Player : IPlayer
{
    public abstract char Icon { get; }

    public abstract Task<Result<PlayerMove>> GetNextMove();

    public override string ToString()
        => $"{this.Icon}";
}
