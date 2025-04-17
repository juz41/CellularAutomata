using CellularAutomata.Cells;
using CellularAutomata.States;

namespace CellularAutomata.Boards;

public class Board<T> : IBoard<T> where T : Enum, IConvertible
{
    public readonly int Height;
    public readonly int Width;
    public readonly Cell<T>[,] Cells;
    private readonly int _n = Enum.GetNames(typeof(T)).Length;
    public Board(int height, int width, (int a, int b)[,,] rule)
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
                Cells[i, j] = new Cell<T>(rule, rand.Next()%2);
            }
        }
    }
    public void UpdateBoard()
    {
        for (int i = 1; i <= Height; i++)
        {
            for (int j = 1; j <= Width; j++)
            {
                Cells[i, j].UpdateCell(new Neighborhood<Cell<T> ,T>(AliveNeighbors(i,j)));
            }
        }
        for (int i = 1; i <= Height; i++)
        {
            for (int j = 1; j <= Width; j++)
            {
                Cells[i, j].MoveStatus();
            }
        }
    }
    public int[] AliveNeighbors(int i, int j)
    {
        T[] neighbours = {Cells[i - 1, j - 1].Current, Cells[i - 1, j].Current, Cells[i - 1, j + 1].Current,
            Cells[i, j - 1].Current, Cells[i, j + 1].Current,
            Cells[i + 1, j - 1].Current, Cells[i + 1, j].Current, Cells[i + 1, j + 1].Current};
        int[] states = new int[Enum.GetNames(typeof(Basic)).Length];
        foreach (T state in neighbours)
            states[(int)(object)state]++;
        return states;
    }
    public void ShowBoard()
    {
        for (int i = 0; i < this.Cells.GetLength(0); i++)
        {
            for (int j = 0; j < this.Cells.GetLength(1); j++)
            {
                Console.Write(this.Cells[i, j].Icon());
            }
            Console.WriteLine();
        }
    }
}