using ModelingCanvas.Domain;

namespace ModelingCanvas.Tests;

public class BoardTests
{
    [Test]
    public void CommandToEvent()
    {
        var board = new Board(Guid.NewGuid());
        var cmd = new Command(Guid.NewGuid());
        var evt = new Event(Guid.NewGuid());

        board.Add(cmd);
        board.Add(evt);
        board.LinkBetween(cmd, evt);

        Assert.That(board.AreLinked(cmd, evt));

        board.DeleteLink(cmd, evt);
     
        Assert.That(board.AreLinked(cmd, evt), Is.False);
    }

    [Test]
    public void EventToView()
    {
        var board = new Board(Guid.NewGuid());
        var view = new View(Guid.NewGuid());
        var evt = new Event(Guid.NewGuid());

        board.Add(view);
        board.Add(evt);
        board.LinkBetween( evt, view);

        Assert.That(board.AreLinked( evt, view));

        board.DeleteLink(evt, view);

        Assert.That(board.AreLinked(evt, view), Is.False);
    }

    [Test]
    public void DeletingLinkedCardRemovesLink()
    {
        var board = new Board(Guid.NewGuid());
        var view = new View(Guid.NewGuid());
        var evt = new Event(Guid.NewGuid());

        board.Add(view);
        board.Add(evt);
        board.LinkBetween(evt, view);

        Assert.That(board.AreLinked(evt, view));

        board.DeleteCard(evt);

        Assert.That(board.AreLinked(evt, view), Is.False);
    }
}