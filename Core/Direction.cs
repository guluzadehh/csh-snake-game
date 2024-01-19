namespace SnakeGame.Core;

public enum Direction
{
    UP, DOWN, RIGHT, LEFT
}

public static class DirectionHandler
{
    public static bool AreOpposite(Direction d1, Direction d2)
    {
        return d1 == OppositeOf(d2);
    }

    public static Direction OppositeOf(Direction direction)
    {
        return direction switch
        {
            Direction.UP => Direction.DOWN,
            Direction.DOWN => Direction.UP,
            Direction.RIGHT => Direction.LEFT,
            Direction.LEFT => Direction.RIGHT,
            _ => throw new Exception(),
        };
    }
}
