using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TP_ConnectFour.Game;

namespace TP_ConnectFour.ViewModels
{
    public partial class ConnectFourGameViewModel : ObservableObject
    {
        private ConnectFourGame _game;
        
        [ObservableProperty]
        private ObservableCollection<ObservableCollection<TokenViewModel>> _board;
        
        [ObservableProperty]
        private char _currentPlayer = 'Y';  // Yellow starts by default
        
        [ObservableProperty]
        private bool _isGameOver;
        
        [ObservableProperty]
        private string _gameStatus = "Yellow's turn";

        public ConnectFourGameViewModel()
        {
            _game = new ConnectFourGame();
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            Board = new ObservableCollection<ObservableCollection<TokenViewModel>>();
            
            for (int i = 0; i < _game.Board.Count; i++)
            {
                var row = new ObservableCollection<TokenViewModel>();
                for (int j = 0; j < _game.Board[i].Count; j++)
                {
                    row.Add(new TokenViewModel(_game.Board[i][j]));
                }
                Board.Add(row);
            }
        }

        [RelayCommand]
        public void DropToken(int column)
        {
            if (IsGameOver || column < 0 || column >= _game.Board[0].Count)
                return;

            if (_game.AddToken(column, CurrentPlayer))
            {
                // Update the view model board
                UpdateBoardFromGame();
                
                // Check for win
                if (_game.CheckWin(CurrentPlayer))
                {
                    GameStatus = $"{(CurrentPlayer == 'Y' ? "Yellow" : "Red")} wins!";
                    IsGameOver = true;
                    return;
                }
                
                // Check for draw
                if (_game.IsDraw())
                {
                    GameStatus = "Game ends in a draw!";
                    IsGameOver = true;
                    return;
                }
                
                // Switch player
                CurrentPlayer = CurrentPlayer == 'Y' ? 'R' : 'Y';
                GameStatus = $"{(CurrentPlayer == 'Y' ? "Yellow" : "Red")}'s turn";
            }
        }
        
        private void UpdateBoardFromGame()
        {
            for (int i = 0; i < _game.Board.Count; i++)
            {
                for (int j = 0; j < _game.Board[i].Count; j++)
                {
                    Board[i][j].Color = _game.Board[i][j].Color;
                }
            }
        }

        [RelayCommand]
        public void RestartGame()
        {
            _game = new ConnectFourGame();
            InitializeBoard();
            CurrentPlayer = 'Y';
            IsGameOver = false;
            GameStatus = "Yellow's turn";
        }
    }
}
