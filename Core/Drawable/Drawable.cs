namespace SnakeGame.Core;

public abstract class BaseDrawable : IDrawable
{
    public abstract void Draw(IDisplay display);

}

public abstract class Drawable(Position position) : BaseDrawable
{
    public Position Position { get; set; } = position;
    public abstract object Element { get; }

    public override void Draw(IDisplay display)
    {
        display.Draw(Render(), Position);
    }

    public virtual object Render()
    {
        return Element;
    }
}