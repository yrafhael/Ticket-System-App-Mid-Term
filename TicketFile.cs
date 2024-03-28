public class TicketFile
    {
        private string ticketsFilePath;
        private string enhancementsFilePath;
        private string tasksFilePath;

        public TicketFile(string ticketsFilePath, string enhancementsFilePath, string tasksFilePath)
        {
            this.ticketsFilePath = ticketsFilePath;
            this.enhancementsFilePath = enhancementsFilePath;
            this.tasksFilePath = tasksFilePath;
        }

        public void ReadDataFromTicketsFile()
        {
            ReadFromFile(ticketsFilePath);
        }

        public void ReadDataFromEnhancementsFile()
        {
            ReadFromFile(enhancementsFilePath);
        }

        public void ReadDataFromTasksFile()
        {
            ReadFromFile(tasksFilePath);
        }

        public void CreateFileFromData()
        {
            string choice;
            do
            {
                Console.WriteLine("Enter TicketID:");
                int ticketID = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter Summary:");
                string summary = Console.ReadLine();

                Console.WriteLine("Enter Status:");
                string status = Console.ReadLine();

                Console.WriteLine("Enter Priority:");
                string priority = Console.ReadLine();

                Console.WriteLine("Enter Submitter:");
                string submitter = Console.ReadLine();

                Console.WriteLine("Enter Assigned:");
                string assigned = Console.ReadLine();

                Console.WriteLine("Enter Watching (separate names with commas):");
                string watching = Console.ReadLine();

                Console.WriteLine("Enter Severity:");
                string severity = Console.ReadLine();

                Console.WriteLine("Is this an Enhancement or Task ticket? (E/T)");
                choice = Console.ReadLine().ToUpper();

                if (choice == "E")
                {
                    Console.WriteLine("Enter Software:");
                    string software = Console.ReadLine();

                    Console.WriteLine("Enter Cost:");
                    double cost = double.Parse(Console.ReadLine());

                    Console.WriteLine("Enter Reason:");
                    string reason = Console.ReadLine();

                    Console.WriteLine("Enter Estimate (in h:m:s format):");
                    TimeSpan estimate = TimeSpan.Parse(Console.ReadLine());

                    // Write to Enhancements.csv
                    using (StreamWriter enhancementsWriter = new StreamWriter(enhancementsFilePath, append: true))
                    {
                        enhancementsWriter.WriteLine($"{ticketID},{summary},{status},{priority},{submitter},{assigned},{watching},{severity},{software},{cost},{reason},{estimate}");
                    }
                }
                else if (choice == "T")
                {
                    Console.WriteLine("Enter Project Name:");
                    string projectName = Console.ReadLine();

                    Console.WriteLine("Enter Due Date (YYYY-MM-DD):");
                    DateTime dueDate = DateTime.Parse(Console.ReadLine());

                    // Write to Tasks.csv
                    using (StreamWriter tasksWriter = new StreamWriter(tasksFilePath, append: true))
                    {
                        tasksWriter.WriteLine($"{ticketID},{summary},{status},{priority},{submitter},{assigned},{watching},{severity},{projectName},{dueDate:yyyy-MM-dd}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter 'E' for Enhancement or 'T' for Task.");
                }

                // Write to Tickets.csv
                using (StreamWriter sw = new StreamWriter(ticketsFilePath, append: true))
                {
                    sw.WriteLine($"{ticketID},{summary},{status},{priority},{submitter},{assigned},{watching},{severity}");
                }

                Console.WriteLine("Do you want to add another ticket? (Y/N)");
                choice = Console.ReadLine().ToUpper();
            } while (choice == "Y");
        }

        private void ReadFromFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            Console.WriteLine(line);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("File does not exist");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
            }
        }
    }