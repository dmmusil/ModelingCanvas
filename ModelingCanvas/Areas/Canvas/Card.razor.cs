using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ModelingCanvas.Domain;

namespace ModelingCanvas.Areas.Canvas;

public partial class Card
{
    public const int Width = 200;
    public const int Height = 100;

    [Parameter]
    public Domain.Card Model { get; set; } = null!;

    [Parameter]
    public EventCallback OnDelete { get; set; }

    [Parameter]
    public EventCallback LinkStarted { get; set; }

    [Parameter]
    public Domain.Card? CardToLink { get; set; }

    [Parameter]
    public LinkMode LinkMode { get; set; }

    [Parameter]
    public EventCallback OnAcceptLink { get; set; }

    [Parameter]
    public EventCallback OnDragged { get; set; }

    private double StartX { get; set; }
    private double StartY { get; set; }

    private string OffsetXStyle => $"{(int)Model.OffsetX}px";
    private string OffsetYStyle => $"{(int)Model.OffsetY}px";

    private void OnDragStart(DragEventArgs obj)
    {
        StartX = obj.ScreenX;
        StartY = obj.ScreenY;
    }

    public bool Dragged { get; set; }

    private void OnDragEnd(DragEventArgs obj)
    {
        Model.OffsetX += obj.ScreenX - StartX;
        Model.OffsetY += obj.ScreenY - StartY;
        Dragged = true;
        OnDragged.InvokeAsync();
    }

    private static readonly string DefaultStyle =
        $"border: 1px solid black; border-radius: 6px; padding: 5px; margin: 5px; width: {Width}px; height: {Height}px;";

    private string Style =>
        Dragged ? $"{DefaultStyle} position: absolute; left: {OffsetXStyle}; top: {OffsetYStyle};" : DefaultStyle;
}
