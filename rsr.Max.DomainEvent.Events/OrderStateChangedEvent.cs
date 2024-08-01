namespace rsr.Max.DomainEvent;

public class OrderStateChangedEvent
{
    public Guid OrderResourceId { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}