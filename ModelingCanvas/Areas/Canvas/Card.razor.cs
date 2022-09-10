using Microsoft.AspNetCore.Components;

namespace ModelingCanvas.Areas.Canvas;

public partial class Card
{

    [Parameter]
    public Domain.Card Model { get; set; }

    [Parameter]
    public EventCallback OnDelete { get; set; }
}
