namespace CellularAutomata;

public class ClassicCell : Cell
{
    public ClassicCell(State current = State.Dead, State next = State.Dead) : base(current, next)
    {
        
    }

    public override void UpdateCell(int alive)
    {
        if (Current == State.Alive)
        {
            if (alive is < 2 or > 3) 
                Next = State.Dead;
            else 
                Next = State.Alive;
        }
        else
        {
            if (alive is 3)
                Next = State.Alive;
            else
                Next = State.Dead;
        }
    }
}