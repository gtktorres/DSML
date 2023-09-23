using Dropbox.Sign.Api;
using Dropbox.Sign.Client;
using Dropbox.Sign.Model;
using Newtonsoft.Json;
using System.Reflection.Metadata;

public class DropBoxSign
{

    private static String ApiKey = "3d684057d1a90cf841bc16500d38507217014f92deffc2efaaa3135ea4a1cdb7";

    private static Configuration _config = new Configuration {Username = ApiKey};

    private protected AccountApi accountApi = new AccountApi(_config);

    private protected SignatureRequestApi signatureRequestApi = new SignatureRequestApi(_config);

    private protected EmbeddedApi embeddedApi = new EmbeddedApi(_config);

    private protected TemplateApi templateApi = new TemplateApi(_config);

    private protected OAuthApi oauthApi = new OAuthApi(_config);

    public async Task<SignatureRequestGetResponse> CreateSignature(SignatureRequestCreateEmbeddedRequest body)
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
        catch(UriFormatException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.HResult);
            Console.WriteLine(e.StackTrace);
        }

        return response;


    }

    public async Task<EmbeddedSignUrlResponse> GetEmbeddedSignURL(string signature_id)
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



    public async Task<TemplateCreateEmbeddedDraftResponse> CreateContract(TemplateCreateEmbeddedDraftRequest body)
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

    //Authentication
    public async Task<AccountCreateResponse> CreateAccount(AccountCreateRequest body)
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

    public async Task<OAuthTokenResponse> OauthTokenGenerate(OAuthTokenGenerateRequest body)
    {
        var response = new OAuthTokenResponse();

        try
        {

            response = await Task.Run(() => oauthApi.OauthTokenGenerate(body));

        }
        catch (ApiException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.ErrorCode);
            Console.WriteLine(e.StackTrace);
        }

        return response;
    }

    public async Task<OAuthTokenResponse> RefreshOathToken(OAuthTokenRefreshRequest body)
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

    public async Task<AccountGetResponse> AccountGet()
    {
        var account = new AccountGetResponse();
    
        try
        {
            account =  await Task.Run(() =>accountApi.AccountGet(null, "gtktorres@gmail.com"));
        }
        catch (ApiException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.ErrorCode);
            Console.WriteLine(e.StackTrace);

        }

        return account;
    }
}
