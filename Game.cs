using SnakeGame.Core;

namespace SnakeGame;

public class Game
{
    private readonly Position GamePosition = new(5, 3);
    public List<IDrawable> Drawables { get; } = [];

    private readonly List<KeyHandler> _keyEventHandlers = [];

    public bool Stop { get; set; } = false;
    private IDisplay _display;

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

    public Position RandomPosition
    {
        get
        {
            Random random = new();
            int index = random.Next(0, EmptyPositions.Count);
            if (index > EmptyPositions.Count() - 1)
            {
                return Position.Hidden;
            }
            else
            {
                return EmptyPositions[index];
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

    public Game(IDisplay display)
    {
        _display = display;
        _keyEventHandlers = InitKeyHandlers();
    }

    public void InitDrawables()
    {
        Arena = new(Width, Height, GamePosition);
        Drawables.Add(Arena);

        Snake = new(Arena.Center);
        Drawables.Add(Snake);

        Fruit = new(RandomPosition);
        Drawables.Add(Fruit);

        Score = new(new Position(5, 1));
        Drawables.Add(Score);

        Message = new(new Position(25, 1));
        Drawables.Add(Message);

        Hint hint = new(new Position(5, Height + 5));
        Drawables.Add(hint);
    }

    public void Start()
    {
        InitDrawables();

        Console.Clear();

        while (!Stop)
        {
            FrameLogic();
            UpdateFrame();
            Thread.Sleep((int)(250 / Snake.Speed));
        }

        Message message = new(new Position(5, Height + 7))
        {
            Data = "Press Enter to quit..."
        };
        message.Draw(_display);

        Console.ReadLine();
        Console.Clear();
    }

    private void FrameLogic()
    {
        if (Snake.Collides(Arena) || Snake.Collides(Snake.Tails))
        {
            Message.Data = "You lose!";
            Stop = true;
            return;
        }

        if (Score.Value == MaxScore)
        {
            Message.Data = "You win!";
            Stop = true;
            return;
        }

        ListenForKeys();

        Snake.Move();

        if (Snake.Collides(Fruit))
        {
            Snake.Grow();
            Score++;

            Fruit.Position = RandomPosition;
        }
    }

    private void UpdateFrame()
    {
        Console.Clear();
        foreach (IDrawable drawable in Drawables)
        {
            drawable.Draw(_display);
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
            new SnakeEscKeyHandler(),
        ];
    }
}