namespace SnakeGame.Core;

public abstract class KeyHandler
{
    protected abstract ConsoleKey Key { get; set; }

    public abstract void Run(Game game);

    public bool ListensFor(ConsoleKey key)
    {
        return Key == key;
    }
}