using CSharpFunctionalExtensions;
using FluentAssertions;
using TicTacToeCLI.Players;
using TicTacToeCLI.Boards;

namespace TestTTTcli
{
    public class RandomPlayerTest
    {
        [Fact]
        public async void GetNextMove_ReturnsValidMove()
        {
            // arrange
            RandomPlayer rndPlayer = new RandomPlayer('X');
            // act
            Result<PlayerMove> actualMove = await rndPlayer.GetNextMove();
            // assert
            actualMove.Value.Column.Should().BeInRange(1, 3);
            actualMove.Value.Row.Should().BeInRange(1, 3);
        }
    }
}
