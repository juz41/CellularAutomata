using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CellularAutomata.Boards;
using CellularAutomata.Output;
using CellularAutomata.States;
using SixLabors.ImageSharp;

namespace CellularAvalonia.Views;

public partial class MainWindow : Window
{
    public Board<Map> Board;
    public ImageOutput<Map> ImageOutput;
    public MainWindow()
    {
        InitializeComponent();
        this.Height = 700;
        this.Width = 700;
        
        var n = Enum.GetNames(typeof(Map)).Length;
        var rule = new List<(int a, int b)>[n, 2, n];
        for (int i = 0; i < n; i++)
        for (int j = 0; j < 2; j++)
        for (int k = 0; k < n; k++)
            rule[i, j, k] = new List<(int a, int b)>();

        rule[(int)Map.Water, 1, (int)Map.Beach].Add((6, 8));
        rule[(int)Map.Water, 1, (int)Map.Land].Add((5, 8));
        
        rule[(int)Map.Beach, 0, (int)Map.Water].Add((5, 8));
        rule[(int)Map.Beach, 1, (int)Map.Beach].Add((5, 8));
        rule[(int)Map.Beach, 1, (int)Map.Land].Add((5, 8));

        rule[(int)Map.Land, 0, (int)Map.Water].Add((3, 8));
        rule[(int)Map.Land, 0, (int)Map.Beach].Add((4, 8));

        Board = new Board<Map>(100, 100, rule);
        Color[]? mapping = new[] { Color.Aqua, Color.Yellow, Color.Green };
        ImageOutput = new ImageOutput<Map>(Board, 10, mapping);
        ImageOutput.ShowBoard("/home/julian/Documents/coding/C#/CellularAutomata/CellularAvalonia/Assets/output.png");
    }

    private void NextOnClick(object? sender, RoutedEventArgs e)
    {
        ImageOutput.ShowBoard("/home/julian/Documents/coding/C#/CellularAutomata/CellularAvalonia/Assets/output.png");
        ImageOutput.Board.UpdateBoard();
        ImageOutput.Board.MoveRound();
        var bitmap = new Avalonia.Media.Imaging.Bitmap("/home/julian/Documents/coding/C#/CellularAutomata/CellularAvalonia/Assets/output.png");
        MainImage.Source = bitmap;
        this.InvalidateVisual();
    }
}