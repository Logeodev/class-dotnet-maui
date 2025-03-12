namespace TP_ConnectFour.Game
{
    public class Token
    {
        public char Color { get; private set; } = ' ';

        public Token() {}
        public Token(char color)
        {
            Color = color;
        }
    }
}