using TestFurstWPF;

public class Ididle
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Downtime> Downtimes { get; set; } = new List<Downtime>();
}