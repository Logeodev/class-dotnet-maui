
using Microsoft.Extensions.Options;

namespace TP_ConnectFour.Game
{
    public class ConnectFourGame
    {
        public List<List<Token>> Board { get; private set; }

        public ConnectFourGame()
        {
            int nbr_rows = 6;
            int nbr_cols = 7;
            Board = new List<List<Token>>();
            for (var i = 0; i < nbr_rows; i++)
            {
                Board.Add(new List<Token>());
                for (var j = 0; j < nbr_cols; j++)
                {
                    Board.Last().Add(new Token());
                }
            }
        }

        public int ValidPositionInColumn(int col)
        {
            for (int row = Board.Count - 1; row >= 0; row--)
            {
                if (Board[row][col].Color == ' ')
                {
                    return row;
                }
            }
            return -1;
        }

        public bool AddToken(int column, char color)
        {
            var positionInColumn = ValidPositionInColumn(column);
            if (positionInColumn != -1)
            {
                Board[positionInColumn][column] = new Token(color);
                return true;
            }
            return false;
        }

        public bool CheckWin(char color)
        {
            int rows = Board.Count;
            int cols = Board[0].Count;
            return CheckHorizontalWin(color, rows, cols)
                || CheckVerticalWin(color, rows, cols)
                || CheckDiagonalWinBottomLeftToTopRight(color, rows, cols) 
                || CheckDiagonalWinTopLeftToBottomRight(color, rows, cols);
        }

        private bool CheckDiagonalWinTopLeftToBottomRight(char color, int rows, int cols)
        {
            // Check diagonal win (top-left to bottom-right)
            for (int row = 0; row < rows - 3; row++)
            {
                for (int col = 0; col < cols - 3; col++)
                {
                    if (Board[row][col].Color == color &&
                        Board[row + 1][col + 1].Color == color &&
                        Board[row + 2][col + 2].Color == color &&
                        Board[row + 3][col + 3].Color == color)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckDiagonalWinBottomLeftToTopRight(char color, int rows, int cols)
        {
            // Check diagonal win (bottom-left to top-right)
            for (int row = 3; row < rows; row++)
            {
                for (int col = 0; col < cols - 3; col++)
                {
                    if (Board[row][col].Color == color &&
                        Board[row - 1][col + 1].Color == color &&
                        Board[row - 2][col + 2].Color == color &&
                        Board[row - 3][col + 3].Color == color)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckVerticalWin(char color, int rows, int cols)
        {
            // Check vertical win
            for (int col = 0; col < cols; col++)
            {
                for (int row = 0; row < rows - 3; row++)
                {
                    if (Board[row][col].Color == color &&
                        Board[row + 1][col].Color == color &&
                        Board[row + 2][col].Color == color &&
                        Board[row + 3][col].Color == color)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckHorizontalWin(char color, int rows, int cols)
        {
            // Check horizontal win
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols - 3; col++)
                {
                    if (Board[row][col].Color == color &&
                        Board[row][col + 1].Color == color &&
                        Board[row][col + 2].Color == color &&
                        Board[row][col + 3].Color == color)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsDraw()
        {
            foreach (var row in Board)
            {
                foreach (var token in row)
                {
                    if (token.Color == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
