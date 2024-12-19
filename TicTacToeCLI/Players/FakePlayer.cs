
using CSharpFunctionalExtensions;
using TicTacToeCLI.Boards;

namespace TicTacToeCLI.Players
{
    public class FakePlayer : Player
    {
        public override char Icon { get; }
        private List<PlayerMove> moves;
        private int currentMoveIndex = 0;
        public FakePlayer(char i, List<PlayerMove> movesSet)
        {
            this.Icon = i;
            this.moves = movesSet;
        }

        public override Result<PlayerMove> GetNextMove()
        {
            PlayerMove pm = new PlayerMove(moves[currentMoveIndex].Row, moves[currentMoveIndex].Column);
            currentMoveIndex = (currentMoveIndex + 1) % moves.Count();
            return Result.Success(pm);
        }
    }
}
