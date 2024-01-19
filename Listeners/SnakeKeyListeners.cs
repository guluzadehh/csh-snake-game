using SnakeGame.Core;

namespace SnakeGame;

public class SnakeUpArrowKeyHandler : KeyHandler
{
    protected override ConsoleKey Key { get; set; } = ConsoleKey.UpArrow;

    public override void Run(Game game)
    {
        game.Snake.Direction = Direction.UP;
    }
}
public class SnakeDownArrowKeyHandler : KeyHandler
{
    protected override ConsoleKey Key { get; set; } = ConsoleKey.DownArrow;

    public override void Run(Game game)
    {
        game.Snake.Direction = Direction.DOWN;
    }
}
public class SnakeLeftArrowKeyHandler : KeyHandler
{
    protected override ConsoleKey Key { get; set; } = ConsoleKey.LeftArrow;

    public override void Run(Game game)
    {
        game.Snake.Direction = Direction.LEFT;
    }
}
public class SnakeRightArrowKeyHandler : KeyHandler
{
    protected override ConsoleKey Key { get; set; } = ConsoleKey.RightArrow;

    public override void Run(Game game)
    {
        game.Snake.Direction = Direction.RIGHT;
    }
}

public class SnakeSpaceKeyHandler : KeyHandler
{
    protected override ConsoleKey Key { get; set; } = ConsoleKey.Spacebar;

    private bool _toggle = true;

    public override void Run(Game game)
    {
        game.Snake.Speed = _toggle ? 2.0f : Snake.DEFAULT_SPEED;
        _toggle = !_toggle;
    }
}