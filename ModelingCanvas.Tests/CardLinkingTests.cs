using ModelingCanvas.Domain;
using static System.Guid;

namespace ModelingCanvas.Tests;

public class CardLinkingTests
{
    [Test]
    public void LinkingCreatesLinkInBoardAndCards()
    {
        var cmd = new Command(NewGuid());
        var evt = new Event(NewGuid());
        var board = new Board(NewGuid());

        board.Add(cmd, evt);

        board.StartLinkMode(cmd);
        Assert.That(board.CardToLink, Is.EqualTo(cmd));

        board.Link(evt);

        Assert.Multiple(() =>
        {
            Assert.That(board.LinkMode, Is.EqualTo(LinkMode.Inactive));
            Assert.That(board.Links, Has.Count.EqualTo(1));
            Assert.That(board.Links[0], Is.EqualTo(new Link(cmd, evt)));
            Assert.That(cmd.Links[0], Is.EqualTo(new Link(cmd, evt)));
            Assert.That(evt.Links[0], Is.EqualTo(new Link(cmd, evt)));
        });
    }

    [Test]
    public void BreakingALinkRemovesAllRelatedLinksInBoardAndCards()
    {
        var cmd = new Command(NewGuid());
        var evt = new Event(NewGuid());
        var board = new Board(NewGuid());

        board.Add(cmd, evt);

        board.StartLinkMode(cmd);
        Assert.That(board.CardToLink, Is.EqualTo(cmd));

        board.Link(evt);
        board.DeleteLink(cmd, evt);

        Assert.Multiple(() =>
        {
            Assert.That(board.LinkMode, Is.EqualTo(LinkMode.Inactive));
            Assert.That(board.Links, Has.Count.EqualTo(0));
            Assert.That(cmd.Links, Is.Empty);
            Assert.That(evt.Links, Is.Empty);
        });
    }

    [Test]
    public void DeletingALinkedCardRemovesAllRelatedLinksInBoardAndRemainingCards()
    {
        var cmd = new Command(NewGuid());
        var evt = new Event(NewGuid());
        var evt2 = new Event(NewGuid());
        var board = new Board(NewGuid());

        board.Add(cmd, evt, evt2);

        board.StartLinkMode(cmd);
        Assert.That(board.CardToLink, Is.EqualTo(cmd));

        board.Link(evt);

        board.StartLinkMode(cmd);
        Assert.That(board.CardToLink, Is.EqualTo(cmd));

        board.Link(evt2);
        Assert.Multiple(() =>
        {
            Assert.That(board.Links, Has.Count.EqualTo(2));
            Assert.That(cmd.Links, Has.Count.EqualTo(2));
            Assert.That(evt.Links, Has.Count.EqualTo(1));
            Assert.That(evt2.Links, Has.Count.EqualTo(1));
        });

        board.DeleteCard(cmd);

        Assert.Multiple(() =>
        {
            Assert.That(board.LinkMode, Is.EqualTo(LinkMode.Inactive));
            Assert.That(board.Links, Is.Empty);
            Assert.That(cmd.Links, Is.Empty);
            Assert.That(evt.Links, Is.Empty);
        });
    }


    [Test]
    public void CorrectLinkBrokenWhenCardWithMultipleLinksHasOneOfItsLinkedCardsDeleted()
    {
        var cmd = new Command(NewGuid());
        var evt = new Event(NewGuid());
        var evt2 = new Event(NewGuid());
        var board = new Board(NewGuid());

        board.Add(cmd, evt, evt2);
        board.StartLinkMode(cmd);
        board.Link(evt);
        board.StartLinkMode(cmd);
        board.Link(evt2);
        board.DeleteCard(evt);

        Assert.Multiple(() =>
        {
            Assert.That(board.Links.Count, Is.EqualTo(1));
            Assert.That(cmd.Links, Has.Count.EqualTo(1));
            Assert.That(evt2.Links, Has.Count.EqualTo(1));
            Assert.That(board.Links[0], Is.EqualTo(evt2.Links[0]));
        });
    }
}