
using FluentAssertions;
using TicTacToeCLI;
using TicTacToeCLI.Boards;
using TicTacToeCLI.Display;
using TicTacToeCLI.Players;

namespace TestTTTcli
{
    enum GameResultInt
    {
        Draw = 0,
        Player1 = 1,
        Player2 = 2,
    }
    public class GameTests
    {
        [Fact]
        public async void Play_2RndPlayer_ReturnsValidGameResult()
        {
            // arrange
            IDisplay display = new DebugDisplay();
            RandomPlayer rPlayer1 = new RandomPlayer(PlayerConstants.PlayerOneIcon);
            RandomPlayer rPlayer2 = new RandomPlayer(PlayerConstants.PlayerTwoIcon);
            Game game = new Game(display, rPlayer1, rPlayer2);

            // act
            string actualGameResult = await game.Play();

            // assert
            actualGameResult.Should().BeOneOf([ GameResult.Draw(), GameResult.Win(rPlayer2), GameResult.Win(rPlayer1) ]);
        }

        [Theory]
        [InlineData('X', 'O', "2 2,3 3,1 2,2 1,1 3", "3 1,1 1,2 3,3 2", 0)]
        public async void Play_2Player_SpecifiedWins(
            char p1Icon, 
            char p2Icon,
            string p1MovesSet,
            string p2MovesSet,
            int expectedWinner
        ) {
            // arrange
            IDisplay display = new DebugDisplay();
            IPlayer p1 = new FakePlayer(p1Icon, ParseStringPlayerMoves(p1MovesSet));
            IPlayer p2 = new FakePlayer(p2Icon, ParseStringPlayerMoves(p2MovesSet));

            Game game = new Game(display, p1, p2);

            // act
            string actualGameResult = await game.Play();

            // assert
            actualGameResult.Should().BeOneOf([GameResult.Draw(), GameResult.Win(p1), GameResult.Win(p2)]);

            switch ((GameResultInt)expectedWinner)
            {
                case GameResultInt.Draw:
                    actualGameResult.Should().Be(GameResult.Draw());
                    break;
                case GameResultInt.Player1:
                    actualGameResult.Should().Be(GameResult.Win(p1));
                    break;
                case GameResultInt.Player2:
                    actualGameResult.Should().Be(GameResult.Win(p2));
                    break;
                default:
                    break;
            }
        }

        private List<PlayerMove> ParseStringPlayerMoves(string movesSetString)
        {
            return movesSetString.Split(',')
                .Select(x => splitMove(x))
                .ToList();
        }

        private PlayerMove splitMove(string move)
        {
            string[] m = move.Split(' ');
            return new PlayerMove(int.Parse(m[0]), int.Parse(m[1]));
        }
    }
}
