using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
class ToDoList
{
    private static readonly string filePath = "ToDoListSave.bin";

    static void Main()
    {
        Dictionary<string, bool> toDoList = LoadTasks();
        foreach (var task in toDoList) Console.WriteLine($"Task: {task.Key}, Is Done: {task.Value}");

        Console.Write("\n + Add Task\n " +
                      "\n - Delete Task\n " +
                      "\n e Edit Task\n " +
                      "\n c Complete Task\n " +
                      "\nSelect Action: \n");


        Console.Write("\nEnter New Tasks (empty line to finish): \n");
        CreateTask(toDoList);

        Console.Write("\nEdit Tasks? (Y/N): ");
        if (Console.ReadLine().Equals("y", StringComparison.OrdinalIgnoreCase))
        {
            while (true)
            {
                Console.Write("\nEnter Task Name to Toggle: ");
                string task = Console.ReadLine();

                if (toDoList.ContainsKey(task)) toDoList[task] = !toDoList[task];
                else Console.WriteLine("Task not found.");

                Console.Write("Stop Editing? (Y/N): ");
                if (Console.ReadLine().Equals("y", StringComparison.OrdinalIgnoreCase)) break;
            }
        }
        SaveTasks(toDoList);
    }
    private static void CreateTask(Dictionary<string,bool> toDoList)
    {
        string task = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(task))
        {
            toDoList[task] = false;
            CreateTask(toDoList);
        }
    }
    private static void DeleteTask(Dictionary<string, bool> toDoList)
    {
        string task = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(task))
        {
            toDoList.Remove(task);
            DeleteTask(toDoList);
        }
    }
    private static void CompleteTask(Dictionary<string, bool> toDoList)
    {

    }
    private static void EditTask(Dictionary<string, bool> toDoList)
    {

    }

    #region Save/Load Functions
    static Dictionary<string, bool> LoadTasks()
    {
        if (!File.Exists(filePath)) return new Dictionary<string, bool>();
        using FileStream fileStream = new(filePath, FileMode.Open);
        return (Dictionary<string, bool>)new BinaryFormatter().Deserialize(fileStream);
    }
    static void SaveTasks(Dictionary<string, bool> tasks)
    {
        using FileStream fileStream = new(filePath, FileMode.Create);
        BinaryFormatter binaryFormatter = new();
        binaryFormatter.Serialize(fileStream, tasks);
        Console.WriteLine("File Saved");
    }
    #endregion
}
