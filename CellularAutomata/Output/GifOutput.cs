using System.Xml.Linq;
using CellularAutomata.Boards;
using CellularAutomata.Cells;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace CellularAutomata.Output;

public class GifOutput<T> where T : Enum, IConvertible
{
    public readonly IBoard<T> Board;
    private const int FrameDuration = 1;
    private readonly Image<Rgba32> _gif;
    private readonly int _cellSize;
    private readonly int _imageWidth;
    private readonly int _imageHeight;
    private readonly Color[]? _mapping;

    public GifOutput(IBoard<T> board, int cellSize = 1, Color[]? mapping = null)
    {
        if (mapping == null)
            this._mapping = [Color.Black, Color.White];
        else
            this._mapping = mapping;
        
        Board = board;
        this._cellSize = cellSize;
        _imageWidth = Board.Width * cellSize;
        _imageHeight = Board.Height * cellSize;
        _gif = new Image<Rgba32>(_imageWidth, _imageHeight);
    }

    public void ShowBoard()
    {
        using (var image = new Image<Rgba32>(_imageWidth, _imageHeight))
        {
            for (int row = 1; row <= Board.Height; row++)
            {
                for (int col = 1; col <= Board.Width; col++)
                {
                    for (int i = 0; i < _cellSize; i++)
                    for (int j = 0; j < _cellSize; j++)
                    {
                        // Console.WriteLine($"{GetColor(Board[row, col])} {Board[row, col].Icon()}");
                        image[(col-1)*_cellSize+i, (row-1)*_cellSize+j] = GetColor(Board[row, col]);
                    }
                }
            }
            image.Frames.RootFrame.Metadata.GetGifMetadata().FrameDelay = FrameDuration;
            _gif.Frames.AddFrame(image.Frames.RootFrame);
        }
    }
    public void Save(string filename = "output.gif")
    {
        using (var fileStream = new FileStream(filename, FileMode.Create))
        {
            _gif.SaveAsGif(fileStream);
        }
    }
    private Color GetColor(Cell<T> cell)
    {
        int iconValue = cell.Icon();
        return _mapping[iconValue];
    }
}