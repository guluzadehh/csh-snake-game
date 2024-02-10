using System.Collections;

namespace SnakeGame.Core;


public abstract class Collidable(Position position) : Movable(position)
{
    public bool Collides(Collidable collidable)
    {
        return Intersects(collidable.Position);
    }

    public bool Intersects(Position position)
    {
        return Position.Contacts(position);
    }
}

public abstract class CompoundCollidable : BaseDrawable, IEnumerable
{
    public bool Collides(CompoundCollidable compoundCollidable)
    {
        foreach (Collidable collidable in compoundCollidable)
        {
            if (Collides(collidable))
            {
                return true;
            }
        }

        return false;
    }

    public bool Collides(IEnumerable<Collidable> collidables)
    {
        foreach (Collidable collidable in collidables)
        {
            if (Collides(collidable))
            {
                return true;
            }
        }

        return false;
    }

    public virtual bool Collides(Collidable collidable)
    {
        foreach (Collidable c in this)
        {
            if (c.Collides(collidable))
            {
                return true;
            }
        }

        return false;
    }

    public virtual bool Intersects(Position position)
    {
        foreach (Collidable c in this)
        {
            if (c.Intersects(position))
            {
                return true;
            }
        }

        return false;
    }

    public abstract IEnumerator GetEnumerator();
}