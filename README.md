# Run Forrest... Run...

Run Forrest provided a quick and simple way of running Tasks from the command line.

### Quick Start
#### 1. Call Run Forrest from Main
```MyTaskClass``` tells RunForrest where to scan for tasks.

```csharp
static void Main(string[] args)
{
    RunForrest.Run<MyTaskClass>(args);
}
```
#### 2. Declare a Task
Any method can be a Task, just decorate with the ```Task``` attribute
```csharp
public class BasicTask
{
    private const string TaskName = "basictask";

    private const string TaskDescription = "basic task description";

    //Task attribute
    [Task(TaskName, TaskDescription)]
    public void DoSomething()
    {
        Console.WriteLine("Run Forrest Run...")
    }
}
```

#### 3. Run from you console
'runforrest' will be the name of your .exe
```sh
$ runforrest basictask
$ Run Forrest Run...
```

## Switches
Available switches example switches
```sh
$ runforrest -h
```

```sh
-l -list                list available tasks            <appname> -l
-t -timed               time task execution             <appname> <taskalias> -t
-v -verbose             print exceptions to console     <appname> <taskalias> -v
-m -method              pass method params              <appname> <taskalias> -m arg1 arg2
-c -constructor         pass constructor args           <appname> <taskalias> -c arg1 arg2
-g -group               run a group of tasks            <appname> <groupalias> -g
-p -parra               run a group in paralell         <appname> <groupalias> -g -p
```

### Passing basic arguments to methods / constructors
```-m``` allows you to pass string arguments from the command line to a method e.g. ```runforrest -m arg1 arg2```

```-c``` allows you to pass string arguments from the command line to a constructor e.g. ```runforrest -c arg1 arg2```

