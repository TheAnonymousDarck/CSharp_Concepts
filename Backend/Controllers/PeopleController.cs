using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {

        private IPeopleService _peopleService;
        
        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = new PeopleService();
        }
        
        [HttpGet("all")]
        public List<People> GetPeople() => RepositoryPople.People;

        [HttpGet("{id}")]
        public ActionResult<People> Get(int id)
        {
            var people = RepositoryPople.People.FirstOrDefault( p => p.Id == id);

            if (people == null)
            {
                return NotFound();
            }
            return Ok(people);
            
        }
        
        [HttpGet("search/{search}")]
        public List<People> Get(string search) => RepositoryPople.People.Where ( p => p.Name.ToUpper().Contains(search.ToUpper()) ).ToList();

        [HttpPost]
        public IActionResult Add(People people)
        {
            if (!_peopleService.Validate(people))
            {
                return BadRequest();
            }
            RepositoryPople.People.Add(people);
            return NoContent();
        }
    }
}

public class RepositoryPople
{
    public static List<People> People = new List<People>
    {
        new People { Id = 1, Name = "John Doe" },
        new People { Id = 2, Name = "Jane Doe" },
        new People { Id = 3, Name = "John Smith" },
        new People { Id = 4, Name = "Jane Smith" }
    };

}

public class People
{
    public int Id { get; set; }
    public string Name { get; set; }
}
