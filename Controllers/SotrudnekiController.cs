using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DyavolskayaKontora.Model;
using DyavolskayaKontora.DB;

namespace DyavolskayaKontora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SotrudnekiController : ControllerBase
    {
        private readonly DB.DB dB;

        public SotrudnekiController(DB.DB db)
        { 
            this.dB = db;
        }

        [HttpPost("НоваяКровь")]
        public async void AddSotrudnek(Devil sotrudnek)
        {
            dB.Sotrudneki.Add(sotrudnek); 
            await dB.SaveChangesAsync();
        }

        [HttpPost("ЧотаИзменилось")]
        public async void UpdateSotrudnek(Devil sotrudnek)
        {
            dB.Sotrudneki.Update(sotrudnek);
            await dB.SaveChangesAsync();
        }

        [HttpPost("БольшеНеНашаКровь")]
        public async Task<IActionResult> EraseSotrudnek(int devilId, string name, int date) 
        {
            var sotrednek = await dB.Sotrudneki.FindAsync(devilId, name, date);
            if (sotrednek == null)
            {
                return NotFound("Дьявольская жопа не найдена");
            }
            var dispose = new Disposal
            {
                Id = devilId, Title = name, Year = date
            };
           dB.Disposals.Add(dispose);
            dB.Sotrudneki.Remove(sotrednek);

            await dB.SaveChangesAsync();

            return Ok("Чорт найден и устранён");

        }
    }
}
