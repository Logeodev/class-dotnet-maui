namespace SampleMAUI;

public partial class TicTacToe : ContentPage
{
	private bool isXTurn = true;

	public TicTacToe()
	{
		InitializeComponent();
	}

    public void OnBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("///MainPage");
    }

	public async void OnButtonClicked(object sender, EventArgs e)
	{
		if (sender is Button b && string.IsNullOrEmpty(b.Text))
		{
			b.Text = this.isXTurn ? "X" : "O";
			this.isXTurn = !this.isXTurn;
			CheckForWinner();
		}
	}

	private void CheckForWinner()
	{
		string[,] board = new string[3, 3]
		{
			{ Button00.Text, Button01.Text, Button02.Text },
			{ Button10.Text, Button11.Text, Button12.Text },
			{ Button20.Text, Button21.Text, Button22.Text }
		};
		for (int i = 0; i < 3; i++)
		{
            if (!string.IsNullOrEmpty(board[i, 0]) && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
            {
                DisplayWinner(board[i, 0]);
                return;
            }
            if (!string.IsNullOrEmpty(board[0, i]) && board[0, i] == board[1, i] && board[1, i] == board[2, i])
            {
                DisplayWinner(board[0, i]);
                return;
            }
        }
        if (!string.IsNullOrEmpty(board[0, 0]) && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
        {
            DisplayWinner(board[0, 0]);
            return;
        }

        if (!string.IsNullOrEmpty(board[0, 2]) && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
        {
            DisplayWinner(board[0, 2]);
            return;
        }
    }

    private async void DisplayWinner(string winner)
    {
        await DisplayAlert("Winner", $"{winner} wins!", "OK");
        ResetBoard();
    }

    private void ResetBoard()
    {
        Button00.Text = Button01.Text = Button02.Text = string.Empty;
        Button10.Text = Button11.Text = Button12.Text = string.Empty;
        Button20.Text = Button21.Text = Button22.Text = string.Empty;
        isXTurn = true;
    }

    public async void ResetGame(object sender, EventArgs e)
    {
        ResetBoard();
    }
}