namespace CellularAutomata;

public class Cell
{
    public bool IsAlive {get; set; }
    public bool IsDead => !IsAlive;
    public bool IsAliveNext { get; set; }

    public Cell()
    {
        IsAlive = false;
        IsAliveNext = false;
    }
    public Cell(bool isAlive)
    {
        this.IsAlive = isAlive;
    }
    public void UpdateCell(int alive)
    {
        if (IsAlive)
        {
            if (alive is < 2 or > 3) 
                IsAliveNext = false;
            else 
                IsAliveNext = true;
        }
        else
        {
            if (alive is 3)
                IsAliveNext = true;
            else
                IsAliveNext = false;
        }
    }
    public void MoveStatus()
    {
        IsAlive = IsAliveNext;
    }
    public override string ToString()
    {
        if (IsAlive) return "\u2588";
        else return " ";
    }
}