namespace RatGang.Server.Tasks.Database.Entety;

public class Task
{
    public Guid Guid { get; set; }
    public Guid UserId { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public long DateCreate { get; set; } = 0;
    public long PlanDate { get; set; } = 0;

    public TaskStatus Status { get; set; }
    public TaskPrioritys TaskPriority { get; set; }
}

public enum TaskStatus
{
    Completed,
    Current,
    Deferred
}

public enum TaskPrioritys
{
    None,
    Minimum,
    Low,
    Medium,
    High,
    VeryHigh,
    Critical
}
