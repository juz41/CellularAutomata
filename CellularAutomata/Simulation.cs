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
    private const int Length = 50;
    public Simulation()
    {
        _board = new Board<Map>(600, 600, RulesBase.MapClassic());
    }

    public void Run()
    {
        // var output = new ConsoleOutput<Basic>(_board);
        var output2 = new GifOutput<Map>(_board, 1, [Color.Blue, Color.Yellow, Color.Green]);
        // _board.EveryRound += (s, e) => output.ShowBoard();
        _board.EveryRound += (s, e) => output2.ShowBoard();
        // _board.EveryRound += (s, e) => Console.Read();
        for (int i = 1; i <= Length; i++)
        {
            _board.UpdateBoard();
            _board.MoveRound();
            DrawProgressBar(i, Length, 100);
        }

        output2.Save();
    }

    static void DrawProgressBar(int progress, int total, int barWidth)
    {
        Console.CursorLeft = 0;
        double percent = (double)progress / total;
        int filledBars = (int)(percent * barWidth);
        string bar = new string('#', filledBars) + new string('-', barWidth - filledBars);
        Console.Write($"[{bar}] {filledBars}/{barWidth}");
    }

    static void Main(string[] args)
    {
        Simulation simulation = new Simulation();
        simulation.Run();
    }
}