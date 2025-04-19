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
    private readonly ImageOutput<T> _image;

    public GifOutput(IBoard<T> board, int cellSize = 1, Color[]? mapping = null)
    {
        _image = new ImageOutput<T>(board, cellSize, mapping);
        Board = board;
        this._cellSize = cellSize;
        _imageWidth = Board.Width * _cellSize;
        _imageHeight = Board.Height * _cellSize;
        _gif = new Image<Rgba32>(_imageWidth, _imageHeight);
    }

    public void ShowBoard()
    {
        using (var image = _image.GenerateImage())
        {
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