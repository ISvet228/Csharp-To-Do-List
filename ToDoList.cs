﻿using System;
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

        Console.Write("\nEnter New Tasks (empty line to finish): \n");
        while (true)
        {
            string task = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(task)) break;
            toDoList[task] = false;
        }

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

    static Dictionary<string, bool> LoadTasks()
    {
        if (!File.Exists(filePath)) return new Dictionary<string, bool>();
        using (FileStream fileStream = new FileStream(filePath, FileMode.Open)) return (Dictionary<string, bool>)new BinaryFormatter().Deserialize(fileStream);
    }

    static void SaveTasks(Dictionary<string, bool> tasks)
    {
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create)) new BinaryFormatter().Serialize(fileStream, tasks);
        Console.WriteLine("File Saved");
    }
}