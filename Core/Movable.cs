namespace SnakeGame.Core;

public abstract class Movable(Position position) : Drawable(position)
{
    protected virtual bool Locked { get; } = false;

    public void MoveY(int y)
    {
        if (Locked) return;

        Position.Y += y;
    }

    public void MoveX(int x)
    {
        if (Locked) return;

        Position.X += x;
    }
}