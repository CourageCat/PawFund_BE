using Google.Cloud.Dialogflow.V2;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Options;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Settings;
using Grpc.Auth;

namespace PawFund.Infrastructure.Services;

public class DialogflowService : IDialogflowService
{
    private readonly SessionsClient _client;
    private readonly string _projectId;
    private readonly DialogflowSetting _dialogflowSetting;

    public DialogflowService(IOptions<DialogflowSetting> dialogflowConfig)
    {
        _dialogflowSetting = dialogflowConfig.Value;

        if (string.IsNullOrEmpty(_dialogflowSetting.PrivateKey))
        {
            throw new ArgumentNullException("PrivateKey cannot be null or empty.");
        }

        _projectId = _dialogflowSetting.ProjectId;

        // Tạo credential từ PrivateKey
        //var credential = GoogleCredential.FromJson("\"-----BEGIN PRIVATE KEY-----\\nMIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCjT2FaFN3vRotx\\nV9g4t/uAK9De3ylvHafSGX/zktqfrzc6pG3fD0qXWphJxyZ+uAzGh7pE6+zNA5d4\\nRYf548SODuU7/LjpbjX5gU0LL9aDBsnd+Qav6ywqF8dRJQEI9IRqGB9PcuGbiK07\\nR9Pc+fAAK7FEkHhHvVmoWND42TP/3SGn7EeEoRScYn0yG8uWyrMHhYA7gubljSj/\\nE2oGZmJSnkq1agtysTjJYSB7JqeVFtncuPw4u+itSuBIL0MN84cS4wKFUAcwyj9a\\nn1amppMxr6S1j2Xv/LhbmA+g1EYtFMRsBccTHzsJIzduCw6Sl35+QagT8F1kTkDj\\nbccm9+6hAgMBAAECggEAANwVHh1IAc90Qnd86ueFCmqwocu48x3L2V5K/yRBxF9u\\nwf9FmMwCz4MplBHZUQd1AcKLJ3u6dmXTcU2kZptdWIYZjNRKwOfUnuigWDO7RFtP\\nyrxZ3ak2RU50vSedtsdavxg7afan22K/zrUVF2+1ZLrWJw8/otkLVMiK6RRzoqAs\\nOq30KKoLOqTEEnqIY9b+i9MHhuytP6dmPfebpp+7SAIkqmgg3S5GQX91HkJm71Hc\\nbz1cBCm4wuEGgrp87YEbYCgTpgv9ksJiS8w0mNNFz8YO9Ar6tMM9cSu/DmokBYjP\\nfwR4rCHgWdGL05U76PEnigvPbLUHLjdUfO+6saf4MQKBgQDUYt5jpvZWYXmnDJbO\\nFft82Sz+tqnIUxg2MFc3XgJ1SdA8ht/lB1LHoamlcbaRQ/cY3T/BXPEazHse8isI\\n4fWwfgzxDSSqp3kDkGmjIOkZG3xxAtexkT/4CW0HqKYky1g3/GBKaUAoplUrsSbF\\nPzI5GGvQtL1fhRMHyA3qCRRD7QKBgQDE2JUPL+G7fnAYv2mh9QXV2hHAoCbnaqX/\\n5F2AZhDjlLu/Hl4rYlyiTEnx+LolXSXfWKt/esfkb3B8gooMK/c/wQWz4c4lz9d/\\n6/iP4dkzjz9G6mt2OyjvgYSl5vejelkYELDY+B6HHGN1Apj5zExtUYqMCwOVzoch\\nKg3F0++nBQKBgGFWfr4FQ1kLi4m4B1WkVYI+tpR2exHIo4wSU6aYe1/t2HYlCnAZ\\nDyNXQD0HBLlHAPRwUkv4oLe7h6IDMx37EXM2EhBYCbn8z0WG5iNRbgOHyVyyBNDo\\n1PyEtWeRL7QlTeU60ZbI7hKzSM2hfzdfY1EvlBou+6vPfY1PbrEYQCVxAoGAYLZk\\nbeTBjEPvwUjyFj9zEYGlrTVCR/qu+k1x3u7Gp3ng0GKUM86M4rSGtBrXIhpkzBod\\nkHF7mnFiG9prBtXnEWy9Z4auqQc3wsLmCZMWCu37UdVE3P7t+tZwnK4gxzIKafdx\\nW/hzz0AUYV3YWQqWFjO01V9n8Ida/uFRiPu6yB0CgYAK8bWVXOaz3Sp2imhPfmbx\\ntffnB18EVB9gl2AG51tR4Y9KM3Nn6Ashqizc3SiQ/UVz6HSkY4b9e7GWJcgzVM+X\\nwrf9EiGdjBApy6E1D5VefDDEuSVYhVR2CZvToz1JeVxeQY3D7QH9AMeO5XrQ9ApO\\nuvLBifTyt54wW9wHVLpJLg==\\n-----END PRIVATE KEY-----\\n\"");
        var credential = GoogleCredential.FromFile(@"C:\\Users\\vietv\\Downloads\pawfund-f38cc3b84087.json");
        // Khởi tạo SessionsClient với credential
        _client = new SessionsClientBuilder
        {
            ChannelCredentials = credential.ToChannelCredentials(),
        }.Build();
    }

    public async Task<string> DetectIntentAsync(string sessionId, string text, string languageCode)
    {
        var session = new SessionName(_projectId, sessionId);
        var queryInput = new QueryInput
        {
            Text = new TextInput
            {
                Text = text,
                LanguageCode = languageCode
            }
        };

        var response = await _client.DetectIntentAsync(session, queryInput);
        return response.QueryResult.FulfillmentText;
    }
}
