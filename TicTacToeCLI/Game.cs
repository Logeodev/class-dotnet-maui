
using CSharpFunctionalExtensions;
using TicTacToeCLI;
using TicTacToeCLI.interfaces;

namespace TicTacToe;

internal class Game
{
    public static char PlayerOneIcon = 'O';
    public static char PlayerTwoIcon = 'X';

    private readonly Board board;
    private readonly IPlayer player1;
    private readonly IPlayer player2;

    public IPlayer currentPlayer {  get; private set; }

    public Game()
    {
        this.board = new Board();
        this.player1 = new Player(PlayerOneIcon);
        this.player2 = new FakePlayer(PlayerTwoIcon);
    }

    public void Init()
    {
        this.board.Init();
        this.currentPlayer = this.player1;
    }

    public void Play()
    {
        this.board.DisplayGameBoardAndHeader();

        while (true)
        {
            Result<PlayerMoves> playerMoves = this.currentPlayer.GetNextMove();
            if (playerMoves.IsFailure)
            {
                Console.WriteLine(playerMoves.Error);
                continue;
            }

            bool movePlayedSuccessfully = this.board.PlayMoveOnBoard(playerMoves.Value, this.currentPlayer.icon);
            while (movePlayedSuccessfully is false)
            {
                Console.WriteLine("Invalid move");
                playerMoves = this.currentPlayer.GetNextMove();
                movePlayedSuccessfully = this.board.PlayMoveOnBoard(playerMoves.Value, this.currentPlayer.icon);
            }
            this.board.DisplayGameBoardAndHeader();

            Maybe<string> gameResult = this.board.IsGameOver(currentPlayer);
            if (gameResult.HasValue)
            {
                Console.WriteLine(gameResult.Value);
                break;
            }

            this.SwitchPlayer();
        }
    }

    private void SwitchPlayer()
    {
        if (this.currentPlayer == player1)
            this.currentPlayer = player2;
        else
            this.currentPlayer = player1;
    }

}
