using System;
using System.Data.Common;

namespace TicTacToeCLI;

internal class Program
{
    static void Main(string[] args)
    {
        new Game().RunGame();
    }
}
