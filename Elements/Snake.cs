using System.Collections;
using SnakeGame.Core;

namespace SnakeGame;

public class SnakeDirection(Direction direction)
{
    private Direction _direction = direction;
    public Direction Direction
    {
        get
        {
            return _direction;
        }
        set
        {
            if (!DirectionHandler.AreOpposite(_direction, value))
            {
                _direction = value;
            }
        }
    }
}

public class Snake : CompoundCollidable, IDrawable
{
    private Stack<BodyPart> _bodyParts = [];
    private BodyPart LastBodyPart { get { return _bodyParts.Peek(); } }

    public Head Head { get; }
    public IEnumerable<BodyPart> Tails { get { return _bodyParts.Reverse().Skip(1); } }

    public const float DEFAULT_SPEED = 1.0f;
    public float Speed { get; set; } = DEFAULT_SPEED;

    private const Direction DEFAULT_DIRECTION = Direction.RIGHT;
    private SnakeDirection _snakeDirection = new SnakeDirection(DEFAULT_DIRECTION);
    public Direction Direction
    {
        get { return _snakeDirection.Direction; }
        set { _snakeDirection.Direction = value; }
    }


    public Snake(Position startPosition)
    {
        Head = new(startPosition, DEFAULT_DIRECTION);

        _bodyParts.Push(Head);
    }

    public void Move()
    {
        Direction frontBodyPartDirection = Direction;

        foreach (BodyPart bodyPart in _bodyParts.Reverse())
        {
            (bodyPart.Direction, frontBodyPartDirection) = (frontBodyPartDirection, bodyPart.Direction); // swap
            bodyPart.Move();
        }
    }

    public void Grow()
    {
        _bodyParts.Push(CreateTail());
    }

    private Tail CreateTail()
    {
        Position newPosition = new(LastBodyPart.Position.X, LastBodyPart.Position.Y);
        Tail tail = new(newPosition, LastBodyPart.Direction);

        Direction oppositeDirection = DirectionHandler.OppositeOf(LastBodyPart.Direction);
        tail.MoveAlong(oppositeDirection);

        return tail;
    }

    public void Draw()
    {
        foreach (BodyPart bodyPart in _bodyParts)
        {
            bodyPart.Draw();
        }
    }

    public override bool Collides(Collidable collidable)
    {
        return Head.Collides(collidable);
    }

    public override IEnumerator GetEnumerator()
    {
        return (IEnumerator)_bodyParts.GetEnumerator();
    }
}

public abstract class BodyPart(Position position, Direction direction) : Collidable(position)
{
    private SnakeDirection _snakeDirection = new SnakeDirection(direction);
    public Direction Direction
    {
        get { return _snakeDirection.Direction; }
        set { _snakeDirection.Direction = value; }
    }

    public void Move()
    {
        MoveAlong(Direction);
    }

    public void MoveAlong(Direction direction)
    {
        switch (direction)
        {
            case Direction.UP:
                MoveUp();
                break;

            case Direction.DOWN:
                MoveDown();
                break;

            case Direction.LEFT:
                MoveLeft();
                break;

            case Direction.RIGHT:
                MoveRight();
                break;
        }

    }

    protected void MoveUp()
    {
        MoveY(-1);
    }

    protected void MoveDown()
    {
        MoveY(1);
    }

    protected void MoveLeft()
    {
        MoveX(-1);
    }

    protected void MoveRight()
    {
        MoveX(1);
    }
}


public class Head(Position position, Direction direction) : BodyPart(position, direction)
{
    public override object Element { get; } = '+';
}

public class Tail(Position position, Direction direction) : BodyPart(position, direction)
{
    public override object Element { get; } = 'o';
}