using System.Text.Json;

public class TaskService
{
    private List<TodoTask> _tasks = new();
    private int _nextId = 1;
    private const string FilePath = "tasks.json";

    // Add this constructor — runs when TaskService is created
    public TaskService()
    {
        LoadFromFile();
    }

    private void SaveToFile()
{
    string json = JsonSerializer.Serialize(_tasks, new JsonSerializerOptions 
    { 
        WriteIndented = true,
        Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() } // ← add this
    });
    File.WriteAllText(FilePath, json);
}

    private void LoadFromFile()
    {
        if (!File.Exists(FilePath)) return;

        string json = File.ReadAllText(FilePath);
        _tasks = JsonSerializer.Deserialize<List<TodoTask>>(json);

        if (_tasks.Count > 0)
            _nextId = _tasks.Max(t => t.Id) + 1;
    }

    public void AddTask(string title, string description)
    {
        var task = new TodoTask
        {
            Id = _nextId++,
            Title = title,
            Description = description,
            Status = TaskStatus.Pending,
            CreatedAt = DateTime.Now
        };

        _tasks.Add(task);
        SaveToFile();
        Console.WriteLine($"\nTask '{title}' added successfully!");
    }

    public void ViewAll()
    {
        if (_tasks.Count == 0)
        {
            Console.WriteLine("\nNo tasks yet.");
            return;
        }

        Console.WriteLine("\n------- All Tasks -------");
        foreach (var task in _tasks)
        {
            PrintTask(task);
        }
        Console.WriteLine("-------------------------");
    }

    public void CompleteTask(int id)
    {
        var task = FindById(id);

        if (task == null)
        {
            Console.WriteLine("Task not found.");
            return;
        }

        task.Status = TaskStatus.Completed;
        SaveToFile();
        Console.WriteLine($"Task '{task.Title}' marked as complete!");
    }

    public void DeleteTask(int id)
    {
        var task = FindById(id);

        if (task == null)
        {
            Console.WriteLine("Task not found.");
            return;
        }

        _tasks.Remove(task);
        SaveToFile();
        Console.WriteLine($"Task '{task.Title}' deleted.");
    }

    public void ViewByStatus(TaskStatus status)
    {
        var filtered = _tasks.Where(t => t.Status == status).ToList();

        if (filtered.Count == 0)
        {
            Console.WriteLine($"\nNo tasks with status: {status}");
            return;
        }

        Console.WriteLine($"\n------- {status} Tasks -------");
        foreach (var task in filtered)
        {
            PrintTask(task);
        }
    }

    // Private helpers — only used inside this class
    private TodoTask? FindById(int id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }

    private void PrintTask(TodoTask task)
    {
        Console.WriteLine($"[{task.Id}] {task.Title} | {task.Status} | {task.CreatedAt:MM/dd/yyyy}");
        Console.WriteLine($"    {task.Description}");
    }
}