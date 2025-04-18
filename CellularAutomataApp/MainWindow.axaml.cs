using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using Avalonia;
using CellularAutomata.Boards;
using CellularAutomata.Output;
using CellularAutomata.States;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        RenderBoard();
    }

    private void RenderBoard()
    {
        var n = Enum.GetNames(typeof(Map)).Length;
        var rule = new List<(int a, int b)>[n, 2, n];
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

        var board = new Board<Map>(100, 100, rule);
        var output = new ImageOutput<Map>(board, 10);
        var imageSharpImage = output.GenerateImage();

        var bitmap = ConvertToWriteableBitmap(imageSharpImage);

        BoardImage.Source = bitmap;
    }

    private WriteableBitmap ConvertToWriteableBitmap(Image<Rgba32> image)
    {
        var pixelSize = new PixelSize(image.Width, image.Height);
        var dpi = new Vector(96, 96);

        var writeableBitmap = new WriteableBitmap(
            pixelSize,
            dpi,
            Avalonia.Platform.PixelFormat.Bgra8888,
            Avalonia.Platform.AlphaFormat.Premultiplied);

        using (var fb = writeableBitmap.Lock())
        {
            var buffer = new Span<byte>((void*)fb.Address, fb.RowBytes * fb.Size.Height);

            for (int y = 0; y < image.Height; y++)
            {
                var srcRow = image.GetPixelRowSpan(y);
                int destRowStart = y * fb.RowBytes;

                for (int x = 0; x < image.Width; x++)
                {
                    var srcPixel = srcRow[x];

                    // Write in BGRA order
                    int destIndex = destRowStart + x * 4;
                    buffer[destIndex + 0] = srcPixel.B;
                    buffer[destIndex + 1] = srcPixel.G;
                    buffer[destIndex + 2] = srcPixel.R;
                    buffer[destIndex + 3] = srcPixel.A;
                }
            }
        }

        return writeableBitmap;
    }
}