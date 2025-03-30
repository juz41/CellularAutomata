namespace CellularAutomata;

public class Cell<T> where T : Enum, IConvertible
{
    public T Current;
    public T Next;
    private int[,,] _rules;
    private int n = Enum.GetNames(typeof(T)).Length;
    
    //  B3/S23. A cell is born if it has exactly three neighbours, survives if it has two or three living neighbours, and dies otherwise
    //  Smooth states - one in which state may only change by one - up or down
    //  string then should have 2*states fields - 2 for edge cases
    //  first field is how to move up, then there are 2 foreach middle state and last one means how to get down
    //  in case of 2 fields this shall simplify to normal rule string without unnecessary chars
    //  B3/S23 = 3/23
    public Cell(int [,,] rules, int state=0)
    {
        this.Current = (T)(object)(state % n);
        this.Next = (T)(object)(0);
        this._rules = rules;
    }
    public virtual void UpdateCell(int[] states)
    {
        int[] stayRules = new int[n];
        int[] upRules = new int[n];
        int curr = (int)(object) Current;
        for (int i = 0; i < n; i++)
        {
            stayRules[i] = _rules[curr, i, 0];
            upRules[i] = _rules[curr, i, 1];
        }

        bool flag = true;
        for (int i = 0; i < n; i++)
        {
            if (stayRules[i] != -1 && )
        }
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