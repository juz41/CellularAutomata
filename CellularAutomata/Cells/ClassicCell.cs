namespace CellularAutomata;

public class ClassicCell : Cell
{
    public ClassicCell(State current = State.Dead, State next = State.Dead) : base(current, next)
    {
        
    }

    public override void UpdateCell(int[] states)
    {
        if (Current == State.Alive)
        {
            if (states[(int)State.Alive] is < 2 or > 3) 
                Next = State.Dead;
            else 
                Next = State.Alive;
        }
        else
        {
            if (states[(int)State.Alive] is 3)
                Next = State.Alive;
            else
                Next = State.Dead;
        }
    }
}