namespace TicTacToeCLI.Display;

public interface IDisplay
{
    int Width { get; }

    void WriteLine(object obj);
    void WriteLine(string message);
    void Clear();
}

public class ConsoleDisplay : IDisplay
{
    public int Width => Console.WindowWidth;

    public void Clear()
    {
        Console.Clear();
    }

    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    public void WriteLine(object obj)
    {
        Console.WriteLine(obj);
    }
}

public class DebugDisplay : IDisplay
{
    public int Width => 0;

    public void Clear()
    {
    }

    public void WriteLine(string message)
    {
        System.Diagnostics.Debug.WriteLine(message);
    }

    public void WriteLine(object obj)
    {
        System.Diagnostics.Debug.WriteLine(obj);
    }
}