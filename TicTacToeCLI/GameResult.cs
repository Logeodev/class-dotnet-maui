using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeCLI.Players;

namespace TicTacToeCLI
{
    public class GameResult
    {
        public static string Draw()
            => $"it's a draw!";

        public static string Win(IPlayer p)
            => $"Player {p} has won the game !!!!";
    }
}
