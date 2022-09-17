
namespace ModelingCanvas.Domain;

public abstract record Card(Guid Id)
{
    private readonly List<Link> _links = new();
    public IReadOnlyList<Link> Links => _links.AsReadOnly();

    public abstract bool CanLinkTo(Card card);
    public abstract bool CanLinkFrom(Card? card);

    public void LinkTo(Card card)
    {
        if (CanLinkTo(card))
        {
            _links.Add(new Link(this, card));
        }
    }

    public void LinkFrom(Card card)
    {
        if (CanLinkFrom(card))
        {
            _links.Add(new Link(card, this));
        }
    }

    public void BreakLinkWith(Card card) => _links.RemoveAll(link => link.Source == card || link.Destination == card);

    public double OffsetX { get; set; }
    public double OffsetY { get; set; }
}

public record Command(Guid Id) : Card(Id)
{
    public override bool CanLinkTo(Card card) => card is Event;
    public override bool CanLinkFrom(Card? card) => card is UserInterface or Automation;
}

public record Event(Guid Id) : Card(Id)
{
    public override bool CanLinkTo(Card card) => card is View;
    public override bool CanLinkFrom(Card? card) => card is Command;
}

public record View(Guid Id) : Card(Id)
{
    public override bool CanLinkTo(Card card) => card is UserInterface or Automation;
    public override bool CanLinkFrom(Card? card) => card is Event;
}

public record UserInterface(Guid Id) : Card(Id)
{
    public override bool CanLinkTo(Card card) => card is Command;
    public override bool CanLinkFrom(Card? card) => card is View;
}

public record Automation(Guid Id) : Card(Id)
{
    public override bool CanLinkTo(Card card) => card is Command;
    public override bool CanLinkFrom(Card? card) => card is View;
}

public record Link(Card Source, Card Destination);

public record Board(Guid Id)
{
    private readonly Dictionary<Guid, Card> _cards = new();
    private readonly List<Link> _links = new();
    public IEnumerable<Card> Cards => _cards.Values;

    public void Add(params Card[] cards)
    {
        foreach (var card in cards)
        {
            _cards[card.Id] = card;
        }
    }

    public void Link(Card destination)
    {
        if (CardToLink?.CanLinkTo(destination) == true)
        {
            _links.Add(new Link(_cards[CardToLink.Id], _cards[destination.Id]));
            CardToLink.LinkTo(destination);
            destination.LinkFrom(CardToLink);
            LinkMode = LinkMode.Inactive;
            CardToLink = null;
        }
    }

    public bool AreLinked(Card source, Card destination) =>
        _links.Contains(new Link(source, destination));

    public void DeleteLink(Card source, Card destination)
    {
        _links.Remove(new Link(source, destination));
        source.BreakLinkWith(destination);
        destination.BreakLinkWith(source);
    }

    public void DeleteCard(Card card)
    {
        foreach (var c in _cards)
        {
            c.Value.BreakLinkWith(card);
        }
        
        _cards.Remove(card.Id);
        _links.RemoveAll(link => link.Source.Id == card.Id || link.Destination.Id == card.Id);
    }

    public void StartLinkMode(Card card)
    {
        LinkMode = LinkMode.Active;
        CardToLink = card;
    }

    public Card? CardToLink { get; set; }

    public LinkMode LinkMode { get; set; } = LinkMode.Inactive;
    public IReadOnlyList<Link> Links => _links.AsReadOnly();

    public void CancelLinkMode()
    {
        LinkMode = LinkMode.Inactive;
        CardToLink = null;
    }
}

public enum LinkMode
{
    Active,
    Inactive
}
