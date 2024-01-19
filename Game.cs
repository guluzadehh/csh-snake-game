using SnakeGame.Core;

namespace SnakeGame;

public class Game
{
    private readonly Position GamePosition = new(5, 3);
    private readonly List<IDrawable> _drawables = [];

    private List<KeyHandler> _keyEventHandlers = [];

    public Arena Arena { get; set; }
    public Snake Snake { get; set; }
    public Fruit Fruit { get; set; }
    public Score Score { get; set; }
    public Message Message { get; set; }

    public List<Position> EmptyPositions
    {
        get
        {
            List<Position> positions = [];

            for (int x = Arena.StartX + 1; x < Arena.EndX; ++x)
            {
                for (int y = Arena.StartY + 1; y < Arena.EndY; ++y)
                {
                    Position position = new(x, y);

                    if ((Snake == null || !Snake.Intersects(position))
                         && (Fruit == null || !Fruit.Intersects(position)))
                    {
                        positions.Add(position);
                    }
                }
            }

            return positions;
        }
    }

    public Position? RandomPosition
    {
        get
        {
            Random random = new();
            int index = random.Next(0, EmptyPositions.Count);
            try
            {
                return EmptyPositions[index];
            }
            catch
            {
                return null;
            }
        }
    }

    public int Width { get; set; } = 31;
    public int Height { get; set; } = 15;

    public int MaxScore
    {
        get
        {
            return (Width - 2) * (Height - 2) - 1;
        }
    }

    public Game()
    {
        _keyEventHandlers = InitKeyHandlers();
    }

    public void InitDrawables()
    {
        Arena = new(Width, Height, GamePosition);
        _drawables.Add(Arena);

        Snake = new(Arena.Center);
        _drawables.Add(Snake);

        Fruit = new(RandomPosition);
        _drawables.Add(Fruit);

        Score = new(new Position(5, 1));
        _drawables.Add(Score);

        Message = new(new Position(25, 1));
        _drawables.Add(Message);

        Hint hint = new(new Position(5, Height + 5));
        _drawables.Add(hint);
    }

    public void Start()
    {
        InitDrawables();

        Console.Clear();

        while (true)
        {
            bool canContinue = FrameLogic();

            UpdateFrame();
            if (!canContinue) break;

            Thread.Sleep((int)(250 / Snake.Speed));
        }

        Console.ReadLine();
        Console.Clear();
    }

    private bool FrameLogic()
    {
        if (Snake.Collides(Arena) || Snake.Collides(Snake.Tails))
        {
            Message.Lose();
            return false;
        }

        if (Score.Value == MaxScore)
        {
            Message.Win();
            return false;
        }

        ListenForKeys();

        Snake.Move();

        if (Snake.Collides(Fruit))
        {
            Snake.Grow();
            Score++;

            Fruit.Position = RandomPosition;
        }

        return true;
    }

    private void UpdateFrame()
    {
        Console.Clear();
        foreach (IDrawable drawable in _drawables)
        {
            drawable.Draw();
        }
    }

    private void ListenForKeys()
    {
        if (Console.KeyAvailable)
        {
            ConsoleKey key = Console.ReadKey(true).Key;

            foreach (KeyHandler keyHandler in _keyEventHandlers)
            {
                if (keyHandler.ListensFor(key))
                {
                    keyHandler.Run(this);
                }
            }
        }
    }

    private List<KeyHandler> InitKeyHandlers()
    {
        return
        [
            new SnakeUpArrowKeyHandler(),
            new SnakeDownArrowKeyHandler(),
            new SnakeLeftArrowKeyHandler(),
            new SnakeRightArrowKeyHandler(),
            new SnakeSpaceKeyHandler(),
        ];
    }
}