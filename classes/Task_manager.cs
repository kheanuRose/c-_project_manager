using System;
using System.Collections.Generic;
using System.IO;

class Task_manager : Task{

    //filepath variable
    string path = "project_details.txt";
    private List<string> task_details = new List<string>(); // Declare the task_details list as a class-level field

    public Task_manager(string Title, string Description, string Due_date, int Priority,string Status)
        : base(Title, Description, Due_date, Priority,Status)
    {
        // You can add additional initialization logic here for the Task_manager class
    }

    public void Addtask(){

        int number_of_projects;
        // Validate the number of projects input
        while (true)
        {
            Console.Write("How many projects do you plan on working on: ");
            if (int.TryParse(Console.ReadLine(), out number_of_projects) && number_of_projects > 0)
            {
                break; // Exit the loop if the input is a positive integer
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a positive integer.");
            }
        }
        
        // Open the file for appending
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            for (int i = 0; i < number_of_projects; i++)
            {
                Guid projectId = Guid.NewGuid(); // Generate a unique GUID for the project

                Console.WriteLine($"\nProject {i + 1}");
                Console.WriteLine($"Project ID: {projectId}");
                Console.WriteLine("Enter Project title:");
                Title = Console.ReadLine() ?? "";

                Console.WriteLine("Enter project description: ");
                Description = Console.ReadLine() ?? "";

                Console.WriteLine("Enter project due date: ");
                Due_date = Console.ReadLine() ?? "";

                int Priority;
                while (true)
                {
                    Console.WriteLine("Enter project priority: ");
                    if (int.TryParse(Console.ReadLine(), out Priority))
                    {
                        break; // Exit the loop if the input is an integer
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter an integer.");
                    }
                }

                Console.WriteLine("Enter project Status: ");
                Status = Console.ReadLine() ?? "";

                // Add task details to the task_details list
                task_details.Add($"Project {i + 1}:");
                task_details.Add($"Project ID: {projectId}");
                task_details.Add("Title: " + Title);
                task_details.Add("Description: " + Description);
                task_details.Add("Due Date: " + Due_date);
                task_details.Add("Priority: " + Priority.ToString());
                task_details.Add("Project Status: "+ Status);

                // Write the task details to the file for each task
                writer.WriteLine("\nProject " + (i + 1) + ":");
                writer.WriteLine($"Project ID: {projectId}");
                writer.WriteLine("Title: " + Title);
                writer.WriteLine("Description: " + Description);
                writer.WriteLine("Due Date: " + Due_date);
                writer.WriteLine("Priority: " + Priority.ToString());
                writer.WriteLine("PRoject Status: " + Status);
                writer.WriteLine("\n"); // Add a blank line for readability
            }
        }

        //displaying project information in the file
        Console.WriteLine("\nData added to file successfully");

        Console.WriteLine("Project Information: ");
        string content = File.ReadAllText(path);
        Console.WriteLine(content);
    }


   public void Delete_task(){
        Console.WriteLine("Enter the ID of the project you want to delete: ");
        Guid projectId;
        while (!Guid.TryParse(Console.ReadLine(), out projectId))
        {
            Console.WriteLine("Invalid input. Please enter a valid Project ID.");
        }

        bool found = false;
        for (int i = 0; i < task_details.Count; i++)
        {
            if (task_details[i].Contains($"Project ID: {projectId}"))
            {
                task_details.RemoveRange(i - 1, 7);
                found = true;
                break;
            }
        }

        if (found)
        {
            File.WriteAllLines(path, task_details);
            Console.WriteLine($"Project with ID {projectId} has been deleted.");
        }
        else
        {
            Console.WriteLine($"Project with ID {projectId} not found.");
        }
    }

    public void Update_task()
    {
        Console.WriteLine("Enter the ID of the project you want to update: ");
        Guid projectId;
        while (!Guid.TryParse(Console.ReadLine(), out projectId))
        {
            Console.WriteLine("Invalid input. Please enter a valid Project ID.");
        }

        bool found = false;
        for (int i = 0; i < task_details.Count; i++)
        {
            if (task_details[i].Contains($"Project ID: {projectId}"))
            {
                found = true;
                bool doneUpdating = false;
                while (!doneUpdating)
                {
                    Console.WriteLine("\nSelect property to update:");
                    Console.WriteLine("1. Title");
                    Console.WriteLine("2. Description");
                    Console.WriteLine("3. Due Date");
                    Console.WriteLine("4. Priority");
                    Console.WriteLine("5. Status");
                    Console.WriteLine("6. Done updating");

                    Console.Write("Enter your choice: ");
                    int choice;
                    while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 6)
                    {
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                    }

                    if (choice == 6)
                    {
                        doneUpdating = true;
                        continue;
                    }

                    Console.Write("Enter new value: ");
                    string newValue = Console.ReadLine() ?? "";

                    switch (choice)
                    {
                        case 1:
                            task_details[i + 2] = "Title: " + newValue;
                            break;
                        case 2:
                            task_details[i + 3] = "Description: " + newValue;
                            break;
                        case 3:
                            task_details[i + 4] = "Due Date: " + newValue;
                            break;
                        case 4:
                            task_details[i + 5] = "Priority: " + newValue;
                            break;
                        case 5:
                            task_details[i + 6] = "Status: " + newValue;
                            break;
                    }
                }

                File.WriteAllLines(path, task_details);
                Console.WriteLine($"Properties updated successfully for Project with ID {projectId}.");
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine($"Project with ID {projectId} not found.");
        }

        Console.WriteLine("\nProject information after Updates: ");
        string content = File.ReadAllText(path);
        Console.WriteLine(content);
    }

}