using Microsoft.EntityFrameworkCore;
using RectangleAPI.Data;
using RectangleAPI.Helpers;
using RectangleAPI.Models;

namespace RectangleAPI.Repositories
{
    public class RectangleRepository
    {
        private readonly ApplicationDbContext _context;

        public RectangleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rectangle>> GetIntersectingRectanglesAsync(Segment segment)
        {
            // Search for rectangles where one of the corners or sides may intersect with the segment
            var possibleRectangles = await _context.Rectangles
                .Where(r => (r.X1 <= segment.X2 && r.X2 >= segment.X1) && (r.Y1 <= segment.Y2 && r.Y2 >= segment.Y1))
                .ToListAsync();

            // Filter those that actually intersect
            return possibleRectangles.Where(rect => GeometryHelper.IsRectangleIntersectingSegment(rect, segment)).ToList();
        }
    }
}
