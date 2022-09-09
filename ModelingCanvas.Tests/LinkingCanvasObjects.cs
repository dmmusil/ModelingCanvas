using ModelingCanvas.Domain;

namespace ModelingCanvas.Tests;

public class LinkingCanvasObjects
{
    [Test]
    public void LinkCommandToEvent()
    {
        var cmd = new Command();
        var @event = new Event();

        var link = new CommandToEvent(cmd, @event);

        Assert.That(link.Source, Is.EqualTo(cmd));
        Assert.That(link.Destination, Is.EqualTo(@event));

        Assert.That(cmd.Next, Is.EqualTo(@event));

        cmd.UnlinkNext();
        Assert.That(cmd.Next, Is.Null);
    }

    [Test]
    public void LinkEventToReadModel()
    {
        var evt = new Event();
        var readModel = new ReadModel();

        var link = new EventToReadModel(evt, readModel);
        Assert.That(link.Source, Is.EqualTo(evt));
        Assert.That(link.Destination, Is.EqualTo(readModel));

        Assert.That(evt.Next, Is.EqualTo(readModel));

        evt.UnlinkNext();
        Assert.That(evt.Next, Is.Null);
    }

    [Test]
    public void LinkReadModelToAutomation()
    {
        var auto = new Automation();
        var readModel = new ReadModel();

        var link = new ReadModelToCommandIssuer(readModel, auto);
        Assert.That(link.Source, Is.EqualTo(readModel));
        Assert.That(link.Destination, Is.EqualTo(auto));

        Assert.That(readModel.Next, Is.EqualTo(auto));

        readModel.UnlinkNext();
        Assert.That(readModel.Next, Is.Null);
    }

    [Test]
    public void LinkReadModelToUserInterface()
    {
        var ui = new UserInterface();
        var readModel = new ReadModel();

        var link = new ReadModelToCommandIssuer(readModel, ui);
        Assert.That(link.Source, Is.EqualTo(readModel));
        Assert.That(link.Destination, Is.EqualTo(ui));

        Assert.That(readModel.Next, Is.EqualTo(ui));

        readModel.UnlinkNext();
        Assert.That(readModel.Next, Is.Null);
    }

    [Test]
    public void LinkUserInterfaceToCommand()
    {
        var cmd = new Command();
        var ui = new UserInterface();

        var link = new CommandIssuerToCommand(ui, cmd);
        Assert.That(link.Source, Is.EqualTo(ui));
        Assert.That(link.Destination, Is.EqualTo(cmd));

        Assert.That(ui.Next, Is.EqualTo(cmd));

        ui.UnlinkNext();
        Assert.That(ui.Next, Is.Null);
    }

    [Test]
    public void LinkAutomationToCommand()
    {
        var cmd = new Command();
        var auto = new Automation();

        var link = new CommandIssuerToCommand(auto, cmd);
        Assert.That(link.Source, Is.EqualTo(auto));
        Assert.That(link.Destination, Is.EqualTo(cmd));

        Assert.That(auto.Next, Is.EqualTo(cmd));

        auto.UnlinkNext();
        Assert.That(auto.Next, Is.Null);
    }

    [Test]
    public void UserInterfaceIssuesCommandRecordsEventFlowsToReadModel()
    {
        var ui = new UserInterface();
        var cmd = new Command();
        var evt = new Event();
        var readModel = new ReadModel();

        var uiToCmd = new CommandIssuerToCommand(ui, cmd);
        var cmdToEvt = new CommandToEvent(cmd, evt);
        var evtToReadModel = new EventToReadModel(evt, readModel);

        Assert.That(readModel, Is.EqualTo(ui?.Next?.Next?.Next));
    }
}