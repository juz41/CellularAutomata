namespace CellularAutomata;

public class WallClassicCell : Cell
{
    public WallClassicCell()
    {
        Current = State.Dead;
        Next = State.Dead;
    }
    public override void UpdateCell(int alive)
    {
        return;
    }
}