namespace CellularAutomata;

public abstract class Cell
{
    public State Current;
    public State Next;

    public Cell(State current = State.Dead, State next = State.Dead)
    {
        this.Current = current;
        this.Next = next;
    }
    public abstract void UpdateCell(int alive);
    public void MoveStatus()
    {
        Current = Next;
    }

    public virtual char Icon()
    {
        if (Current == State.Alive) 
            return '\u2588';
        else 
            return ' ';
    }
}