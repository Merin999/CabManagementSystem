using CabAPI.Data;
using Microsoft.AspNetCore.Http;    
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CabAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public CabController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<Cab>> Get()
        {
            return await db.Cabs.ToListAsync();
        }

        [HttpPost]
        public async Task<Cab> Post(Cab movie)
        {
            db.Cabs.Add(movie);
            await db.SaveChangesAsync();
            return movie;
        }
    }
}
