using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToeCLI
{
    internal class Game
    {
        const char PlayerOne = 'O';
        const char PlayerTwo = 'X';

        public List<Cell> grid = new List<Cell>()
        {
            Cell.EmptyCell(1, 1),
            Cell.EmptyCell(1, 2),
            Cell.EmptyCell(1, 3),
            Cell.EmptyCell(2, 1),
            Cell.EmptyCell(2, 2),
            Cell.EmptyCell(2, 3),
            Cell.EmptyCell(3, 1),
            Cell.EmptyCell(3, 2),
            Cell.EmptyCell(3, 3),
        };

        char currentPlayer = PlayerOne;
        public void RunGame()
        {
            DisplayGameBoard();

            while (true)
            {
                string? playerInput = AskCurrentPlayerForInput();

                if (playerInput == "q") break;

                string[]? splittedInput = playerInput?.Split(' ');

                int targetRow = ValidCellNumber(splittedInput?[0]);
                int targetColumn = ValidCellNumber(splittedInput?[1]);

                TryPlay(targetRow, targetColumn);

                Console.Clear();
                DisplayGameBoard();

                string? endMessage = IsEndGame();
                if (endMessage != null)
                {
                    Console.WriteLine($"{endMessage}");
                    break;
                }

                currentPlayer = currentPlayer == PlayerOne ? PlayerTwo : PlayerOne;
            }
        }

        private string? AskCurrentPlayerForInput()
        {
            Console.WriteLine($"Player {currentPlayer} - Enter row (1-3) and column (1-3), separated by a space, or 'q' to quit...");
            string? input = Console.ReadLine();

            if (string.Compare(input, "q", StringComparison.OrdinalIgnoreCase) == 0)
                return "q";
            return input;
        }

        private void TryPlay(int targetRow, int targetColumn)
        {
            bool movePlayedSuccessfully = PlayOnGameBoard(grid, targetRow, targetColumn, currentPlayer);

            if (movePlayedSuccessfully is false)
            {
                Console.WriteLine("Invalid move");
            }
        }

        private void DisplayGameBoard()
        {
            Console.WriteLine(new string('=', Console.WindowWidth));
            Console.WriteLine(".NET MAUI".PadLeft(Console.WindowWidth / 2));
            Console.WriteLine(new string('=', Console.WindowWidth));

            Console.WriteLine($"|-----------------|");
            DisplayGameBoardLine(GetCellContent(grid, 1, 1), GetCellContent(grid, 1, 2), GetCellContent(grid, 1, 3));
            Console.WriteLine($"|-----|-----|-----|");
            DisplayGameBoardLine(GetCellContent(grid, 2, 1), GetCellContent(grid, 2, 2), GetCellContent(grid, 2, 3));
            Console.WriteLine($"|-----|-----|-----|");
            DisplayGameBoardLine(GetCellContent(grid, 3, 1), GetCellContent(grid, 3, 2), GetCellContent(grid, 3, 3));
            Console.WriteLine($"|-----------------|");
        }

        private Cell? GetCell(List<Cell> grid, int row, int column)
            => grid
                .Where(cell => cell.Row == row)
                .Where(cell => cell.Column == column)
                .FirstOrDefault();

        private char GetCellContent(List<Cell> grid, int row, int column)
            => GetCell(grid, row, column)?.Value ?? ' ';

        private void DisplayGameBoardLine(char leftCell, char middleCell, char rightCell)
        {
            Console.WriteLine($"|  {leftCell}  |  {middleCell}  |  {rightCell}  |");
        }

        private bool PlayOnGameBoard(List<Cell> grid, int targetRow, int targetColumn, char currentPlayer)
        {
            Cell? cell = GetCell(grid, targetRow, targetColumn);

            if (cell == null || cell.Value == PlayerOne || cell.Value == PlayerTwo)
                return false;

            cell.UpdateValue(currentPlayer);
            return true;
        }

        public bool IsGameBoardWin(List<Cell> grid)
        {
            var testWin = false;

            IEnumerable<IGrouping<int, Cell>> rows = grid
                .GroupBy(cell => cell.Row);

            IEnumerable<IGrouping<int, Cell>> columns = grid
                .GroupBy(cell => cell.Column);

            testWin = TestWinInGroup(rows);
            testWin = TestWinInGroup(columns);

            IEnumerable<Cell> firstDiagonal = grid.Where(c => c.Row == c.Column);
            IEnumerable<Cell> secondDiagonal = grid.Where(c => (c.Row + c.Column) == 4);

            var diagonals = new List<IEnumerable<Cell>>
            {
                firstDiagonal,
                secondDiagonal
            };
            
            if (diagonals.Any(diagonals =>
                            diagonals.All(cell => cell.Value == PlayerOne) ||
                            diagonals.All(cell => cell.Value == PlayerTwo)))
            {
                testWin = true;
            }

            return testWin;
        }

        private bool TestWinInGroup(IEnumerable<IGrouping<int, Cell>> group)
        {
            if (group.Any(g =>
                g.All(cell => cell.Value == PlayerOne) ||
                g.All(cell => cell.Value == PlayerTwo)))
            {
                return true;
            }
            return false;
        }

        private bool IsGameBoardFull(List<Cell> grid)
            => grid.All(cell => cell.Value.HasValue);

        private int ValidCellNumber(string input)
        {
            if (int.TryParse(input, out int target) is false ||
                    target < 1 || target > 3)
            {
                Console.WriteLine("Invalid target cell, must be betwen 1 and 3");
            }
            return target;

        }

        private string? IsEndGame()
        {
            if (IsGameBoardWin(grid))
            {
                return $"Player {currentPlayer} has won the game !!!!";
            }
            if (IsGameBoardFull(grid))
            {
                return $"It's a draw";
            }
            return null;
        }
    }
}