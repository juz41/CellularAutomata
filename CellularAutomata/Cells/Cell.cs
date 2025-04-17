namespace CellularAutomata.Cells;

public class Cell<T> where T : Enum, IConvertible
{
    public T Current;
    public T Next;
    private readonly List<(int a, int b)>[,,] _rules;
    private readonly int _n = Enum.GetNames(typeof(T)).Length;
    public Cell(List<(int a, int b)>[,,] rules, int state=0)
    {
        this.Current = (T)(object)(state % _n);
        this.Next = (T)(object)(0);
        this._rules = rules;
    }
    public virtual void UpdateCell(Neighborhood<Cell<T>, T> nei)
    {
        int curr = (int)(object) Current;
        Next = Current;
        for (int i = 0; i < _n; i++)
        {
            var downRules = _rules[curr, 0, i];
            foreach (var rule in downRules)
            {
                if (nei[i] >= rule.a && nei[i] <= rule.b)
                {
                    if ((int)(object)(Current) - 1 < 0) return;
                    this.Next = (T)(object)((int)(object)(Current)-1);
                    return;
                }
            }
        }
        
        for (int i = 0; i < _n; i++)
        {
            var upRules = _rules[curr, 1, i];
            foreach (var rule in upRules)
            {
                if (nei[i] >= rule.a && nei[i] <= rule.b)
                {
                    if ((int)(object)(Current) + 1 >= _n) return;
                    this.Next = (T)(object)(((int)(object)(Current)+1)%_n);
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