namespace CellularAutomata.Cells;

public class Cell<T> where T : Enum, IConvertible
{
    public T Current;
    public T Next;
    private readonly (int a, int b)[,,] _rules;
    // if cell in state a is in range of <a,b> then it stays goes up depending on second index of _rules
    // goes down otherwise
    // (a,b)[i,j,k] -> a,b is range (inclusive)
    // i is current state, j is stay/up, last one is all states to match
    private readonly int _n = Enum.GetNames(typeof(T)).Length;
    public Cell((int a, int b)[,,] rules, int state=0)
    {
        this.Current = (T)(object)(state % _n);
        this.Next = (T)(object)(0);
        this._rules = rules;
    }
    public virtual void UpdateCell(Neighborhood<Cell<T>, T> nei)
    {
        (int a, int b) stayRules, upRules; 
        int curr = (int)(object) Current;
        
        bool stay = true;
        for (int i = 0; i < _n; i++)
        {
            stayRules = _rules[curr, 0, i];
            if (!(nei[i] >= stayRules.a && nei[i] <= stayRules.b))
            {
                stay = false;
                break;
            }
        }
        if (stay)
        {
            return;
        }
        
        bool up = true;
        for (int i = 0; i < _n; i++)
        {
            upRules = _rules[curr, 1, i];
            if (!(nei[i] >= upRules.a && nei[i] <= upRules.b))
            {
                up = false;
                break;
            }
        }
        if (up)
        {
            this.Next = (T)(object)(((int)(object)(Current)+1)%_n);
            return;
        }

        if ((int)(object)(Current) - 1 < 0) return;
        this.Next = (T)(object)((int)(object)(Current)-1);
    }
    public virtual int Icon()
    {
        return (int)(object)(Current);
    }
    public void MoveStatus()
    {
        Current = Next;
    }
}