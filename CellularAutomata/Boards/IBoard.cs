namespace CellularAutomata.Boards;

public interface IBoard<T> where T : Enum, IConvertible
{
    public void UpdateBoard();
    public int[] AliveNeighbors(int i, int j);
    public void MoveRound();
}