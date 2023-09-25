using Dropbox.Sign.Api;
using Dropbox.Sign.Client;
using Dropbox.Sign.Model;
using Newtonsoft.Json;
using Xunit;

namespace DropBoxSign.Tests;

public class DropBoxSignContractUnitTests
{

    private string API_KEY = "3d684057d1a90cf841bc16500d38507217014f92deffc2efaaa3135ea4a1cdb7";

    [Fact]
    public void TestTemplateCreateEmbeddedDraft()
    {
        var config = new Configuration();
        // Configure HTTP basic authorization: api_key
        config.Username = API_KEY;

        // or, configure Bearer (JWT) authorization: oauth2
        // config.AccessToken = "YOUR_BEARER_TOKEN";

        var templateApi = new TemplateApi(config);

        var role1 = new SubTemplateRole(
            name: "Client",
            order: 0
        );

        var role2 = new SubTemplateRole(
            name: "Witness",
            order: 1
        );

        var mergeField1 = new SubMergeField(
            name: "Full Name",
            type: SubMergeField.TypeEnum.Text
        );

        var mergeField2 = new SubMergeField(
            name: "Is Registered?",
            type: SubMergeField.TypeEnum.Checkbox
        );

        var subFieldOptions = new SubFieldOptions(
            dateFormat: SubFieldOptions.DateFormatEnum.DDMMYYYY
        );

        var files = new List<Stream> {
            new FileStream(
                "./example_nda_signature_request.pdf",
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read
            )
        };

        var data = new TemplateCreateEmbeddedDraftRequest(
            clientId: "518c7f7524e4583b7139cfbf6ea1d6b4",
            files: files,
            title: "Test Template",
            subject: "Please sign this document",
            message: "For your approval",
            signerRoles: new List<SubTemplateRole>() { role1, role2 },
            ccRoles: new List<string>() { "Manager" },
            mergeFields: new List<SubMergeField>() { mergeField1, mergeField2 },
            fieldOptions: subFieldOptions,
            testMode: true
        );

        var val = new TemplateCreateEmbeddedDraftResponse();


        var result = templateApi.TemplateCreateEmbeddedDraft(data);
        Assert.True(result.GetType().Equals(val.GetType()));
    }
}