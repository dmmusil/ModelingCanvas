using ModelingCanvas.Domain;

namespace ModelingCanvas.Tests;

public class LinkingCanvasObjects
{
    [Test]
    public void LinkCommandToEvent()
    {
        var cmd = new Command();
        var @event = new Event();
        cmd.Link(@event);

        Assert.That(cmd.Next, Is.EqualTo(@event));

        cmd.Unlink();
        Assert.That(cmd.Next, Is.Null);
    }

    [Test]
    public void LinkEventToReadModel()
    {
        var evt = new Event();
        var readModel = new ReadModel();
        evt.Link(readModel);

        Assert.That(evt.Next, Is.EqualTo(readModel));

        evt.Unlink();
        Assert.That(evt.Next, Is.Null);
    }

    [Test]
    public void LinkReadModelToAutomation()
    {
        var auto = new Automation();
        var readModel = new ReadModel();
        readModel.Link(auto);

        Assert.That(readModel.Next, Is.EqualTo(auto));

        readModel.Unlink();
        Assert.That(readModel.Next, Is.Null);
    }

    [Test]
    public void LinkReadModelToUserInterface()
    {
        var ui = new UserInterface();
        var readModel = new ReadModel();
        readModel.Link(ui);

        Assert.That(readModel.Next, Is.EqualTo(ui));

        readModel.Unlink();
        Assert.That(readModel.Next, Is.Null);
    }

    [Test]
    public void LinkUserInterfaceToCommand()
    {
        var cmd = new Command();
        var ui = new UserInterface();
        ui.Link(cmd);

        Assert.That(ui.Next, Is.EqualTo(cmd));

        ui.Unlink();
        Assert.That(ui.Next, Is.Null);
    }

    [Test]
    public void LinkAutomationToCommand()
    {
        var cmd = new Command();
        var auto = new Automation();
        auto.Link(cmd);

        Assert.That(auto.Next, Is.EqualTo(cmd));

        auto.Unlink();
        Assert.That(auto.Next, Is.Null);
    }
}