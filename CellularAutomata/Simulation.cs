using CellularAutomata.States;
namespace CellularAutomata;

public class Simulation
{
    private Board<Basic> _board;
    public Simulation()
    {
        _board = new Board<Basic>(20,40, "3/23");
    }
    public void Run()
    {
        for (;;)
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