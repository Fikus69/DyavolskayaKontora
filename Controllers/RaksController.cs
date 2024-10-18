using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DyavolskayaKontora.Model;
using DyavolskayaKontora.DB;

namespace DyavolskayaKontora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaksController : ControllerBase
    {
        private readonly DB.DB dB;

        public RaksController(DB.DB db)
        {
            this.dB = db;
        }

        [HttpPost("НовоеОрудиеПыток")]
        public async void AddRack(Rack rack)
        {
            dB.Racks.Add(rack);
            await dB.SaveChangesAsync();
        }

        [HttpPost("КтотаТронулДыбу")]
        public async void UpdateRack(Rack rack)
        {
            dB.Racks.Update(rack);
            await dB.SaveChangesAsync();
        }
    }
}