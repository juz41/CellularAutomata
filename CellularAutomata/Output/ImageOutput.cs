using CellularAutomata.Boards;
using CellularAutomata.Cells;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace CellularAutomata.Output;

public class ImageOutput<T> where T : Enum, IConvertible
{
    public IBoard<T> Board;
    private readonly int _cellSize;

    public ImageOutput(IBoard<T> board, int cellSize = 1)
    {
        Board = board;
        this._cellSize = cellSize;
    }

    public void ShowBoard(string filename = "output.png")
    {
        int imageWidth = Board.Width * _cellSize;
        int imageHeight = Board.Height * _cellSize;
        using (var image = new Image<Rgba32>(imageWidth, imageHeight))
        {
            for (int row = 1; row <= Board.Height; row++)
            {
                for (int col = 1; col <= Board.Width; col++)
                {
                    for (int i = 0; i < _cellSize; i++)
                    for (int j = 0; j < _cellSize; j++)
                        image[(col-1)*_cellSize+i, (row-1)*_cellSize+j] = GetColor(Board[row, col]);
                }
            }
            image.Save(filename);
        }
    }
    public Image<Rgba32> GenerateImage()
    {
        int imageWidth = Board.Width * _cellSize;
        int imageHeight = Board.Height * _cellSize;
        var image = new Image<Rgba32>(imageWidth, imageHeight);

        for (int row = 1; row <= Board.Height; row++)
        {
            for (int col = 1; col <= Board.Width; col++)
            {
                for (int i = 0; i < _cellSize; i++)
                for (int j = 0; j < _cellSize; j++)
                    image[(col - 1) * _cellSize + i, (row - 1) * _cellSize + j] = GetColor(Board[row, col]);
            }
        }
        return image;
    }
    private Color GetColor(Cell<T> cell)
    {
        int iconValue = cell.Icon();
        return iconValue switch
        {
            0 => Color.Black, // Dead cell (black)
            1 => Color.White, // Alive cell (white)
            _ => Color.Red // Fallback (red)
        };
    }
}