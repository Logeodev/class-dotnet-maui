using CSharpFunctionalExtensions;
using TicTacToeCLI.Boards;
using TicTacToeCLI.Players;

namespace TicTacToe;

public class RandomPlayer : Player
{
    public override char Icon { get; }

    public RandomPlayer(char icon)
    {
        this.Icon = icon;
    }

    public override Result<PlayerMove> GetNextMove()
        => PlayerMove.Random;

}