using ModelingCanvas.Domain;

namespace ModelingCanvas.Areas.Canvas;

public partial class Canvas
{
    private readonly Board _board = new Board(Guid.NewGuid());

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _board.Add(new UserInterface(Guid.NewGuid()));
        _board.Add(new Command(Guid.NewGuid()));
        _board.Add(new Event(Guid.NewGuid()));
        _board.Add(new View(Guid.NewGuid()));
    }
}