using ConvertIntoWords.Models;
using ConvertIntoWords.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConvertIntoWords.Controllers
{
    [Route("convert")]
    [ApiController]
    public class ConvertIntoWordsController : ControllerBase
    {
        private readonly IEnConvertIntoWordsService enConvertIntoWordsService;

        public ConvertIntoWordsController(IEnConvertIntoWordsService enConvertIntoWordsService)
        {
            this.enConvertIntoWordsService = enConvertIntoWordsService ?? throw new ArgumentNullException(nameof(enConvertIntoWordsService));
        }

        [HttpPost]
        public IActionResult ConvertIntoWords([FromBody] NumberDto number)
        {
            var result = enConvertIntoWordsService.Convert(number.Number);

            if (result.IsSuccess)
            {
                return Ok(new { Result = result.ResultValue });
            }

            return BadRequest(new { Result = result.ResultValue });
        }
    }
}
