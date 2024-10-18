using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DyavolskayaKontora.Model;
using DyavolskayaKontora.DB;

namespace DyavolskayaKontora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisposalController : ControllerBase
    {
        private readonly DB.DB dB;

        public DisposalController(DB.DB db)
        {
            this.dB = db;
        }

        [HttpPost("НеПригодноДляПыток")]
        public async Task<IActionResult> EraseRack(int rackId, string name, int date)
        {
            var sotrednek = await dB.Sotrudneki.FindAsync(rackId, name, date);
            if (sotrednek == null)
            {
                return NotFound("Орудие пыток не обнаружено");
            }
            var dispose = new Disposal
            {
                Id = rackId,
                Title = name,
                Year = date
            };
            dB.Disposals.Add(dispose);
            dB.Sotrudneki.Remove(sotrednek);

            await dB.SaveChangesAsync();

            return Ok("Орудие пыток утилизировано");

        }
    }
}
