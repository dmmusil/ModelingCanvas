using ModelingCanvas.Domain;

namespace ModelingCanvas.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void LinkEventToCommand()
        {
            var cmd = new Command();
            var @event = new Event();
            cmd.Link(@event);

            Assert.That(cmd.Event, Is.EqualTo(@event));

            cmd.Unlink(@event);
            Assert.That(cmd.Event, Is.Null);
        }

        [Test]
        public void LinkEventToReadModel()
        {
            var evt = new Event();
            var readModel = new ReadModel();
            evt.Link(readModel);

            Assert.That(evt.ReadModel, Is.EqualTo(readModel));

            evt.Unlink(readModel);
            Assert.That(evt.ReadModel, Is.Null);
        }

        [Test]
        public void LinkReadModelToAutomation()
        {
            var auto = new Automation();
            var readModel = new ReadModel();
            readModel.Link(auto);

            Assert.That(readModel.CommandIssuer, Is.EqualTo(auto));

            readModel.Unlink(auto);
            Assert.That(readModel.CommandIssuer, Is.Null);
        }

        [Test]
        public void LinkReadModelToUserInterface()
        {
            var ui = new UserInterface();
            var readModel = new ReadModel();
            readModel.Link(ui);

            Assert.That(readModel.CommandIssuer, Is.EqualTo(ui));

            readModel.Unlink(ui);
            Assert.That(readModel.CommandIssuer, Is.Null);
        }

        [Test]
        public void LinkUserInterfaceToCommand()
        {
            var cmd = new Command();
            var ui = new UserInterface();
            ui.Link(cmd);

            Assert.That(ui.Command, Is.EqualTo(cmd));

            ui.Unlink(cmd);
            Assert.That(ui.Command, Is.Null);
        }

        [Test]
        public void LinkAutomationToCommand()
        {
            var cmd = new Command();
            var auto = new Automation();
            auto.Link(cmd);

            Assert.That(auto.Command, Is.EqualTo(cmd));

            auto.Unlink(cmd);
            Assert.That(auto.Command, Is.Null);
        }
    }
}