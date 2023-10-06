using backend.Services;
using Dropbox.Sign.Client;
using Dropbox.Sign.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static DropboxSignService;
using DBS = DropboxSignService;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    public class DropboxSignController : ControllerBase
    {
        private readonly ILogger<DropboxSignController> _logger;
        private readonly IDropboxSignService _dropboxSignService;
        private IOptionsMonitor<Configuration> _optionsMonitor;

        public DropboxSignController(
            ILogger<DropboxSignController> logger,
            IDropboxSignService dropboxSignService
        )
        {
            _logger = logger;
            _dropboxSignService = dropboxSignService;
        }
        //Authentication

        [HttpPost("GenerateOAuthToken")]
        public async Task<OAuthTokenResponse> GenerateOAuthToken([FromBody] OAuthTokenGenerateRequest body)
        {
            return await new DBS(_optionsMonitor).OauthTokenGenerate(body);
        }

        [HttpPost("RefreshOAuthToken")]
        public async Task<OAuthTokenResponse> RefreshOAuthToken([FromBody] OAuthTokenRefreshRequest body)
        {
            return await new DBS(_optionsMonitor).OauthTokenRefresh(body);
        }

        //Accounts

        [HttpPost("CreateAccount")]
        public async Task<AccountCreateResponse> AccountCreate([FromBody] AccountCreateRequest body)
        {
            return await new DBS(_optionsMonitor).AccountCreate(body);
        }

        [HttpPost("VerifyAccount")]
        public async Task<AccountVerifyResponse> AccountVerify(string email)
        {
            return await new DBS(_optionsMonitor).AccountVerify(email);
        }

        [HttpGet("GetAccount")]
        public async Task<AccountGetResponse> AccountGet(string email)
        {
            return await new DBS(_optionsMonitor).AccountGet(email);
        }

        [HttpPut("UpdateAccount")]
        public async Task<AccountGetResponse> AccountUpdate(AccountUpdateRequest body)
        {
            return await new DBS(_optionsMonitor).AccountUpdate(body);
        }

        [HttpDelete("DeleteAccount")]
        public async Task<string> ApiAppDelete(string clientId)
        {
            return await new DBS(_optionsMonitor).ApiAppDelete(clientId);
        }

        //Contracts
        //create a drafted contract with an embedded template
        //[HttpPost("CreateContractTemplate")]
        //public async Task<TemplateCreateEmbeddedDraftResponse> TemplateCreateEmbeddedDraft([FromBody]TemplateCreateEmbeddedDraftRequest body)
        //{
        //    return await new DBS().TemplateCreateEmbeddedDraft(body);
        //}
        //update a file(s)
        [HttpPost("TemplateUpdateFiles")]
        public async Task<TemplateUpdateFilesResponse> TemplateUpdateFiles(string template_id, [FromBody]TemplateUpdateFilesRequest request)
        {
            return await new DBS(_optionsMonitor).TemplateUpdateFiles(template_id, request);
        }
        //add users to template
        [HttpPost("AddUserToTemplate")]
        public async Task<TemplateGetResponse> TemplateAddUser(string template_id, [FromBody]TemplateAddUserRequest request)
        {
            return await new DBS(_optionsMonitor).TemplateAddUser(template_id, request);
        }
        //remove users to template
        [HttpPost("RemoveUserFromTemplate")]
        public async Task<TemplateGetResponse> TemplateRemoveUser(string template_id, [FromBody] TemplateRemoveUserRequest request)
        {
            return await new DBS(_optionsMonitor).TemplateRemoveUser(template_id, request);
        }
        //create a contract with submitted documents
        [HttpPost("CreateEmbeddedSignature")]
        public async Task<SignatureRequestGetResponse> SignatureRequestCreateEmbedded([FromBody] SignatureRequestCreateEmbeddedRequest body)
        {
            return await new DBS(_optionsMonitor).SignatureRequestCreateEmbedded(body);
        }
        //create a contract with a given template
        //[HttpPost("CreateEmbeddedSignatrueWithTemplate")]
        //public async Task<SignatureRequestGetResponse> SignatureRequestCreateEmbeddedWithTemplate(SignatureRequestCreateEmbeddedWithTemplateRequest body)
        //{
        //    return await new DBS().SignatureRequestCreateEmbeddedWithTemplate(body);
        //}
        //create contracts via template
        //[HttpPost("BulkSendEmbeddedTemplate")]
        //public async Task<BulkSendJobSendResponse> SendBulkWithEmbeddedTemplate(SignatureRequestBulkCreateEmbeddedWithTemplateRequest body)
        //{
        //    return await new DBS().SendBulkWithEmbeddedTemplate(body);
        //}
        //get the signature url of a contract
        [HttpGet("GetEmbeddedSignature")]
        public async Task<EmbeddedSignUrlResponse> EmbeddedSignUrl(string signature_id)
        {
            return await new DBS(_optionsMonitor).EmbeddedSignUrl(signature_id);
        }

        //get a list of signature urls
        [HttpGet("GetAllSignatures")]
        public async Task<SignatureRequestListResponse> GetAllSignatures(string account_id)
        {
            return await new DBS(_optionsMonitor).GetAllSignatures(account_id);
        }
        //download files, as fileurl, as datauri
        [HttpPost("DownloadFiles")]
        public async Task<bool> DownloadFiles(string signatureRequestId, downloadType type)
        {
            return await new DBS(_optionsMonitor).DownloadFiles(signatureRequestId, type);
        }
        //delete contract template
        [HttpDelete("DeleteContractTemplate")]
        public async Task<bool> TemplateDelete(string templateId)
        {
            return await new DBS(_optionsMonitor).TemplateDelete(templateId);
        }


    }
}
