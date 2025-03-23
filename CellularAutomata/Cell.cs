namespace CellularAutomata;

public class Cell
{
    public bool IsAlive {get; set; }
    public bool IsDead => !IsAlive;
    public bool IsAliveNext { get; set; }
    public bool IsDeadNext => !IsAliveNext;

    public Cell()
    {
        IsAlive = false;
        IsAliveNext = false;
    }
    public Cell(bool _isAlive)
    {
        this.IsAlive = _isAlive;
    }
    public void UpdateCell(int alive)
    {
        if (IsAlive)
        {
            if (alive < 2 || alive > 3) 
                IsAliveNext = false;
            else 
                IsAliveNext = true;
        }
        else
        {
            if (alive == 3)
                IsAliveNext = true;
            IsAliveNext = false;
        }
    }

    public void MoveStatus()
    {
        IsAlive = IsAliveNext;
    }
    public override string ToString()
    {
        if (IsAlive) return "1";
        else return "0";
    }
}