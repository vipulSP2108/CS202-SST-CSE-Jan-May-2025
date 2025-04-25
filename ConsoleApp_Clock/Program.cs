using System;
using System.Timers;

namespace ConsoleApp_Clock
{
    // Delegate for the alarm event
    public delegate void AlarmEventHandler(object sender, EventArgs e);

    // Publisher class
    class AlarmClock
    {
        // User-defined event with required modifier to handle non-nullable warning
        public event AlarmEventHandler raiseAlarm;

        private DateTime targetTime;
        private System.Timers.Timer timer; // Fully qualified name to resolve ambiguity

        public AlarmClock(string timeInput)
        {
            if (timeInput == null) // Null check for timeInput
            {
                Console.WriteLine("Time input cannot be null. Please use HH:MM:SS.");
                return;
            }

            // Parse user input time
            if (DateTime.TryParse(timeInput, out targetTime))
            {
                // Set target time to today with user-specified time
                targetTime = DateTime.Today.Add(targetTime.TimeOfDay);
                if (targetTime < DateTime.Now) // If time is in the past, set for tomorrow
                    targetTime = targetTime.AddDays(1);

                // Initialize timer to check every second
                timer = new System.Timers.Timer(1000);
                timer.Elapsed += CheckTime;
                timer.AutoReset = true;
                timer.Start();
                Console.WriteLine($"Alarm set for {targetTime:HH:mm:ss}");
            }
            else
            {
                Console.WriteLine("Invalid time format. Please use HH:MM:SS.");
            }
        }

        private void CheckTime(object sender, ElapsedEventArgs e)
        {
            // Check if current time matches target time
            if (DateTime.Now.Hour == targetTime.Hour &&
                DateTime.Now.Minute == targetTime.Minute &&
                DateTime.Now.Second == targetTime.Second)
            {
                // Raise the alarm event
                raiseAlarm?.Invoke(this, EventArgs.Empty);
                timer.Stop(); // Stop the timer after alarm
            }
        }
    }

    // Subscriber class
    class AlarmSubscriber
    {
        public void Subscribe(AlarmClock clock)
        {
            // Subscribe to the raiseAlarm event
            clock.raiseAlarm += Ring_alarm;
        }

        private void Ring_alarm(object sender, EventArgs e)
        {
            Console.WriteLine("Alarm! Time's up!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter alarm time (HH:MM:SS): ");
            string timeInput = Console.ReadLine();

            // Create publisher and subscriber
            AlarmClock alarmClock = new AlarmClock(timeInput ?? string.Empty); // Handle null with empty string
            AlarmSubscriber subscriber = new AlarmSubscriber();

            // Subscribe to the event
            subscriber.Subscribe(alarmClock);

            // Keep the console running
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}