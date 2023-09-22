using Dropbox.Sign.Model;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using DBS = DropBoxSign;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    public class DropBoxSignController : ControllerBase
    {

        [HttpPost("CreateEmbeddedSignature")]
        public async Task<SignatureRequestGetResponse> CreateSignature([FromBody] SignatureRequestCreateEmbeddedRequest body)
        {
            return await new DBS().CreateSignature(body);
        }

        [HttpGet("GetEmbeddedSignature")]
        public async Task<EmbeddedSignUrlResponse> GetEmbeddedSignURL(string signature_id)
        {
            return await new DBS().GetEmbeddedSignURL(signature_id);
        }

        



    }
}
