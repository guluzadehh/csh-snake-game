namespace SnakeGame.Core;

public interface IDrawable
{
    void Draw();
}

public abstract class Drawable(Position position) : IDrawable
{
    public Position? Position { get; set; } = position;
    public abstract object Element { get; }

    public void Draw()
    {
        if (Position == null) return;

        Console.SetCursorPosition(Position.X, Position.Y);
        Console.Write(Display());
    }

    public virtual object Display()
    {
        return Element;
    }
}