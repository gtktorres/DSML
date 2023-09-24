using Dropbox.Sign.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
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
            return await new DBS().OauthTokenRefresh(body);
        }

        //Accounts

        [HttpPost("CreateAccount")]
        public async Task<AccountCreateResponse> AccountCreate([FromBody] AccountCreateRequest body)
        {
            return await new DBS().AccountCreate(body);
        }

        [HttpGet("GetAccount")]
        public async Task<AccountGetResponse> AccountGet()
        {
            return await new DBS().AccountGet();
        }

        [HttpGet("UpdateAccount")]
        public async Task<AccountGetResponse> AccountUpdate(AccountUpdateRequest body)
        {
            return await new DBS().AccountUpdate(body);
        }

        [HttpGet("DeleteAccount")]
        public async Task<string> ApiAppDelete(string clientId)
        {
            return await new DBS().ApiAppDelete(clientId);
        }
        }

        //Contracts
        //create a drafted contract with an embedded template
        [HttpPost("CreateContractTemplate")]
        public async Task<TemplateCreateEmbeddedDraftResponse> TemplateCreateEmbeddedDraft([FromBody]TemplateCreateEmbeddedDraftRequest body)
        {
            return await new DBS().TemplateCreateEmbeddedDraft(body);
        }
        //update a files
        [HttpPost("TemplateUpdateFiles")]
        public async Task<TemplateUpdateFilesResponse> TemplateUpdateFiles(string template_id, [FromBody]TemplateUpdateFilesRequest request)
        {
            return await new DBS().TemplateUpdateFiles(template_id, request);
        }
        //add users to template
        [HttpPost("AddUserToTemplate")]
        public async Task<TemplateGetResponse> TemplateAddUser(string template_id, [FromBody]TemplateAddUserRequest request)
        {
            return await new DBS().TemplateAddUser(template_id, request);
        }
        //remove users to template
        [HttpPost("RemoveUserFromTemplate")]
        public async Task<TemplateGetResponse> TemplateRemoveUser(string template_id, [FromBody] TemplateRemoveUserRequest request)
        {
            return await new DBS().TemplateRemoveUser(template_id, request);
        }
        //create a contracts with submitted documents
        [HttpPost("CreateEmbeddedSignature")]
        public async Task<SignatureRequestGetResponse> SignatureRequestCreateEmbedded([FromBody] SignatureRequestCreateEmbeddedRequest body)
        {
            return await new DBS().SignatureRequestCreateEmbedded(body);
        }
        //create a contracts with a given template
        [HttpPost("CreateEmbeddedSignatrueWithTemplate")]
        public async Task<SignatureRequestGetResponse> SignatureRequestCreateEmbeddedWithTemplate(SignatureRequestCreateEmbeddedWithTemplateRequest body)
        {
            return await new DBS().SignatureRequestCreateEmbeddedWithTemplate(body);
        }
        //create a contracts
        [HttpPost("BulkSendEmbeddedTemplate")]
        public async Task<BulkSendJobSendResponse> SendBulkWithEmbeddedTemplate(SignatureRequestBulkCreateEmbeddedWithTemplateRequest body)
        {
            return await new DBS().SendBulkWithEmbeddedTemplate(body);
        }
        //get the signature url of a contract
        [HttpGet("GetEmbeddedSignature")]
        public async Task<EmbeddedSignUrlResponse> EmbeddedSignUrl(string signature_id)
        {
            return await new DBS().EmbeddedSignUrl(signature_id);
        }
        //download files, as fileurl, as datauri
        [HttpPost("DownloadFiles")]
        public async Task<bool> DownloadFiles(string signatureRequestId, downloadType type)
        {
            return await new DBS().DownloadFiles(signatureRequestId, type);
        }
        //delete contract template
        [HttpPost("DeleteContractTemplate")]
        public async Task<bool> TemplateDelete(string templateId)
        {
            return await new DBS().TemplateDelete(templateId);
        }


    }
}
