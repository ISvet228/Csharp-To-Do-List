using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
class Program
{
    private static readonly string filePath = "data.bin";
    static void Main()
    {
        Dictionary<string, bool> ToDoList = new Dictionary<string, bool>();

        if (File.Exists(filePath))
        {
            FileStream firstFileStream = new FileStream(filePath, FileMode.Open);
            BinaryFormatter FirstFormater = new BinaryFormatter();
            ToDoList = (Dictionary<string, bool>)FirstFormater.Deserialize(firstFileStream);
            foreach (var task in ToDoList) Console.WriteLine($"Task: {task.Key}, Is Done: {task.Value}");
        }

        Console.WriteLine($"\nEnter Number Of New Tasks: ");
        int TaskCount = Convert.ToInt32(Console.ReadLine());

        if (TaskCount > 0)
        {
            Console.WriteLine($"\nEnter New Tasks: ");
            for (int i = 0; i < TaskCount; i++) ToDoList.Add(Console.ReadLine(), false);
        }

        Console.WriteLine($"\nEdit Some Tasks?(Y, N)");
        string Edit = Console.ReadLine();

        if (Edit.ToLower() == "y")
        {
            string Stop = "";
            while (Stop.ToLower() != "y")
            {
                Console.WriteLine($"\nEnter Task Name: ");
                Edit = Console.ReadLine();
                if (!ToDoList.ContainsKey(Edit)) return;
                ToDoList[Edit] = !ToDoList[Edit];
                Console.WriteLine($"\nStop Editing?(Y, N)");
                Stop = Console.ReadLine();
            }
        }

        using (FileStream SecondFileStream = new FileStream(filePath, FileMode.Create))
        {
            BinaryFormatter SecondFormatter = new BinaryFormatter();
            SecondFormatter.Serialize(SecondFileStream, ToDoList);
            Console.WriteLine("Файл сохранён.");
        }
    }
}