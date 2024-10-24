using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DyavolskayaKontora.Model;
using DyavolskayaKontora.DB;
using Microsoft.EntityFrameworkCore;

namespace DyavolskayaKontora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaksController : ControllerBase
    {
        private readonly DB.DB db;

        public RaksController(DB.DB db)
        {
            this.db = db;
        }

        [HttpPost("PoluchitOrudiePitki")]
        public async Task<List<Rack>> GetRacks()
        {
            List<Rack> racks = new List<Rack>();
            racks = db.Racks.Include(s=> s.IdDevilNavigation).ToList();
            return racks;
            
        }

        [HttpPost("NewOrudiePitki")]
        public async Task AddRack(Rack rack)
        {
            db.Racks.Add(rack);
            await db.SaveChangesAsync();
        }

        [HttpPost("Kto-taTronulOrudiePitki")]
        public async Task UpdateRack(Rack rack)
        {
            db.Racks.Update(rack);
            await db.SaveChangesAsync();
        }
    }
}