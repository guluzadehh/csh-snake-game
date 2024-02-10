namespace SnakeGame.Core
{
    public class ConsoleDisplay : IDisplay
    {
        public void Draw(object Element, Position position)
        {
            if (position.X < 0 || position.Y < 0) return;

            Console.SetCursorPosition(position.X, position.Y);
            Console.Write(Element);
        }
    }
}