using RectangleAPI.Models;
using RectangleAPI.Helpers;
using Xunit;

namespace RectangleAPITests;

public class GeometryHelperTests
{
    [Fact]
    public void TestSegmentsDoNotIntersect()
    {
        // Arrange
        Segment segment1 = new Segment { X1 = 1, Y1 = 1, X2 = 3, Y2 = 3 };
        Segment segment2 = new Segment { X1 = 4, Y1 = 4, X2 = 6, Y2 = 6 };

        // Act
        bool result = GeometryHelper.DoSegmentsIntersect(segment1, segment2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TestSegmentsIntersect()
    {
        // Arrange
        Segment segment1 = new Segment { X1 = 1, Y1 = 1, X2 = 4, Y2 = 4 };
        Segment segment2 = new Segment { X1 = 1, Y1 = 4, X2 = 4, Y2 = 1 };

        // Act
        bool result = GeometryHelper.DoSegmentsIntersect(segment1, segment2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestSegmentsIntersectAtEndpoint()
    {
        // Arrange
        Segment segment1 = new Segment { X1 = 1, Y1 = 1, X2 = 2, Y2 = 2 };
        Segment segment2 = new Segment { X1 = 2, Y1 = 2, X2 = 3, Y2 = 3 };

        // Act
        bool result = GeometryHelper.DoSegmentsIntersect(segment1, segment2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestRectangleIntersectingSegment()
    {
        // Arrange
        Rectangle rectangle = new Rectangle { X1 = 1, Y1 = 1, X2 = 3, Y2 = 3 };
        Segment segment = new Segment { X1 = 2, Y1 = 0, X2 = 2, Y2 = 4 };

        // Act
        bool result = GeometryHelper.IsRectangleIntersectingSegment(rectangle, segment);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestRectangleNotIntersectingSegment()
    {
        // Arrange
        Rectangle rectangle = new Rectangle { X1 = 1, Y1 = 1, X2 = 3, Y2 = 3 };
        Segment segment = new Segment { X1 = 4, Y1 = 4, X2 = 5, Y2 = 5 };

        // Act
        bool result = GeometryHelper.IsRectangleIntersectingSegment(rectangle, segment);

        // Assert
        Assert.False(result);
    }
}