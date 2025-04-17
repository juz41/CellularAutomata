using CellularAutomata.Cells;

namespace CellularAutomata.Boards;

public class HexagonalBoard<T> : IBoard<T> where T : Enum, IConvertible
{
    public void UpdateBoard()
    {
        throw new NotImplementedException();
    }

    public int[] AliveNeighbors(int i, int j)
    {
        throw new NotImplementedException();
    }

    public void MoveRound()
    {
        throw new NotImplementedException();
    }

    public int Width { get; }
    public int Height { get; }

    public Cell<T> this[int i, int j] => throw new NotImplementedException();
}