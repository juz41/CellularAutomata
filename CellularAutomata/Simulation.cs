using CellularAutomata.States;
using CellularAutomata.Boards;
namespace CellularAutomata;

public class Simulation
{
    private Board<Basic> _board;
    public Simulation()
    {
        int n = Enum.GetNames(typeof(Basic)).Length;
        var rule = new (int a, int b)[n, 2, n];
        rule[0, 0, 0] = (0, -1); // stay — dead cell never stays dead this way (forces down unless goes up)
        rule[0, 0, 1] = (0, -1);
        rule[0, 1, 0] = (5, 5);  // up — 5 dead neighbors
        rule[0, 1, 1] = (3, 3);  // up — 3 alive neighbors

        rule[1, 0, 0] = (5, 6);  // stay — 5-6 dead neighbors (meaning 2-3 alive)
        rule[1, 0, 1] = (2, 3);
        rule[1, 1, 0] = (0, -1); // up — no case
        rule[1, 1, 1] = (0, -1);

        _board = new Board<Basic>(20,40, rule);
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