using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ModelingCanvas.Domain;

namespace ModelingCanvas.Areas.Canvas;

public partial class Card
{

    [Parameter]
    public Domain.Card Model { get; set; }

    [Parameter]
    public EventCallback OnDelete { get; set; }

    [Parameter]
    public EventCallback LinkStarted { get; set; }

    [Parameter]
    public Domain.Card? CardToLink { get; set; }

    [Parameter]
    public LinkMode LinkMode { get; set; }

    private double StartX { get; set; }
    private double StartY { get; set; }

    private double OffsetX { get; set; }
    private double OffsetY { get; set; }

    private string OffsetXStyle => $"{(int)OffsetX}px";
    private string OffsetYStyle => $"{(int)OffsetY}px";

    private void OnDragStart(DragEventArgs obj)
    {
        StartX = obj.ScreenX;
        StartY = obj.ScreenY;
        Dragged = true;
    }

    public bool Dragged { get; set; }

    private void OnDragEnd(DragEventArgs obj)
    {
        OffsetX += obj.ScreenX - StartX;
        OffsetY += obj.ScreenY - StartY;
    }

    private const string DefaultStyle =
        "border: 1px solid black; padding: 5px; margin: 5px; width: 200px; height: 100px;";

    private string Style =>
        Dragged ? $"{DefaultStyle} position: absolute; left: {OffsetXStyle}; top: {OffsetYStyle};" : DefaultStyle;
}
