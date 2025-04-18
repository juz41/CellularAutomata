using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using CellularAutomata.States;
using CellularAutomata.Boards;
using CellularAutomata.Output;
using Color = SixLabors.ImageSharp.Color;

namespace CellularAutomata;

public class Simulation
{
    private Board<Basic> _board;
    private const int Length = 100;
    private static readonly (int, int) CatchNone = (-1, -1);

    public Simulation()
    {
        _board = new Board<Basic>(100, 100, Rules.BasicClassic());
    }

    public void Run()
    {
        // var output = new ConsoleOutput<Basic>(_board);
        var output2 = new GifOutput<Basic>(_board, 8, [Color.Black, Color.Yellow]);
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