namespace CellularAutomata.Cells;

public class Cell<T> where T : Enum, IConvertible
{
    public T Current;
    public T Next;
    private readonly List<(Func<Neighborhood<Cell<T>, T>, bool> func, T state)>[]? _rules;
    private readonly int _n = Enum.GetNames(typeof(T)).Length;

    public Cell(List<(Func<Neighborhood<Cell<T>, T>, bool>, T)>[] rules, int state = 0)
    {
        this.Current = (T)(object)(state % _n);
        this.Next = (T)(object)(0);
        this._rules = rules;
    }

    public virtual void UpdateCell(Neighborhood<Cell<T>, T> nei)
    {
        int curr = (int)(object)Current;
        Next = Current;
        for (var i = 0; i < _n; i++)
        {
            foreach (var rule in _rules[curr])
            {
                if (rule.func(nei) == true)
                {
                    this.Next = (T)(object)(rule.state);
                    return;
                }
            }
        }
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