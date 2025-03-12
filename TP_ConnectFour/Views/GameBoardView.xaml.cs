using TP_ConnectFour.ViewModels;

namespace TP_ConnectFour.Views;

public partial class GameBoardView : ContentView
{
    public GameBoardView()
    {
        InitializeComponent();
        CreateButtons();
        CreateGameBoard();
    }

    private void CreateButtons()
    {
        // Buttons to insert token
        for (int i = 0; i < 7; i++)
        {
            var button = new Button();
            button.Text = "↓";
            button.Command = ((ConnectFourGameViewModel)BindingContext).DropTokenCommand;
            button.CommandParameter = i;
            ButtonStackLayout.Children.Add(button);
        }
    }

    private void CreateGameBoard()
    {
        for (int row = 0; row < 6; row++)
        {
            GameBoardGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
            for (int col = 0; col < 7; col++)
            {
                if (row == 0)
                {
                    GameBoardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
                }
                var tokenView = new TokenView
                {
                    BindingContext = ((ConnectFourGameViewModel)BindingContext).Board[row][col]
                };
                
                // Set Grid.Row and Grid.Column properties to position the TokenView in the correct cell
                Grid.SetRow(tokenView, row);
                Grid.SetColumn(tokenView, col);
                
                GameBoardGrid.Children.Add(tokenView);
            }
        }
    }
}
