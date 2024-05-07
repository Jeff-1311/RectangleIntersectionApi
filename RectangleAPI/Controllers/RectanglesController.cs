using Microsoft.AspNetCore.Mvc;
using RectangleAPI.Models;
using RectangleAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RectangleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RectanglesController : ControllerBase
    {
        private readonly RectangleRepository _repository;

        public RectanglesController(RectangleRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("intersect")]
        public async Task<ActionResult<IEnumerable<Rectangle>>> GetIntersectingRectangles([FromBody] Segment segment)
        {
            var rectangles = await _repository.GetIntersectingRectanglesAsync(segment);
            return Ok(rectangles);
        }
    }
}
