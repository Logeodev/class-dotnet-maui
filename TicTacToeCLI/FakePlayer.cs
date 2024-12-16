using CSharpFunctionalExtensions;
using TicTacToe;
using TicTacToeCLI.interfaces;

namespace TicTacToeCLI
{
    internal class FakePlayer : IPlayer
    {
        private char _icon;
        public char icon => _icon;

        public FakePlayer(char i)
        {
            _icon = i;
        }

        public Result<PlayerMoves> GetNextMove()
        {
            int targetRow = new Random().Next(1, 4);
            int targetColumn = new Random().Next(1, 4);

            return Result.Success(new PlayerMoves(targetRow, targetColumn));
        }
    }
}
