using SnakeGame.Core;

namespace SnakeGame;

public class Score(Position position) : Drawable(position)
{
    public override object Element { get; } = "Score: ";

    public int Value { get; set; } = 0;

    public override object Display()
    {
        return base.Display() + Value.ToString();
    }

    public static Score operator ++(Score score)
    {
        score.Value++;
        return score;
    }
}