using backend.DTOs;
using Dropbox.Sign.Model;
using Microsoft.AspNetCore.Mvc;
using DBS = DropBoxSign;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    public class DropBoxSignController : ControllerBase
    {
        [HttpGet("GetAccount")]
        public async Task<AccountGetResponse> GetAccount()
        {
            return await new DBS().AccountGet();
        }

        [HttpPost("CreateEmbeddedSignature")]
        public async Task<SignatureRequestGetResponse> CreateSignature([FromBody]SignatureRequestCreateEmbeddedRequest body)
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
