namespace SnakeGame.Core;

public class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public static Position Hidden { get; } = new(-1, -1);

    public Position(int x, int y) => (X, Y) = (x, y);

    public bool Contacts(Position position)
    {
        return X == position.X && Y == position.Y;
    }

    public static Position operator +(Position a, Position b)
    {
        return new Position(a.X + b.X, a.Y + b.Y);
    }

    public static Position operator -(Position a, Position b)
    {
        return new Position(a.X - b.X, a.Y - b.Y);
    }
}