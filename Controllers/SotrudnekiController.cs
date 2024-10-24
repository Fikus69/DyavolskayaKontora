using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DyavolskayaKontora.Model;
using DyavolskayaKontora.DB;
using Microsoft.EntityFrameworkCore;

namespace DyavolskayaKontora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SotrudnekiController : ControllerBase
    {
        private readonly DB.DB db;

        public SotrudnekiController(DB.DB db)
        { 
            this.db = db;
        }


        [HttpPost("PoluchitBloods")]
        public async Task<List<Devil>> GetSotrudneki()
        {
            List<Devil> devils = new List<Devil>();
            devils = db.Devils.Include(s => s.Racks).ToList();
            return devils;
        }

        [HttpPost("NewBlood")]
        public async Task AddSotrudnek(Devil sotrudnek)
        {
            db.Devils.Add(sotrudnek); 
            await db.SaveChangesAsync();
        }

        [HttpPost("Cho-taIzmenilos")]
        public async Task UpdateSotrudnek(Devil sotrudnek)
        {
            db.Devils.Update(sotrudnek);
            await db.SaveChangesAsync();
        }

        [HttpPost("BolsheNeNashaBlood")]
        public async Task<IActionResult> EraseSotrudnek(int devilId, string name, int date) 
        {
            var sotrednek = await db.Devils.FindAsync(devilId, name, date);
            if (sotrednek == null)
            {
                return NotFound("Дьявольская жопа не найдена");
            }
            var dispose = new Disposal
            {
                Id = devilId, Title = name, Year = date
            };
            db.Disposals.Add(dispose);
            db.Devils.Remove(sotrednek);

            await db.SaveChangesAsync();

            return Ok("Чорт найден и устранён");

        }
    }
}
