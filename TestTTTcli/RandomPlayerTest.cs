using CSharpFunctionalExtensions;
using FluentAssertions;
using TicTacToeCLI.Players;
using TicTacToeCLI.Boards;

namespace TestTTTcli
{
    public class RandomPlayerTest
    {
        [Fact]
        public void GetNextMove_ReturnsValidMove()
        {
            RandomPlayer rndPlayer = new RandomPlayer('X');

            Result<PlayerMove> actualMove = rndPlayer.GetNextMove();

            actualMove.Value.Column.Should().BeInRange(1, 3);
            actualMove.Value.Row.Should().BeInRange(1, 3);
        }
    }
}
