using Dropbox.Sign.Api;
using Dropbox.Sign.Client;
using Dropbox.Sign.Model;
using Newtonsoft.Json;

public class DropBoxSign
{

    private static String ApiKey = "3d684057d1a90cf841bc16500d38507217014f92deffc2efaaa3135ea4a1cdb7";

    private protected AccountApi accountApi = new AccountApi(new Configuration { Username = ApiKey });

    private protected SignatureRequestApi signatureRequestApi = new SignatureRequestApi(ApiKey);

    private protected EmbeddedApi embeddedApi = new EmbeddedApi(new Configuration { Username = ApiKey });

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

    public async Task<SignatureRequestGetResponse> CreateSignature(SignatureRequestCreateEmbeddedRequest request)
    {

        var response = new SignatureRequestGetResponse();

        try
        {
            response = await Task.Run(() => signatureRequestApi.SignatureRequestCreateEmbedded(request));
        }
        catch (ApiException e)
        {
            Console.WriteLine("Exception when calling Dropbox Sign API: " + e.Message);
            Console.WriteLine("Status Code: " + e.ErrorCode);
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
}
