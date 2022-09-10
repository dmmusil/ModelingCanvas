using ModelingCanvas.Domain;

namespace ModelingCanvas.Areas.Canvas;

public partial class Canvas
{
    private readonly Board _board = new Board(Guid.NewGuid());
}