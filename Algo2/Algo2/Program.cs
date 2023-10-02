namespace Algo2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Activity
    {
        public string Name { get; set; }
        public int Start { get; set; }
        public int End { get; set; }

        public Activity(string name, int start, int end)
        {
            Name = name;
            Start = start;
            End = end;
        }
    }

    class Program
    {
        static List<Activity> FindMaxNonConflictActivities(List<Activity> activities)
        {
            List<Activity> result = new List<Activity>();

            // Сортираме дейностите по време на завършване
            activities = activities.OrderBy(a => a.End).ToList();

            // Избираме първата дейност
            Activity currentActivity = activities[0];
            result.Add(currentActivity);

            // Намираме следващите неконфликтни дейности
            for (int i = 1; i < activities.Count; i++)
            {
                if (activities[i].Start >= currentActivity.End)
                {
                    currentActivity = activities[i];
                    result.Add(currentActivity);
                }
            }

            return result;
        }

        static void Main(string[] args)
        {
            List<Activity> activities = new List<Activity>
        {
            new Activity("a1", 1, 3),
            new Activity("a2", 0, 4),
            new Activity("a3", 1, 2),
            new Activity("a4", 4, 6),
            new Activity("a5", 2, 9),
            new Activity("a6", 5, 8),
            new Activity("a7", 3, 5),
            new Activity("a8", 4, 5)
        };

            List<Activity> maxNonConflictActivities = FindMaxNonConflictActivities(activities);

            Console.WriteLine("Максимални неконфликтни дейности:");
            foreach (var activity in maxNonConflictActivities)
            {
                Console.WriteLine($"Дейност {activity.Name} Начало: {activity.Start} Край: {activity.End}");
            }
        }
    }
}