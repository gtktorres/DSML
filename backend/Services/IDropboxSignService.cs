using backend.Configurations;
using Dropbox.Sign.Model;
using static DropboxSignService;

namespace backend.Services
{
    public interface IDropboxSignService
    {

        Task<OAuthTokenResponse> OauthTokenGenerate(OAuthTokenGenerateRequest body);
        Task<OAuthTokenResponse> OauthTokenRefresh(OAuthTokenRefreshRequest body);
        Task<AccountCreateResponse> AccountCreate(AccountCreateRequest body);
        Task<AccountVerifyResponse> AccountVerify(string email);
        Task<AccountGetResponse> AccountUpdate(AccountUpdateRequest body);
        Task<AccountGetResponse> AccountGet(string? accountID, string? email);
        Task<string> ApiAppDelete(string clientId);
        Task<TemplateCreateEmbeddedDraftResponse> TemplateCreateEmbeddedDraft(TemplateCreateEmbeddedDraftRequest body);
        Task<TemplateUpdateFilesResponse> TemplateUpdateFiles(string template_id, TemplateUpdateFilesRequest request);
        Task<TemplateGetResponse> TemplateAddUser(string template_id, TemplateAddUserRequest request);
        Task<TemplateGetResponse> TemplateRemoveUser(string template_id, TemplateRemoveUserRequest request);
        Task<SignatureRequestGetResponse> SignatureRequestCreateEmbedded(SignatureRequestCreateEmbeddedRequest body);
        Task<SignatureRequestGetResponse> SignatureRequestCreateEmbeddedWithTemplate(SignatureRequestCreateEmbeddedWithTemplateRequest body);
        Task<BulkSendJobSendResponse> SendBulkWithEmbeddedTemplate(SignatureRequestBulkCreateEmbeddedWithTemplateRequest body);
        Task<EmbeddedSignUrlResponse> EmbeddedSignUrl(string signature_id);
        Task<SignatureRequestListResponse> GetAllSignatures(string account_id);
        Task<bool> DownloadFiles(string signatureRequestId, downloadType type);
        Task<bool> TemplateDelete(string templateId);
    }
}
