using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.IO;
using System;

class ToDoList
{
    private static readonly string filePath = "ToDoList.sus";
    private static List<ToDoTask> toDoList = new();
    private static int nextID = 1;

    private static void Main()
    {
        LoadTasks();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Create Task" +
                "\n2. Read Tasks" +
                "\n3. Update Task" + 
                "\n4. Delete Task" +
                "\n5. End Programm");
            Console.Write("Enter Action Number: ");

            string action = Console.ReadLine();

            switch (action)
            {
                case "1": CreateTask(); break;
                case "2": ReadTasks(); break;
                case "3": UpdateTask(); break;
                case "4": DeleteTask(); break;
                case "5": SaveTasks(); return;
                default: Console.WriteLine("Incorrect Input!!!!"); break;
            }
        }
    }

    #region CRUD
    private static void CreateTask()
    {
        Console.Write("\nEnter Task Name: ");
        string taskName = Console.ReadLine();
        toDoList.Add(new ToDoTask { Id = nextID++, TaskName = taskName });
        Console.WriteLine("Task Added. Press Any Key...");
        Console.ReadKey();
    }
    private static void ReadTasks()
    {
        Console.WriteLine("\nTask List: ");
        foreach (var task in toDoList) Console.WriteLine($"{task.Id}. {task.TaskName}");
        Console.WriteLine("Press Any Key...");
        Console.ReadKey();
    }
    private static void UpdateTask()
    {
        Console.Write("Enter Task ID: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var task = toDoList.Find(TASK => TASK.Id == id);
            if (task != null)
            {
                Console.Write("Enter New Name: ");
                task.TaskName = Console.ReadLine();
                Console.WriteLine("Task Updated");
            }
            else Console.WriteLine("Task Not Found");
        }
        else Console.WriteLine("Incorrect ID");
        Console.WriteLine("Task Added. Press Any Key...");
        Console.ReadKey();
    }
    private static void DeleteTask()
    {
        Console.Write("Enter Task ID To Delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var task = toDoList.Find(TASK => TASK.Id == id);
            if (task != null)
            {
                toDoList.Remove(task);
                Console.WriteLine("Task Deleted");
            }
            else Console.WriteLine("Task Not Found");
        }
        else Console.WriteLine("Incorrect ID");
        Console.WriteLine("Task Added. Press Any Key...");
        Console.ReadKey();
    }
#endregion

    #region Save/Load Functions
    static void SaveTasks()
    {
        using FileStream fileStream = new(filePath, FileMode.Create);
        BinaryFormatter binaryFormatter = new();
        binaryFormatter.Serialize(fileStream, toDoList);
    }
    static void LoadTasks()
    {
        if (File.Exists(filePath))
        {
            try
            {
                using FileStream fileStream = new(filePath, FileMode.Open);
                BinaryFormatter binaryFormatter = new();
                toDoList = (List<ToDoTask>)binaryFormatter.Deserialize(fileStream);
                nextID = toDoList.Count > 0 ? toDoList[^1].Id + 1 : 1;
            }
            catch(Exception exeption)
            {
                Console.WriteLine($"Error: {exeption.Message}");
                toDoList = new();
                nextID = 1;
            }
        }
    }
    #endregion
}
[Serializable]
class ToDoTask
{
    public int Id;
    public string TaskName;
}
