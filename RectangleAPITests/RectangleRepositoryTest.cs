using Microsoft.EntityFrameworkCore;
using RectangleAPI.Data;
using RectangleAPI.Models;
using RectangleAPI.Repositories;
using Xunit;

public class RectangleRepositoryTests
{
    private readonly RectangleRepository _repository;

    public RectangleRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        var context = new ApplicationDbContext(options);
        context.Rectangles.AddRange(new List<Rectangle>
    {
        new Rectangle { Id = 1, X1 = 0, Y1 = 0, X2 = 10, Y2 = 10 },
        new Rectangle { Id = 2, X1 = 5, Y1 = 5, X2 = 15, Y2 = 15 },
        new Rectangle { Id = 3, X1 = 20, Y1 = 20, X2 = 30, Y2 = 30 }
    });
        context.SaveChanges();

        _repository = new RectangleRepository(context);
    }


    [Fact]
    public async Task GetIntersectingRectanglesAsync_FindsIntersectingRectangles()
    {
        var segment = new Segment { X1 = 11, Y1 = 11, X2 = 19, Y2 = 20 };

        var result = await _repository.GetIntersectingRectanglesAsync(segment);

        Assert.Contains(result, r => r.Id == 2);
        Assert.DoesNotContain(result, r => r.Id == 1);
        Assert.DoesNotContain(result, r => r.Id == 3);
    }
}
