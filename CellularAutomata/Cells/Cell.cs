namespace CellularAutomata.Cells;

public class Cell<T> where T : Enum, IConvertible
{
    public T Current;
    public T Next;
    private readonly List<Func<Neighborhood<Cell<T>, T>,bool>>[,]? _rules;
    private readonly int _n = Enum.GetNames(typeof(T)).Length;
    public Cell(List<Func<Neighborhood<Cell<T>, T>,bool>>[,]? rules, int state=0)
    {
        this.Current = (T)(object)(state % _n);
        this.Next = (T)(object)(0);
        this._rules = rules;
    }
    public virtual void UpdateCell(Neighborhood<Cell<T>, T> nei)
    {
        int curr = (int)(object) Current;
        Next = Current;
        for (var i = 0; i < _n; i++)
        {
            foreach (var rule in _rules[curr, i])
            {
                if (rule(nei) == true)
                {
                    this.Next = (T)(object)(i);
                    return;
                }
            }
        }
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