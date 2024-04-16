using System.Globalization;

namespace coding_tracker
{
    internal class UserInput
    {
        private DatabaseController databaseController = new();

        internal void Menu()
        {
            bool closeApp = false;
            while (closeApp == false)
            {
                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\nType 0 to Close Application.");
                Console.WriteLine("Type 1 to View records");
                Console.WriteLine("Type 2 to Add records");
                Console.WriteLine("Type 3 to Delete records");
                Console.WriteLine("Type 4 to Update records");

                var command = Console.ReadLine();

                switch (command)
                {
                    case "0":
                        closeApp = true;
                        break;

                    case "1":
                        Get();
                        break;

                    case "2":
                        Add();
                        break;

                    case "3":
                        ProcessDelete();
                        break;
                }
            }
        }

        private void Get()
        {
            var coding = new CodingSession();

            Console.WriteLine("---------------------------");

            databaseController.Read();

            Console.WriteLine("---------------------------");
        }

        private void Add()
        {
            var coding = new CodingSession();

            var startTime = GetStartEndTime("Please insert the start time in format (hh:mm)");

            coding.StartTime = startTime;

            Console.WriteLine();

            var endTime = GetStartEndTime("Press any key to enter your end time also in format (hh:mm)");
            coding.EndTime = endTime;

            var duration = GetDuration(startTime, endTime);
            coding.Duration = duration;
            Console.WriteLine(duration);

            databaseController.Insert(coding);
        }

        private void ProcessDelete()
        {
            var coding = new CodingSession();

            Console.WriteLine("Please enter the id you want to delete");

            Get();

            var id = Console.ReadLine();

            while (!Int32.TryParse(id, out _) || Convert.ToInt32(id) < 0)
            {
                Console.WriteLine("Id invalid please type another id");
                id = Console.ReadLine();
            }

            coding.Id = Convert.ToInt32(id);

            databaseController.Delete(coding);
        }

        private string GetStartEndTime(string message)
        {
            Console.WriteLine(message);
            var startTimeInput = Console.ReadLine();

            while (!DateTime.TryParseExact(startTimeInput, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                Console.WriteLine("Input not valid please try again or type 0 to go back to main menu");

                startTimeInput = Console.ReadLine();
                if (startTimeInput == "0") Menu();
            }

            return startTimeInput;
        }

        private string GetDuration(string startTime, string endTime)
        {
            var startDateTime = DateTime.ParseExact(startTime, "HH:mm", CultureInfo.InvariantCulture);
            var endDateTime = DateTime.ParseExact(endTime, "HH:mm", CultureInfo.InvariantCulture);
            var duration = endDateTime - startDateTime;

            // Format the duration as a string in the format "hh:mm"
            return $"{(int)duration.TotalHours:D2} hours :{duration.Minutes:D2} minutes";
        }
    }
}