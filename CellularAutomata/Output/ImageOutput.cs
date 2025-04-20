using CellularAutomata.Boards;
using CellularAutomata.Cells;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace CellularAutomata.Output;

public class ImageOutput<T> where T : Enum, IConvertible
{
    public IBoard<T> Board;
    public readonly int CellSize;
    private readonly int _imageWidth;
    private readonly int _imageHeight;
    private readonly Color[]? _mapping;

    public ImageOutput(IBoard<T> board, int cellSize = 1, Color[]? mapping = null)
    {
        if (mapping == null)
            this._mapping = [Color.Black, Color.White];
        else
            this._mapping = mapping;
        Board = board;
        this.CellSize = cellSize;
        _imageWidth = Board.Width * cellSize;
        _imageHeight = Board.Height * cellSize;
    }

    public void ShowBoard(string filename = "output.png")
    {
        using (var image = new Image<Rgba32>(_imageWidth, _imageHeight))
        {
            for (int row = 1; row <= Board.Height; row++)
            {
                for (int col = 1; col <= Board.Width; col++)
                {
                    for (int i = 0; i < CellSize; i++)
                    for (int j = 0; j < CellSize; j++)
                        image[(col-1)*CellSize+i, (row-1)*CellSize+j] = GetColor(Board[row, col]);
                }
            }
            image.Save(filename);
        }
    }
    public Image<Rgba32> GenerateImage()
    {
        int imageWidth = Board.Width * CellSize;
        int imageHeight = Board.Height * CellSize;
        var image = new Image<Rgba32>(imageWidth, imageHeight);

        for (int row = 1; row <= Board.Height; row++)
        {
            for (int col = 1; col <= Board.Width; col++)
            {
                for (int i = 0; i < CellSize; i++)
                for (int j = 0; j < CellSize; j++)
                    image[(col - 1) * CellSize + i, (row - 1) * CellSize + j] = GetColor(Board[row, col]);
            }
        }
        return image;
    }
    
    private Color GetColor(Cell<T> cell)
    {
        int iconValue = cell.Icon();
        return _mapping[iconValue];
    }
}