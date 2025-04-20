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
        RefreshImage();
    }

    private void NextOnClick(object? sender, RoutedEventArgs e)
    {
        ImageOutput.Board.UpdateBoard();
        ImageOutput.Board.MoveRound();
        RefreshImage();
    }
    private void ResetOnClick(object? sender, RoutedEventArgs e)
    {
        ImageOutput.Board.Clear();
        RefreshImage();
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
    
    private void Image_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (MainImage.Source is Avalonia.Media.Imaging.Bitmap bitmap)
        {
            var point = e.GetPosition(MainImage);
            var controlWidth = MainImage.Bounds.Width;
            var controlHeight = MainImage.Bounds.Height;
            int pixelX = (int)(point.X / controlWidth * ImageOutput.Board.Width)+1;
            int pixelY = (int)(point.Y / controlHeight * ImageOutput.Board.Height)+1;
            ImageOutput.Board[pixelY, pixelX].CurrentUp();
            RefreshImage();
        }
    }

    private void RefreshImage()
    {
        var bitmap = ConvertToAvaloniaBitmap(ImageOutput.GenerateImage());
        MainImage.Source = bitmap;
        this.InvalidateVisual();
    }
}