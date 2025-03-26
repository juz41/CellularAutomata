namespace CellularAutomata;

public interface IBoard
{
    public void UpdateBoard();
    public int[] AliveNeighbors(int i, int j);
    public void ShowBoard();
}