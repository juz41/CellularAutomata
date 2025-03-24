namespace CellularAutomata;

public class Simulation
{
    private Board _board;
    public Simulation()
    {
        _board = new Board(100,400);
    }
    public void Run()
    {
        for (int i=0;;i++)
        {
            Console.Clear();
            _board.UpdateBoard();
            _board.ShowBoard();
            Console.Read();
        }
    }
    
    static void Main(string[] args)
    {
        Simulation simulation = new Simulation();
        simulation.Run();
    }
}