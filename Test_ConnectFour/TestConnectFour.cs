using TP_ConnectFour.Game;

namespace Test_ConnectFour
{
    public class TestConnectFour
    {
        [Fact]
        public void TestInitializeBoard()
        {
            // Arrange
            var game = new ConnectFourGame();

            // Act

            // Assert
            Assert.NotNull(game.Board);
            Assert.Equal(6, game.Board.Count()); // 6 rows
            Assert.Equal(7, game.Board[0].Count()); // 7 columns
        }

        [Fact]
        public void TestAddToken()
        {
            // Arrange
            var game = new ConnectFourGame();

            // Act
            game.AddToken(0, 'R'); // Add red token to column 0

            // Assert
            Assert.Equal<char>('R', game.Board[5][0].Color); // Token should be at the bottom of the column
        }

        [Fact]
        public void TestAddTokenFullColumn()
        {
            // Arrange
            var game = new ConnectFourGame();
            // Act
            for (int i = 0; i < 6; i++)
            {
                game.AddToken(0, 'R');
            }
            // Assert
            Assert.False(game.AddToken(0, 'R')); // Column is full
        }

        [Fact]
        public void TestHorizontalWin()
        {
            // Arrange
            var game = new ConnectFourGame();

            // Act
            game.AddToken(0, 'R');
            game.AddToken(1, 'R');
            game.AddToken(2, 'R');
            game.AddToken(3, 'R');

            // Assert
            Assert.True(game.CheckWin('R'));
        }

        [Fact]
        public void TestVerticalWin()
        {
            // Arrange
            var game = new ConnectFourGame();

            // Act
            game.AddToken(0, 'R');
            game.AddToken(0, 'R');
            game.AddToken(0, 'R');
            game.AddToken(0, 'R');

            // Assert
            Assert.True(game.CheckWin('R'));
        }

        [Fact]
        public void TestDiagonalWin()
        {
            // Arrange
            var game = new ConnectFourGame();

            // Act
            game.AddToken(0, 'R');
            game.AddToken(1, 'Y');
            game.AddToken(1, 'R');
            game.AddToken(2, 'Y');
            game.AddToken(2, 'Y');
            game.AddToken(2, 'R');
            game.AddToken(3, 'Y');
            game.AddToken(3, 'Y');
            game.AddToken(3, 'Y');
            game.AddToken(3, 'R');

            // Assert
            Assert.True(game.CheckWin('R'));
        }

        [Fact]
        public void TestDraw()
        {
            // Arrange
            var game = new ConnectFourGame();

            // Act
            for (int col = 0; col < 7; col++)
            {
                for (int row = 0; row < 6; row++)
                {
                    game.AddToken(col, ((row*col) % 2 == 0) ? 'R' : 'Y');
                }
            }
            // Assert
            Assert.True(game.IsDraw());
        }
    }
}
