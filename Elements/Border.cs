using SnakeGame.Core;

namespace SnakeGame;

public class Border(Position position) : Collidable(position)
{
    public override object Element { get; } = '*';
}