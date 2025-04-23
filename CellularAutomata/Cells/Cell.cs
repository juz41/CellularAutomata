namespace CellularAutomata.Cells;

public class Cell<T> where T : Enum, IConvertible
{
    public T Current;
    public T Next;
    private readonly Rules<T> _rules;
    private readonly int _n = Enum.GetNames(typeof(T)).Length;

    public Cell(Rules<T> rules, int state = 0)
    {
        this.Current = (T)(object)(state % _n);
        this.Next = (T)(object)(0);
        this._rules = rules;
    }

    public virtual void UpdateCell(Neighborhood<Cell<T>, T> nei)
    {
        this.Next = _rules.NextState(this, nei);
    }

    public void CurrentUp()
    {
        Current = (T)(object)(((int)(object)Current + 1) % _n);
    }

    public void CurrentDown()
    {
        Current = (T)(object)(((int)(object)Current - 1) % _n);
    }

    public virtual int Icon()
    {
        return (int)(object)(Current);
    }

    public virtual void MoveStatus()
    {
        Current = Next;
    }
}