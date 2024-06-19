using System;
using System.IO;

namespace project
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string path = "project_details.txt";
            Task_manager manager = new Task_manager("", "", "", 0, "");

            Console.WriteLine("Simple CLI task Manager\n");
            bool exitProgram = false;

            do
            {
                int choice = 0;
                bool isValidInput = false;

                while (!isValidInput)
                {
                    Console.WriteLine("ENTER 1-ADD TASK\n2-DELETE TASK\n3-UPDATE TASK");
                    string input = Console.ReadLine() ?? "";

                    // Try to parse the input string to an integer
                    isValidInput = int.TryParse(input, out choice);

                    if (!isValidInput)
                    {
                        Console.WriteLine("\nPlease enter a valid integer.");
                    }
                    else if (choice < 1 || choice > 3)
                    {
                        Console.WriteLine("Invalid entry. Please enter a number between 1 and 3.\n");
                        isValidInput = false; // Reset isValidInput to continue the loop
                    }
                    else
                    {
                        switch (choice)
                        {
                            case 1:
                                // Keep asking the user to add a task until the file is created
                                while (!File.Exists(path))
                                {
                                    manager.Addtask(); // Add a task
                                }
                                if (File.Exists(path)){
                                    manager.Addtask();
                                }
                                break;
                            case 2:
                                if (!File.Exists(path))
                                {
                                    Console.WriteLine("Text file doesn't exist. Add a task first in order to create the text file\n");
                                    isValidInput = false; // Reset isValidInput to continue the loop
                                }
                                else
                                {
                                    manager.Delete_task();
                                }
                                break;
                            case 3:
                                if (!File.Exists(path))
                                {
                                    Console.WriteLine("Text file doesn't exist. Add a task first in order to create the text file\n");
                                    isValidInput = false; // Reset isValidInput to continue the loop
                                }
                                else
                                {
                                    manager.Update_task();
                                }
                                break;
                        }
                    }
                }

                Console.WriteLine("Do you want to choose again? (Y/N)");
                string restartInput = Console.ReadLine() ?? "";
                if (restartInput != "Y" && restartInput != "y"){
                    exitProgram = true;
                }
            } while (!exitProgram);
        }
    }
}
