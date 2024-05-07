using Microsoft.EntityFrameworkCore;
using RectangleAPI.Models;
using System.Linq;

namespace RectangleAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if there are already rectangles in the database
            if (context.Rectangles.Any())
            {
                return; // The database is already filled
            }

            var rectangles = new Rectangle[]
            {
                new Rectangle { X1 = 0, Y1 = 0, X2 = 10, Y2 = 10 },
                new Rectangle { X1 = 5, Y1 = 5, X2 = 15, Y2 = 15 },
                // Add more rectangles here
            };

            foreach (Rectangle r in rectangles)
            {
                context.Rectangles.Add(r);
            }
            context.SaveChanges();
        }
    }
}
