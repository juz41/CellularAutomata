using CellularAutomata.Cells;

namespace CellularAutomata.Boards;

public interface IBoard<T> where T : Enum, IConvertible
{
    public void UpdateBoard();
    public int[] AliveNeighbors(int i, int j);
    public void Clear();
    public void MoveRound();
    public int Width { get; }
    public int Height { get; }
    public Cell<T> this[int i, int j] {
        get;
    }
}