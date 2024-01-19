using SnakeGame.Core;

namespace SnakeGame;

public class Fruit(Position position) : Collidable(position)
{
    protected override bool Locked { get; } = true;
    public override object Element { get; } = '@';
}