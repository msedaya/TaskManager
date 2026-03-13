
Console.WriteLine("\n===== Task Manager =====\n");
TaskService taskService = new TaskService();
Console.Write("What is your name: ");
string name = Console.ReadLine() ?? "No-Name";
bool running = true;

while (running)
{
    Console.WriteLine("1. Add Task");
    Console.WriteLine("2. View All Task");
    Console.WriteLine("3. Complete Task");
    Console.WriteLine("4. Delete Task");
    Console.WriteLine("5. View By Status");
    Console.WriteLine("6. Exit");

    Console.Write("\nChoose in opition: \n");
    string input = Console.ReadLine() ?? "0";

    switch (input)
    {
        case "1":
            Console.Write("Enter Title of Task: ");
            string title = Console.ReadLine() ?? "No title";
            Console.Write("Enter Description : ");
            string description = Console.ReadLine() ?? "No description";
            
            taskService.AddTask(title,description);


            
            break;
        case "2":
            taskService.ViewAll();
            break;
        case "3":
            {
                taskService.ViewAll();
            Console.Write("Enter Id of task to complete: ");
            string id = Console.ReadLine() ?? "0";

            if (!int.TryParse(id, out int idInt) || idInt <= 0)
            {
                Console.WriteLine("Invalid Id.");
                continue;
            }
            taskService.CompleteTask(idInt);

            break;
            }
        case "4":
            {
                taskService.ViewAll();
            Console.Write("Enter Id of task to delete: ");
            string id = Console.ReadLine() ?? "0";

            if (!int.TryParse(id, out int idInt) || idInt <= 0)
            {
                Console.WriteLine("Invalid Id.");
                continue;
            }
            taskService.DeleteTask(idInt);
            break;
            }
        case "5":
            Console.WriteLine("1. Pending");
            Console.WriteLine("2. In Progress");
            Console.WriteLine("3. Completed");
            string statusChoice = Console.ReadLine() ?? "0";

            TaskStatus status = TaskStatus.Pending;

            switch (statusChoice)
            {
                case "1":
                    status = TaskStatus.Pending;
                    break;
                case "2":
                    status = TaskStatus.InProgress;
                    break;
                case "3":
                    status = TaskStatus.Completed;
                    break;
                default:
                    Console.WriteLine("Invalid Choice.");
                    break;
            }
            taskService.ViewByStatus(status);

            break;
        case "6":
            Console.WriteLine("Thank you!. Goodbye.");
            running = false;
            break;
        default:
            Console.WriteLine("Invalid option try again");
            break;
    }
}


