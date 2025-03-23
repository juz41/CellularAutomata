namespace CellularAutomata;

public class WallCell : Cell
{
    public bool IsAlive = false;
    public bool IsDead => !IsAlive;
    public bool IsAliveNext = false;
    public bool IsDeadNext => !IsAliveNext;

    public WallCell()
    {
        IsAlive = false;
        IsAliveNext = false;
    }
    public void UpdateCell(int alive)
    {
        return;
    }
    public override string ToString()
    {
        return "\u2588";
    }
}