using System.Collections;
using SnakeGame.Core;

namespace SnakeGame;

public class Arena : CompoundCollidable, IDrawable
{
    private Position Offset { get; }

    private int Width { get; }
    private int Height { get; }

    public int StartX { get { return Offset.X; } }
    public int EndX { get { return Offset.X + Width; } }

    public int StartY { get { return Offset.Y; } }
    public int EndY { get { return Offset.Y + Height; } }

    public Position Center { get; }

    public List<Border> Borders { get; } = [];

    public Arena(int width, int height, Position? offset = null)
    {
        Offset = offset ?? new Position(0, 0);
        (Width, Height) = (width - 1, height - 1);
        Center = new Position(Offset.X + Width / 2, Offset.Y + Height / 2);

        CreateHorizontalBorders();
        CreateVerticalBorders();

    }

    public void Draw()
    {
        foreach (Border border in Borders)
        {
            border.Draw();
        }
    }

    private void CreateHorizontalBorders()
    {
        for (int x = 0; x <= Width; ++x)
        {

            Borders.Add(CreateTopBorder(x));
            Borders.Add(CreateBottomBorder(x));
        }
    }

    private void CreateVerticalBorders()
    {
        for (int y = 0; y <= Height; ++y)
        {
            Borders.Add(CreateLeftBorder(y));
            Borders.Add(CreateRightBorder(y));
        }
    }

    private Border CreateTopBorder(int x)
    {
        return new Border(new Position(x + StartX, 0 + Offset.Y));
    }

    private Border CreateBottomBorder(int x)
    {
        return new Border(new Position(x + StartX, EndY));
    }

    private Border CreateLeftBorder(int y)
    {
        return new Border(new Position(0 + StartX, y + StartY));
    }

    private Border CreateRightBorder(int y)
    {
        return new Border(new Position(EndX, y + StartY));
    }

    public override IEnumerator GetEnumerator()
    {
        return (IEnumerator)Borders.GetEnumerator();
    }
}