﻿using Domain;
using Microsoft.Extensions.DependencyInjection;
namespace Persistence
{
    public static class Seed
    {
        public static async Task SeedData(this IServiceCollection services)
        {

            var context = services.BuildServiceProvider().GetRequiredService<DataContext>();
            if (!context.Activities.Any())
            {

                var activities = new List<Activity>
                {
                    new Activity
                    {
                        Title = "Past Activity 1",
                        CreateData = DateTime.Now.AddMonths(-2),
                        Description = "Activity 2 months ago",
                        Category = "drinks",
                        City = "London",
                        Venue = "Pub",

                    },
                    new Activity
                    {
                        Title = "Past Activity 2",
                        CreateData = DateTime.Now.AddMonths(-1),
                        Description = "Activity 1 month ago",
                        Category = "culture",
                        City = "Paris",
                        Venue = "The Louvre",

                    },
                    new Activity
                    {
                        Title = "Future Activity 1",
                        CreateData = DateTime.Now.AddMonths(1),
                        Description = "Activity 1 month in future",
                        Category = "music",
                        City = "London",
                        Venue = "Wembly Stadium",

                    },
                    new Activity
                    {
                        Title = "Future Activity 2",
                        CreateData = DateTime.Now.AddMonths(2),
                        Description = "Activity 2 months in future",
                        Category = "food",
                        City = "London",
                        Venue = "Jamies Italian",

                    },
                    new Activity
                    {
                        Title = "Future Activity 3",
                        CreateData = DateTime.Now.AddMonths(3),
                        Description = "Activity 3 months in future",
                        Category = "drinks",
                        City = "London",
                        Venue = "Pub",

                    },
                    new Activity
                    {
                        Title = "Future Activity 4",
                        CreateData = DateTime.Now.AddMonths(4),
                        Description = "Activity 4 months in future",
                        Category = "culture",
                        City = "London",
                        Venue = "British Museum",

                    },
                    new Activity
                    {
                        Title = "Future Activity 5",
                        CreateData = DateTime.Now.AddMonths(5),
                        Description = "Activity 5 months in future",
                        Category = "drinks",
                        City = "London",
                        Venue = "Punch and Judy",

                    },
                    new Activity
                    {
                        Title = "Future Activity 6",
                        CreateData = DateTime.Now.AddMonths(6),
                        Description = "Activity 6 months in future",
                        Category = "music",
                        City = "London",
                        Venue = "O2 Arena",

                    },
                    new Activity
                    {
                        Title = "Future Activity 7",
                        CreateData = DateTime.Now.AddMonths(7),
                        Description = "Activity 7 months in future",
                        Category = "travel",
                        City = "Berlin",
                        Venue = "All",

                    },
                    new Activity
                    {
                        Title = "Future Activity 8",
                        CreateData = DateTime.Now.AddMonths(8),
                        Description = "Activity 8 months in future",
                        Category = "drinks",
                        City = "London",
                        Venue = "Pub"
                    }
                };

                await context.Activities.AddRangeAsync(activities);
                await context.SaveChangesAsync();
            }
        }
    }
}