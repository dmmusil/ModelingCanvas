using Microsoft.AspNetCore.Components;
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
}
