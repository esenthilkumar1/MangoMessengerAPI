using MangoAPI.DiffieHellmanLibrary.Constants;
using MangoAPI.DiffieHellmanLibrary.Helpers;
using MangoAPI.DiffieHellmanLibrary.Services;

namespace MangoAPI.DiffieHellmanLibrary.OpenSslHandlers;

public class OpenSslUploadDhParametersHandler : BaseHandler
{
    public OpenSslUploadDhParametersHandler(HttpClient httpClient, TokensService tokensService) : base(httpClient,
        tokensService)
    {
    }

    public async Task UploadDhParametersAsync()
    {
        Console.WriteLine(@"Attempting to upload DH parameters file...");

        await OpenSslUploadDhParametersAsync();

        Console.WriteLine(@"DH parameters have been updated successfully.");

        Console.WriteLine();
    }

    private async Task OpenSslUploadDhParametersAsync()
    {
        var parametersPath = Path.Combine(
            OpenSslDirectoryHelper.OpenSslDhParametersDirectory,
            FileNameHelper.ParametersFileName);

        await using var stream = File.OpenRead(parametersPath);

        var uri = new Uri(OpenSslRoutes.OpenSslParameters, UriKind.Absolute);

        using var request = new HttpRequestMessage(HttpMethod.Post, uri);

        using var content = new MultipartFormDataContent
        {
            { new StreamContent(stream), "file", "dhp.pem" }
        };

        request.Content = content;

        var httpResponseMessage = await HttpClient.SendAsync(request);

        httpResponseMessage.EnsureSuccessStatusCode();
    }
}