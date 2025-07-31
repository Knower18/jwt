using jwt.serveces;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom;

namespace jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AthuController : ControllerBase
    {
        private readonly iathuseves _thuuserviece;
        public AthuController(iathuseves athuserviece)
        {
            this._thuuserviece = athuserviece;
        }
        [HttpPost("register")]
        public  async Task<IActionResult> register( [FromBody] Register register)
        {
            if (!ModelState.IsValid)
            {
                return  BadRequest(ModelState);
            }
            var result = await  _thuuserviece.registerasync(register);
            if (!result.isathu)
            {
                return BadRequest(result.message);

            }
            return Ok(new {token=result.token,expiredate=result.expire});

        }
        [HttpPost("token")]
        public async Task<IActionResult> login([FromBody] Tokenrquenst register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _thuuserviece.GETTOKEN(register);
            if (!result.isathu)
            {
                return BadRequest(result.message);

            }
            return Ok(new { token = result.token, expiredate = result.expire });

        }
    }
}
