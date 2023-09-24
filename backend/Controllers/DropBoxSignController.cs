using Dropbox.Sign.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using static DropBoxSign;
using DBS = DropBoxSign;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    public class DropBoxSignController : ControllerBase
    {
        //Authentication

        [HttpPost("GenerateOAuthToken")]
        public async Task<OAuthTokenResponse> GenerateOAuthToken([FromBody] OAuthTokenGenerateRequest body)
        {
            return await new DBS().OauthTokenGenerate(body);
        }

        [HttpPost("RefreshOAuthToken")]
        public async Task<OAuthTokenResponse> RefreshOAuthToken([FromBody] OAuthTokenRefreshRequest body)
        {
            return await new DBS().RefreshOAuthToken(body);
        }

        //Accounts

        [HttpPost("CreateAccount")]
        public async Task<AccountCreateResponse> CreateAccount([FromBody] AccountCreateRequest body)
        {
            return await new DBS().CreateAccount(body);
        }

        [HttpGet("GetAccount")]
        public async Task<AccountGetResponse> GetAccount()
        {
            return await new DBS().AccountGet();
        }

        [HttpGet("UpdateAccount")]
        public async Task<AccountGetResponse> UpdateAccount(AccountUpdateRequest body)
        {
            return await new DBS().UpdateAccount(body);
        }

        [HttpGet("DeleteAccount")]
        public async Task<string> DeleteAccount(string clientId)
        {
            return await new DBS().DeleteAccount(clientId);
        }

        //Contract

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
        [HttpPost("DownloadFiles")]
        public async Task<bool> DownloadFiles(string signatureRequestId, downloadType type)
        {
            return await new DBS().DownloadFiles(signatureRequestId, type);
        }


    }
}
