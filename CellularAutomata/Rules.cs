using CellularAutomata.Cells;
using CellularAutomata.States;

namespace CellularAutomata;

public class Rules<T> where T : Enum, IConvertible
{
    private List<(Func<Neighborhood<Cell<T>, T>, bool> func, T state)>[]? _rules;
    private int _n = Enum.GetNames(typeof(T)).Length;
    public Rules(List<(Func<Neighborhood<Cell<T>, T>, bool> func, T state)>[] rules)
    {
        _rules = rules;
    }
    public virtual T NextState(Cell<T> cell, Neighborhood<Cell<T>, T> nei)
    {
        int curr = (int)(object)cell.Current;
        cell.Next = cell.Current;
        for (var i = 0; i < _n; i++)
        {
            foreach (var rule in _rules[curr])
            {
                if (rule.func(nei) == true)
                {
                    return rule.state;
                }
            }
        }
        return cell.Current;
    }
}