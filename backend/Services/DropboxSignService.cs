using backend.Configurations;
using backend.Services;
using Dropbox.Sign.Api;
using Dropbox.Sign.Client;
using Dropbox.Sign.Model;
using Microsoft.Extensions.Options;

public class DropboxSignService : IDropboxSignService
{
    private static Configuration? _config;

    private AccountApi accountApi;

    private SignatureRequestApi signatureRequestApi;

    private EmbeddedApi embeddedApi;

    private TemplateApi templateApi;

    private OAuthApi oauthApi;
    private ApiAppApi apiAppApi;

    public enum downloadType { files, datauri, fileurl }

    public DropboxSignService(

            IOptionsMonitor<Configuration> optionsMonitor,
            DropboxSignConfig config

    )
    {
        _config = optionsMonitor.CurrentValue;
        _config.Username = config.Key;

        this.accountApi = new AccountApi(_config);
        this.signatureRequestApi = new SignatureRequestApi(_config);
        this.embeddedApi = new EmbeddedApi(_config);
        this.templateApi = new TemplateApi(_config);
        this.oauthApi = new OAuthApi(_config);
        this.apiAppApi = new ApiAppApi(_config);
    }

    //Authentication
    public async Task<OAuthTokenResponse> OauthTokenGenerate(OAuthTokenGenerateRequest body)
    {
        var response = new OAuthTokenResponse();

        try
        {

            response = await Task.Run(() => this.oauthApi.OauthTokenGenerate(body));

        }
        catch (ApiException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.ErrorCode);
            Console.WriteLine(e.StackTrace);
        }

        return response;
    }

    public async Task<OAuthTokenResponse> OauthTokenRefresh(OAuthTokenRefreshRequest body)
    {
        var response = new OAuthTokenResponse();

        try
        {

            response = await Task.Run(() => oauthApi.OauthTokenRefresh(body));

        }
        catch (ApiException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.ErrorCode);
            Console.WriteLine(e.StackTrace);
        }

        return response;
    }

    //Accounts
    public async Task<AccountCreateResponse> AccountCreate(AccountCreateRequest body)
    {
        var response = new AccountCreateResponse();

        try
        {

            response = await Task.Run(() => accountApi.AccountCreate(body));

        }
        catch (ApiException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.ErrorCode);
            Console.WriteLine(e.StackTrace);
        }

        return response;
    }

    public async Task<AccountVerifyResponse> AccountVerify(string email)
    {
        var response = new AccountVerifyResponse();
        var accountVerifyRequest = new AccountVerifyRequest(
            emailAddress: $"{email}"
            );

        try
        {
            response = await Task.Run(() => accountApi.AccountVerify(accountVerifyRequest));
        }
        catch (ApiException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.ErrorCode);
            Console.WriteLine(e.StackTrace);
        }

        return response;
    }

    public async Task<AccountGetResponse> AccountUpdate(AccountUpdateRequest body)
    {
        var response = new AccountGetResponse();

        try
        {

            response = await Task.Run(() => accountApi.AccountUpdate(body));

        }
        catch (ApiException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.ErrorCode);
            Console.WriteLine(e.StackTrace);
        }

        return response;
    }

    public async Task<AccountGetResponse> AccountGet(string email)
    {
        var account = new AccountGetResponse();

        try
        {
            account = await Task.Run(() => accountApi.AccountGet(null, email));
        }
        catch (ApiException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.ErrorCode);
            Console.WriteLine(e.StackTrace);

        }

        return account;
    }

    public async Task<string> ApiAppDelete(string clientId)
    {
        string response = "";

        try
        {

            await Task.Run(() => apiAppApi.ApiAppDelete(clientId));
            Console.WriteLine("Account deleted.");
            response = "Account deleted.";

        }
        catch (ApiException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.ErrorCode);
            Console.WriteLine(e.StackTrace);
        }

        return response;
    }

    //Contracts
    public async Task<TemplateCreateEmbeddedDraftResponse> TemplateCreateEmbeddedDraft(TemplateCreateEmbeddedDraftRequest body)
    {
        var response = new TemplateCreateEmbeddedDraftResponse();

        try
        {

            response = await Task.Run(() => templateApi.TemplateCreateEmbeddedDraft(body));

        }
        catch (ApiException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.ErrorCode);
            Console.WriteLine(e.StackTrace);
        }

        return response;
    }

    public async Task<TemplateUpdateFilesResponse> TemplateUpdateFiles(string template_id, TemplateUpdateFilesRequest request)
    {
        var response = new TemplateUpdateFilesResponse();

        try
        {

            response = await Task.Run(() => templateApi.TemplateUpdateFiles(template_id, request));

        }
        catch (ApiException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.ErrorCode);
            Console.WriteLine(e.StackTrace);
        }

        return response;
    }

    public async Task<TemplateGetResponse> TemplateAddUser(string template_id, TemplateAddUserRequest request)
    {
        var response = new TemplateGetResponse();

        try
        {

            response = await Task.Run(() => templateApi.TemplateAddUser(template_id, request));

        }
        catch (ApiException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.ErrorCode);
            Console.WriteLine(e.StackTrace);
        }

        return response;
    }
    public async Task<TemplateGetResponse> TemplateRemoveUser(string template_id, TemplateRemoveUserRequest request)
    {
        var response = new TemplateGetResponse();

        try
        {

            response = await Task.Run(() => templateApi.TemplateRemoveUser(template_id, request));

        }
        catch (ApiException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.ErrorCode);
            Console.WriteLine(e.StackTrace);
        }

        return response;
    }

    public async Task<SignatureRequestGetResponse> SignatureRequestCreateEmbedded(SignatureRequestCreateEmbeddedRequest body)
    {
        var response = new SignatureRequestGetResponse();

        try
        {
            response = await Task.Run(() => signatureRequestApi.SignatureRequestCreateEmbedded(body));
        }
        catch (ApiException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.ErrorCode);
            Console.WriteLine(e.StackTrace);
        }
        catch (UriFormatException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.HResult);
            Console.WriteLine(e.StackTrace);
        }

        return response;


    }

    public async Task<SignatureRequestGetResponse> SignatureRequestCreateEmbeddedWithTemplate(SignatureRequestCreateEmbeddedWithTemplateRequest body)
    {
        var response = new SignatureRequestGetResponse();

        try
        {

            response = await Task.Run(() => signatureRequestApi.SignatureRequestCreateEmbeddedWithTemplate(body));
            Console.WriteLine("Bulk Signature requests sent.");

        }
        catch (ApiException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.ErrorCode);
            Console.WriteLine(e.StackTrace);
        }

        return response;
    }

    public async Task<BulkSendJobSendResponse> SendBulkWithEmbeddedTemplate(SignatureRequestBulkCreateEmbeddedWithTemplateRequest body)
    {
        var response = new BulkSendJobSendResponse();

        try
        {

            response = await Task.Run(() => signatureRequestApi.SignatureRequestBulkCreateEmbeddedWithTemplate(body));
            Console.WriteLine("Bulk Signature requests sent.");

        }
        catch (ApiException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.ErrorCode);
            Console.WriteLine(e.StackTrace);
        }

        return response;
    }

    public async Task<EmbeddedSignUrlResponse> EmbeddedSignUrl(string signature_id)
    {
        var response = new EmbeddedSignUrlResponse();

        try
        {
            response = await Task.Run(() => embeddedApi.EmbeddedSignUrl(signature_id));
        }
        catch (ApiException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.ErrorCode);
            Console.WriteLine(e.StackTrace);
        }

        return response;
    }
    public async Task<SignatureRequestListResponse> GetAllSignatures(string account_id)
    {
        var response = new SignatureRequestListResponse();

        try
        {
            response = await Task.Run(() => signatureRequestApi.SignatureRequestList(account_id));
        }
        catch (ApiException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.ErrorCode);
            Console.WriteLine(e.StackTrace);
        }

        return response;
    }

    public async Task<bool> DownloadFiles(string signatureRequestId, downloadType type)
    {

        try
        {
            switch (type)
            {
                case downloadType.files:

                    using (FileStream fs = File.Create("file_response.pdf"))
                    {
                        var resultFiles = signatureRequestApi.SignatureRequestFiles(signatureRequestId, "pdf");
                        await Task.Run(() => resultFiles.Seek(0, SeekOrigin.Begin));
                        await Task.Run(() => resultFiles.CopyTo(fs));
                        fs.Close();
                        return true;
                    }

                case downloadType.fileurl:
                    var resultFileURL = signatureRequestApi.SignatureRequestFilesAsFileUrl(signatureRequestId);
                    return true;

                case downloadType.datauri:
                    var resultDataUri = signatureRequestApi.SignatureRequestFilesAsDataUri(signatureRequestId);
                    return true;

                default:
                    Console.WriteLine("File download choice is invalid.");
                    return false;
            }

        }
        catch (ApiException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.ErrorCode);
            Console.WriteLine(e.StackTrace);
            return false;
        }
    }

    public async Task<bool> TemplateDelete(string templateId) { 

        try
        {

            await Task.Run(() => templateApi.TemplateDelete(templateId));
            Console.WriteLine("Template deleted.");
            return true;

        }
        catch (ApiException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.ErrorCode);
            Console.WriteLine(e.StackTrace);
            return false;
        }
    }
}
