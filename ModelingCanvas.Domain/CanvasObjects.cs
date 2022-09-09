namespace ModelingCanvas.Domain;

public interface ILinkTo<T>
{
    T? Next { get; }
    void LinkTo(T next);
    void UnlinkNext();
}

public interface ILinkFrom<T>
{
    T? Prev { get; }
    void LinkFrom(T prev);
    void UnlinkPrev();
}

public abstract record ForwardLinkTo<TNext> : ILinkTo<TNext>
{
    public TNext? Next { get; private set; }
    public void LinkTo(TNext next) => Next = next;
    public void UnlinkNext() => Next = default;
}

public abstract record LinkToLinkFrom<TNext, TPrev> : ForwardLinkTo<TNext>, ILinkFrom<TPrev>
{
    public TPrev? Prev { get; private set; }
    public void LinkFrom(TPrev prev) => Prev = prev;
    public void UnlinkPrev() => Prev = default;
}

public record Command : LinkToLinkFrom<Event, CommandIssuer>;

public record Event : LinkToLinkFrom<ReadModel, Command>;

public record SpontaneousEvent : ForwardLinkTo<ReadModel>;

public record ReadModel : LinkToLinkFrom<CommandIssuer, Event>;

public abstract record CommandIssuer : LinkToLinkFrom<Command, ReadModel>;

public record Automation : CommandIssuer;

public record UserInterface : CommandIssuer;

public abstract class LinkBetween<TSource, TDestination> 
    where TSource : ILinkTo<TDestination>
    where TDestination : ILinkFrom<TSource>
{
    public TSource Source { get; }
    public TDestination Destination { get; }

    protected LinkBetween(TSource source, TDestination destination)
    {
        source.LinkTo(destination);
        destination.LinkFrom(source);
        Source = source;
        Destination = destination;
    }
}

public class CommandToEvent : LinkBetween<Command, Event>
{
    public CommandToEvent(Command source, Event destination) : base(source, destination)
    {
    }
}

public class EventToReadModel : LinkBetween<Event, ReadModel>
{
    public EventToReadModel(Event source, ReadModel destination) : base(source, destination)
    {
    }
}

public class ReadModelToCommandIssuer : LinkBetween<ReadModel, CommandIssuer>
{
    public ReadModelToCommandIssuer(ReadModel source, CommandIssuer destination) : base(source, destination)
    {
    }
}

public class CommandIssuerToCommand : LinkBetween<CommandIssuer, Command>
{
    public CommandIssuerToCommand(CommandIssuer source, Command destination) : base(source, destination)
    {
    }
}