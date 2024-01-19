using SnakeGame.Core;

namespace SnakeGame;

public class Hint(Position position) : Drawable(position)
{
    public override object Element { get; } = "Hint: Press spacebar for boost";
}
