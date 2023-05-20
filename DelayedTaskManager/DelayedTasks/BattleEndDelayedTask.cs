namespace TickManager.Tick;

public class BattleEndDelayedTask : IDelayedTask
{
    private readonly IEntity _entity;

    public BattleEndDelayedTask(IEntity entity)
    {
        Console.WriteLine($"+ Tick Registered: {nameof(BattleEndDelayedTask)} with a delay of {Delay} ticks.");
        _entity = entity;
        DelayedTask = () =>
        {
            Console.WriteLine($"+ Tick: {nameof(BattleEndDelayedTask)} invoked.");
            Console.WriteLine($"+ Entity: {_entity.Name} has respawned.");
            /* No longer in combat */
            /* Reset animation */
        };
    }

    public int Delay { get; set; } = 3;
    public Action DelayedTask { get; set; }
}