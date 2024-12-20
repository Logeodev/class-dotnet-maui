namespace ConnectFourTDD
{
    public class Cell
    {
        public int row { get; private set; }
        public int column { get; private set; }
        public char value { get; private set; }

        public Cell(int r, int c, char v)
        {
            row = r;
            column = c;
            value = v;
        }

        public void UpdateValue(char value)
        {
            this.value = value;
        }
    }
}