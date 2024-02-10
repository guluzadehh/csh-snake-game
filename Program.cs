namespace SnakeGame;

class SnakeGame
{
    public static void Main()
    {
        Game game = new(new Core.ConsoleDisplay());
        game.Start();
    }
}