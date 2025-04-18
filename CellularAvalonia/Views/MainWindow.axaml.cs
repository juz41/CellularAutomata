using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using CellularAutomata;
using CellularAutomata.Boards;
using CellularAutomata.Output;
using CellularAutomata.States;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

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
        Board = new Board<Map>(100, 100, Rules.MapClassic());
        ImageOutput = new ImageOutput<Map>(Board, 10, [Color.Aqua, Color.Yellow, Color.Green]);
        var bitmap = ConvertToAvaloniaBitmap(ImageOutput.GenerateImage());
        MainImage.Source = bitmap;
        this.InvalidateVisual();
    }

    private void NextOnClick(object? sender, RoutedEventArgs e)
    {
        ImageOutput.Board.UpdateBoard();
        ImageOutput.Board.MoveRound();
        MainImage.Source = ConvertToAvaloniaBitmap(ImageOutput.GenerateImage());
        this.InvalidateVisual();
    }
    public Bitmap ConvertToAvaloniaBitmap(Image<Rgba32> image)
    {
        using (var memoryStream = new MemoryStream())
        {
            image.SaveAsPng(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var avaloniaBitmap = new Bitmap(memoryStream);
            return avaloniaBitmap;
        }
    }
}