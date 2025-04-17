using CellularAutomata.Boards;
using CellularAutomata.Cells;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace CellularAutomata.Output;

public class ImageOutput<T> where T : Enum, IConvertible
{
    public IBoard<T> Board;
    // private int cellSize;

    public ImageOutput(IBoard<T> board, int cellSize = 1)
    {
        Board = board;
        // this.cellSize = cellSize;
    }

    public void ShowBoard(string filename = "output.png")
    {
        // int imageWidth = Board.Width * cellSize;
        // int imageHeight = Board.Height * cellSize;
        int imageHeight = Board.Height;
        int imageWidth = Board.Width;

        using (var image = new Image<Rgba32>(imageWidth, imageHeight))
        {
            for (int row = 1; row <= Board.Height; row++)
            {
                for (int col = 1; col <= Board.Width; col++)
                {
                    image[col - 1, row - 1] = GetColor(Board[row, col]);
                }
            }

            // Save the generated image to the specified filename
            image.Save(filename);
        }
    }

    // Map the state of the cell to a color
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