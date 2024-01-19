using SnakeGame.Core;

namespace SnakeGame;

public class Message(Position position) : Drawable(position)
{
    public override object Element { get; } = "Message: ";

    public string Data { get; set; } = "";

    public override object Display()
    {
        return base.Display() + Data;
    }
}
