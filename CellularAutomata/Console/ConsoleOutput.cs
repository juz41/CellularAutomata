using CellularAutomata.Boards;

namespace CellularAutomata.Console;

public class ConsoleOutput<T> where T : Enum, IConvertible
{
    public Board<T> Board;
    public ConsoleOutput(Board<T> board)
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
                System.Console.Write(Board.Cells[row, col].Icon());
            }
            System.Console.WriteLine();
        }
    }
}