using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using CellularAutomata.States;
using CellularAutomata.Boards;
using CellularAutomata.Output;
using Color = SixLabors.ImageSharp.Color;

namespace CellularAutomata;

public class Simulation
{
    private Board<Map> _board;
    private const int Length = 100;
    private static readonly (int, int) CatchNone = (-1, -1);

    public Simulation()
    {
        int n = Enum.GetNames(typeof(Basic)).Length;
        var rule = new List<(int a, int b)>[n, 2, n];
        for (int i = 0; i < n; i++)
        for (int j = 0; j < 2; j++)
        for (int k = 0; k < n; k++)
            rule[i, j, k] = new List<(int a, int b)>();

        // if cell in state a is in range of <a,b> then it stays goes up depending on second index of _rules
        // goes down otherwise
        // (a,b)[i,j,k] -> a,b is range (inclusive)
        // i is current state, j is down/up, last one is all states to match
        rule[0, 1, 1].Add((3, 3)); // Becomes alive if exactly 3 alive neighbors
        rule[1, 0, 1].Add((0, 1)); // Dies if <2 or >3 alive neighbors
        rule[1, 0, 1].Add((4, Int32.MaxValue)); // Dies if <2 or >3 alive neighbors

        n = Enum.GetNames(typeof(Map)).Length;
        rule = new List<(int a, int b)>[n, 2, n];
        for (int i = 0; i < n; i++)
        for (int j = 0; j < 2; j++)
        for (int k = 0; k < n; k++)
            rule[i, j, k] = new List<(int a, int b)>();

        rule[(int)Map.Water, 1, (int)Map.Beach].Add((6, 8));
        
        rule[(int)Map.Beach, 0, (int)Map.Water].Add((5, 8));
        rule[(int)Map.Beach, 1, (int)Map.Beach].Add((5, 8));
        rule[(int)Map.Beach, 1, (int)Map.Land].Add((5, 8));

        rule[(int)Map.Land, 0, (int)Map.Water].Add((3, 8));
        rule[(int)Map.Land, 0, (int)Map.Beach].Add((4, 8));

        _board = new Board<Map>(100, 100, rule);
    }

    public void Run()
    {
        Color[]? mapping = new[] { Color.Aqua, Color.Yellow, Color.Green };
        var output = new ConsoleOutput<Map>(_board);
        var output2 = new GifOutput<Map>(_board, 8, mapping);
        // _board.EveryRound += (s, e) => output.ShowBoard();
        _board.EveryRound += (s, e) => output2.ShowBoard();
        // _board.EveryRound += (s, e) => Console.Read();
        for (int i = 1; i <= Length; i++)
        {
            _board.UpdateBoard();
            _board.MoveRound();
            DrawProgressBar(i, Length, 50);
            // var key = Console.ReadKey(true);
            // if (key.Key == ConsoleKey.Q)
            // {
            //     break;
            // }
        }

        output2.Save();
    }

    static void DrawProgressBar(int progress, int total, int barWidth)
    {
        Console.CursorLeft = 0;
        double percent = (double)progress / total;
        int filledBars = (int)(percent * barWidth);
        // Console.WriteLine($"{percent} {filledBars} {barWidth - filledBars}");
        string bar = new string('#', filledBars) + new string('-', barWidth - filledBars);
        Console.Write($"[{bar}] {progress}%");
    }

    static void Main(string[] args)
    {
        Simulation simulation = new Simulation();
        simulation.Run();
    }
}