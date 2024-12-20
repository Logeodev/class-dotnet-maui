using CSharpFunctionalExtensions;
using TicTacToeCLI.Boards;

namespace TicTacToeCLI.Players;

public class RandomPlayer : Player
{
    public override char Icon { get; }

    public RandomPlayer(char icon)
    {
        this.Icon = icon;
    }

    public async override Task<Result<PlayerMove>> GetNextMove()
        => await Task.Run(() => {
            Thread.Sleep(500);
            Console.Write(" .");
            Thread.Sleep(500);
            Console.Write(" . ");
            Thread.Sleep(500);
            Console.Write(". ");
            Thread.Sleep(500);
            return PlayerMove.Random;
        });

}