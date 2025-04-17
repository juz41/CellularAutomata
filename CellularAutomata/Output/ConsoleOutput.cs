using CellularAutomata.Boards;

namespace CellularAutomata.Output;

public class ConsoleOutput<T> where T : Enum, IConvertible
{
    public IBoard<T> Board;
    public ConsoleOutput(IBoard<T> board)
    {
        Board = board;
    }
    public void ShowBoard()
    {
        System.Console.Clear();
        for (int row = 1; row <= Board.Height; row++)
        {
            for (int col = 1; col <= Board.Width; col++)
            {
                System.Console.Write(Board[row, col].Icon());
            }
            System.Console.WriteLine();
        }
    }
}