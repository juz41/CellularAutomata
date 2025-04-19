namespace CellularAutomata.Cells;

public class WallCell<T> : Cell<T> where T : Enum, IConvertible
{
    public WallCell(List<Func<Neighborhood<Cell<T>, T>,bool>>[,]? rules = null, int state = 0) : base(rules, state)
    {
    }

    public override void UpdateCell(Neighborhood<Cell<T>, T> nei)
    {
        return;
    }
    public override void MoveStatus()
    {
        return;
    }
}