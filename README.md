<br />
<div align="center">
  <h3 align="center">RuneScape 05 Task Delay Emulation</h3>

  <p align="center">
    A manager to emulate delayed tasks in a RuneScape simulation 
    <br />
    <a href="https://oldschool.runescape.com/"><strong>OldSchool RuneScape »</strong></a>
  </p>
</div>


<!-- ABOUT THE PROJECT -->
## About The Project

The project was started becase I was interested in learning how to create a combat system that works like the one RuneScape has implemented.<br/>
And when you defeat an enemy you want to delay the respawn by x amount of ticks to perform events such as playing the "Fall" animation.
This also applies to cutting down a tree, you want to give the tree a delay before it respawns.

Here's why:
* If done correctly, it should serve as a template for other projects which might need to implement something similar.
* Should demonstrate basic SRP.
* It should also demonstrate the DRY principle.

Of course, this project is not by any means flawless and could benefit from some more encapsulation but in good time this shall be worked on as well.


<!-- GETTING STARTED -->
## Getting Started

How to get this running locally.
### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/E-lysian/DelayedTaskManager
   ```
2. Build
   ```sh
   dotnet build
   ```
3. Change Directory (if not already in the project directory)
   ```sh
   cd DelayedTaskManager
   ```

4. Run
    ```sh
    dotnet run
    ```

<br/>

Following these steps successfully should print this out
```cs
Tick..
Tick..
Tick..
Tick..
+ Entity: Goblin has fallen!
+ Tick Registered: BattleEndDelayedTask with a delay of 3 ticks.
Tick..
Tick..
Tick..
+ Tick: BattleEndDelayedTask invoked.
+ Entity: Goblin has respawned.
Tick..
Tick..
...
```

<!-- USAGE EXAMPLES -->
## Usage

### Create a new DelayedTask
In order to create your own DelayedTask you can look at the already existing [BattleEndDelayedTask](https://github.com/E-lysian/DelayedTaskManager/blob/master/DelayedTaskManager/DelayedTasks/BattleEndDelayedTask.cs).<br/>
The way it works is that it inherits from the [IDelayedTask](https://github.com/E-lysian/DelayedTaskManager/blob/master/DelayedTaskManager/DelayedTasks/IDelayedTask.cs) which contains two properties, `int Delay` and `Action DelayedTask`.


```cs
public interface IDelayedTask
{
    public int Delay { get; set; } /* Delay in ticks before executed */
    public Action DelayedTask { get; set; } /* Action to perform when ready */
}
```

In the main cycle it'll demonstrate how to register a new `DelayedTask` ([RegisterDelayedTask](https://github.com/E-lysian/DelayedTaskManager/blob/master/DelayedTaskManager/Handlers/DelayedTaskHandler.cs#LL7C24-L7C24)) and how it checks for any queued `DelayedTask` ([HandleDelayedTasks](https://github.com/E-lysian/DelayedTaskManager/blob/master/DelayedTaskManager/Handlers/DelayedTaskHandler.cs#L12))

```cs
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
```

<br/>

### Example
Here's an example `DelayedTask` which emulates a tree that has been chopped down and now needs to wait in order to respawn.

```cs
public class TreeChoppedDelayedTask : IDelayedTask
{
    private readonly ITree _iTree;
    public TreeChoppedDelayedTask(ITree entity)
    {
        Console.WriteLine($"+ Tick Registered: {nameof(TreeChoppedDelayedTask)} with a delay of {Delay} ticks.");
        _iTree = entity;
        DelayedTask = () =>
        {
            Console.WriteLine($"+ Tick: {nameof(TreeChoppedDelayedTask)} invoked.");
            Console.WriteLine($"+ Entity: {_iTree.Name} has respawned.");
        };
    }
    public int Delay { get; set; } = 3;
    public Action DelayedTask { get; set; }
}
```


<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* [OldSchool RuneScape](https://oldschool.runescape.com/)
* [OSRS Wiki](https://oldschool.runescape.wiki/)
