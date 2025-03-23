namespace CellularAutomata;

public class Simulation
{
    private Board _board;
    public Simulation()
    {
        _board = new Board();
    }
    public void Run()
    {
        for (int i=0;;i++)
        {
            Console.Clear();
            _board.UpdateBoard();
            ShowBoard();
            Console.Read();
        }
    }
    
    public void ShowBoard()
    {
        for (int i = 0; i < _board.board.GetLength(0); i++)
        {
            for (int j = 0; j < _board.board.GetLength(1); j++)
            {
                Console.Write(_board.board[i, j]);
            }
            Console.WriteLine();
        }
    }
}