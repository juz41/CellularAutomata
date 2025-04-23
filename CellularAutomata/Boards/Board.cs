using CellularAutomata.Cells;
using CellularAutomata.States;

namespace CellularAutomata.Boards;
public class Board<T> : IBoard<T> where T : Enum, IConvertible
{
    public int Height { get; private set; }

    public Cell<T> this[int i, int j]
    {
        get => Cells[i,j];
    }

    public int Width { get; private set; }
    public readonly Cell<T>[,] Cells;
    private readonly int _n = Enum.GetNames(typeof(T)).Length;
    public event EventHandler EveryRound;
    public Board(int height, int width, Rules<T> rule)
    {
        this.Height = height;
        this.Width = width;
        Cells = new Cell<T>[Height+2, Width+2];
        Random rand = new Random();
        for (int i = 0; i < Height+2; i++)
        {
            Cells[i, 0] = new WallCell<T>();
            Cells[i, Width+1] = new WallCell<T>();
        }
        for (int j = 0; j < Width+2; j++)
        {
            Cells[0, j] = new WallCell<T>();
            Cells[Height+1, j] = new WallCell<T>();
        }
        
        for (int i = 1; i <= Height; i++)
        {
            for (int j = 1; j <= Width; j++)
            {
                Cells[i, j] = new Cell<T>(rule, rand.Next()%_n);
            }
        }
    }
    public void UpdateBoard()
    {
        Parallel.For(1, Height + 1, i =>
        {
            for (int j = 1; j <= Width; j++)
            {
                Cells[i, j].UpdateCell(new Neighborhood<Cell<T>, T>(AliveNeighbors(i, j)));
            }
        });
        Parallel.For(1, Height + 1, i =>
        {
            for (int j = 1; j <= Width; j++)
            {
                Cells[i, j].MoveStatus();
            }
        });
    }
    public int[] AliveNeighbors(int i, int j)
    {
        T[] neighbours = {Cells[i - 1, j - 1].Current, Cells[i - 1, j].Current, Cells[i - 1, j + 1].Current,
            Cells[i, j - 1].Current, Cells[i, j + 1].Current,
            Cells[i + 1, j - 1].Current, Cells[i + 1, j].Current, Cells[i + 1, j + 1].Current};
        int[] states = new int[Enum.GetNames(typeof(T)).Length];
        foreach (T state in neighbours)
        {
            // if (state is WallCell<T>)
            //     continue;
            states[(int)(object)state]++;
        }
        return states;
    }

    public void Clear()
    {
        Parallel.For(1, Height + 1, i =>
        {
            for (int j = 1; j <= Width; j++)
            {
                Cells[i, j].Current = (T)(object)(0);
                Cells[i, j].Next = (T)(object)(0);
            }
        });
    }
    public void MoveRound()
    {
        EveryRound?.Invoke(this, EventArgs.Empty);
    }
}