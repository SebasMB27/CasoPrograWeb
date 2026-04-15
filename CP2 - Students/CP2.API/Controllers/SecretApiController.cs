using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;

namespace CP2.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecretApiController : ControllerBase
    {

        public SecretApiController()
        {
        }

        [HttpGet(Name = "GetPuzzle")]
        public string Get()
        {
            int key = 3;
            string encrypted = "OLVNRY";
            string result = $"Word: ???\nKey: Shift +{key}\nEncrypted: {encrypted}\nDecrypt Key: Shift -{key}";
            return result;
        }
    }
}
