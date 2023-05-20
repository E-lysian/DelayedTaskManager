using TickManager;
using TickManager.Tick;

var tickTaskHandler = new TickTaskHandler();

var goblin = new NPC
{
    Id = 0,
    Name = "Goblin",
    HP = 10
};

/* Super accurate tick system */
while (true)
{
    if (goblin.HP > 0)
    {
        /* Simulate goblin being attacked */
        goblin.HP -= 2;
        if (goblin.HP <= 0)
        {
            Console.WriteLine($"+ Entity: {goblin.Name} has fallen!");
            tickTaskHandler.RegisterDelayedTask(new BattleEndDelayedTask(goblin));
        }
    }

    tickTaskHandler.HandleDelayedTasks();
    Console.WriteLine("Tick..");
    Thread.Sleep(600);
}