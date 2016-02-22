# Run Forrest... Run...

Run Forrest provided a quick and simple way of running Tasks from the command line.

### Quick Start
#### 1. Call Run Forrest from Main
MyTaskClass tells RunForrest where to scan for tasks.

```csharp
static void Main(string[] args)
{
    RunForrest.Run<MyTaskClass>(args);
}
```
#### 2. Declare a Task
Any method can be a Task, just decorate with the Task attribute
```csharp
public class BasicTask
{
    private const string TaskName = "basictask";

    private const string TaskDescription = "basic task description";

    //Task attribute
    [Task(TaskName, TaskDescription, 10)]
    public void DoSomething()
    {
        Console.WriteLine("Run Forrest Run...")
    }
}
```

#### 3. Run from you console
'runforrest' will be the name of you exe
```sh
$ runforrest basictask
$ Run Forrest Run...
```

