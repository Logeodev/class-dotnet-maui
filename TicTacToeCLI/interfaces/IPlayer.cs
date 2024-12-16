using CSharpFunctionalExtensions;
using TicTacToe;

namespace TicTacToeCLI.interfaces
{
    internal interface IPlayer
    {
        public char icon { get; }

        public Result<PlayerMoves> GetNextMove();
    }
}
