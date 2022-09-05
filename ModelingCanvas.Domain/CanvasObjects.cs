namespace ModelingCanvas.Domain;

public abstract record LinkedObject<T> 
{
    public T? Next { get; protected set; }
    public void Link(T next) => Next = next;
    public void Unlink() => Next = default;
}

public record Command : LinkedObject<Event>;

public record Event : LinkedObject<ReadModel>;

public record ReadModel : LinkedObject<CommandIssuer>;

public abstract record CommandIssuer : LinkedObject<Command>;

public record Automation : CommandIssuer;

public record UserInterface : CommandIssuer;