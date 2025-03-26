namespace CellularAutomata;

public class Board
{
    public readonly int Height;
    public readonly int Width;
    public readonly Cell[,] Cells;
    public Board(int height, int width)
    {
        this.Height = height;
        this.Width = width;
        Cells = new Cell[Height+2, Width+2];
        Random rand = new Random();
        for (int i = 0; i < Height+2; i++)
        {
            Cells[i, 0] = new WallClassicCell();
            Cells[i, Width+1] = new WallClassicCell();
        }
        for (int j = 0; j < Width+2; j++)
        {
            Cells[0, j] = new WallClassicCell();
            Cells[Height+1, j] = new WallClassicCell();
        }
        
        for (int i = 1; i <= Height; i++)
        {
            for (int j = 1; j <= Width; j++)
            {
                Cells[i, j] = new ClassicCell((State) rand.Next(0, 2));
            }
        }
    }
    public void UpdateBoard()
    {
        for (int i = 1; i <= Height; i++)
        {
            for (int j = 1; j <= Width; j++)
            {
                Cells[i, j].UpdateCell(AliveNeighbors(i,j));
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
        State[] neighbours = {Cells[i - 1, j - 1].Current, Cells[i - 1, j].Current, Cells[i - 1, j + 1].Current,
            Cells[i, j - 1].Current, Cells[i, j + 1].Current,
            Cells[i + 1, j - 1].Current, Cells[i + 1, j].Current, Cells[i + 1, j + 1].Current};
        int[] states = new int[Enum.GetNames(typeof(State)).Length];
        foreach (State state in neighbours)
            states[(int)state]++;
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