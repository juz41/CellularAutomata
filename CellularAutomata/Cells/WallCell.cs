namespace CellularAutomata.Cells;

public class WallCell<T> : Cell<T> where T : Enum, IConvertible
{
    public WallCell(string rule) : base(rule)
    {
        
    }

    public override void UpdateCell(int[] states)
    {
        return;
    }
}