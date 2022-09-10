﻿using System.Collections;

namespace ModelingCanvas.Domain;

public abstract record Card(Guid Id)
{
    public abstract bool CanLinkTo(Card card);
    public abstract bool CanLinkFrom(Card card);
}

public record Command(Guid Id) : Card(Id)
{
    public override bool CanLinkTo(Card card) => card is Event;
    public override bool CanLinkFrom(Card card) => card is UserInterface or Automation;
}

public record Event(Guid Id) : Card(Id)
{
    public override bool CanLinkTo(Card card) => card is View;
    public override bool CanLinkFrom(Card card) => card is Command;
}

public record View(Guid Id) : Card(Id)
{
    public override bool CanLinkTo(Card card) => card is UserInterface or Automation;
    public override bool CanLinkFrom(Card card) => card is Event;

}

public record UserInterface(Guid Id) : Card(Id)
{
    public override bool CanLinkTo(Card card) => card is Command;
    public override bool CanLinkFrom(Card card) => card is View;
}

public record Automation(Guid Id) : Card(Id)
{
    public override bool CanLinkTo(Card card) => card is Command;
    public override bool CanLinkFrom(Card card) => card is View;
}

public record Link(Card Source, Card Destination);

public record Board(Guid Id)
{
    private readonly Dictionary<Guid, Card> _cards = new();
    private readonly List<Link> _links = new();
    public IEnumerable<Card> Cards => _cards.Values;

    public void Add(Card card) => _cards[card.Id] = card;

    public void LinkBetween(Card source, Card destination)
    {
        if (source.CanLinkTo(destination))
        {
            _links.Add(new Link(_cards[source.Id], _cards[destination.Id]));
        }
    }

    public bool AreLinked(Card source, Card destination) =>
        _links.Contains(new Link(source, destination));

    public void DeleteLink(Card source, Card destination) => 
        _links.Remove(new Link(source, destination));

    public void DeleteCard(Card card)
    {
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
