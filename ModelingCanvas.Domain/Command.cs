namespace ModelingCanvas.Domain
{
    public class Command
    {
        public Event? Event { get; private set; }
        public void Link(object? obj)
        {
            if (obj is Event @event)
                Event = @event;
        }

        public void Unlink(object obj)
        {
            if (obj is Event)
                Event = null;
        }
    }

    public class Event
    {
        public ReadModel? ReadModel { get; private set; }
        public void Link(object? obj)
        {
            if (obj is ReadModel readModel)
                ReadModel = readModel;
        }

        public void Unlink(object obj)
        {
            if (obj is ReadModel)
                ReadModel = null;
        }
    }

    public class ReadModel
    {
        public CommandIssuer? CommandIssuer { get; private set; }

        public void Link(CommandIssuer obj) => CommandIssuer = obj;

        public void Unlink(object obj) => CommandIssuer = null;
    }

    public abstract class CommandIssuer
    {
        public Command? Command { get; private set; }

        public void Link(object obj)
        {
            if (obj is Command command)
                Command = command;
        }

        public void Unlink(object obj)
        {
            Command = null;
        }
    }

    public class Automation : CommandIssuer { }

    public class UserInterface : CommandIssuer { }
}