namespace CellularAutomata;

public class Board
{
    private const int Height = 100;
    private const int Width = 100;
    public Cell[,] board = new Cell[Height+2, Width+2];
    public Board()
    {
        Random rand = new Random();
        for (int i = 0; i < Height+2; i++)
        {
            board[i, 0] = new WallCell();
            board[i, Width+1] = new WallCell();
        }
        for (int j = 0; j < Width+2; j++)
        {
            board[0, j] = new WallCell();
            board[Height+1, j] = new WallCell();
        }
        
        for (int i = 1; i <= Height; i++)
        {
            for (int j = 1; j <= Width; j++)
            {
                board[i, j] = new Cell(rand.NextDouble() >= 0.5);
            }
        }
    }

    public void UpdateBoard()
    {
        for (int i = 1; i <= Height; i++)
        {
            for (int j = 1; j <= Width; j++)
            {
                board[i, j].UpdateCell(GetNeighbors(i,j));
            }
        }
        for (int i = 1; i <= Height; i++)
        {
            for (int j = 1; j <= Width; j++)
            {
                board[i, j].MoveStatus();
            }
        }
    }

    public int GetNeighbors(int i, int j)
    {
        bool[] tmp = {board[i - 1, j - 1].IsAlive, board[i - 1, j].IsAlive, board[i - 1, j + 1].IsAlive,
            board[i, j - 1].IsAlive, board[i, j].IsAlive, board[i, j + 1].IsAlive,
            board[i + 1, j - 1].IsAlive, board[i + 1, j].IsAlive, board[i + 1, j + 1].IsAlive};
        return tmp.Count(c => c);
    }

}