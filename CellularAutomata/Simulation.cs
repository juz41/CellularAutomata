using CellularAutomata.States;
using CellularAutomata.Boards;
using CellularAutomata.Output;

namespace CellularAutomata;

public class Simulation
{
    private Board<Basic> _board;

    public Simulation()
    {
        int n = Enum.GetNames(typeof(Basic)).Length;
        var rule = new List<(int a, int b)>[n, 2, n];
        for (int i = 0; i < n; i++)
        for (int j = 0; j < 2; j++)
        for (int k = 0; k < n; k++)
            rule[i, k, j] = new List<(int a, int b)>();
        
        (int, int) catchAll = (0, Int32.MaxValue);
        (int, int) catchNone = (-1, -1);
        // if cell in state a is in range of <a,b> then it stays goes up depending on second index of _rules
        // goes down otherwise
        // (a,b)[i,j,k] -> a,b is range (inclusive)
        // i is current state, j is down/up, last one is all states to match

        rule[0, 0, 0].Add(catchNone); // Dead cell moving DOWN (staying dead) — Dead neighbors
        rule[0, 0, 1].Add(catchNone); // Dead cell moving DOWN (staying dead) — Alive neighbors
        rule[0, 1, 0].Add(catchNone);  // Dead cell moving UP (becomes alive) — Dead neighbors
        rule[0, 1, 1].Add((3, 3)); // Becomes alive if exactly 3 alive neighbors

        rule[1, 0, 0].Add(catchNone); // Alive cell moving DOWN (dies) — Dead neighbors
        rule[1, 0, 1].Add((0, 1)); // Dies if <2 or >3 alive neighbors
        rule[1, 0, 1].Add((4, Int32.MaxValue)); // Dies if <2 or >3 alive neighbors
        rule[1, 1, 0].Add(catchNone); // Alive cell moving UP (stays alive) — Dead neighbors
        rule[1, 1, 1].Add(catchNone); // Stays alive if 2-3 alive neighbors (but since 2-3 allowed, this line



        _board = new Board<Basic>(20, 40, rule);
    }

    public void Run()
    {
        var _output = new ConsoleOutput<Basic>(_board);
        var _output2 = new GifOutput<Basic>(_board);
        _board.EveryRound += (s, e) => _output.ShowBoard();
        _board.EveryRound += (s, e) => _output2.ShowBoard();
        // _board.EveryRound += (s, e) => System.Console.Read();
        for (;;)
        {
            _board.UpdateBoard();
            _board.MoveRound();
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Q)
            {
                break;
            }
        }

        _output2.Save();
    }

    static void Main(string[] args)
    {
        Simulation simulation = new Simulation();
        simulation.Run();
    }
}