using SnakeGame.Core;

namespace SnakeGame;

public class Message(Position position) : Drawable(position)
{
    public override object Element { get; } = "Message: ";

    private string _message = "";

    public void Win()
    {
        _message = "You win!";
    }

    public void Lose()
    {
        _message = "You lose!";
    }

    public override object Display()
    {
        return base.Display() + _message;
    }
}
