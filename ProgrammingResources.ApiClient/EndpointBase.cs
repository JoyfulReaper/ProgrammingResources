namespace ProgrammingResources.ApiClient;

public abstract class EndpointBase
{
    protected virtual void CheckResponse(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"API Response was not successful: {response.StatusCode} {response.ReasonPhrase}");
        }
    }

    protected virtual void ThrowIfNull<T>(T obj)
    {
        if (obj is null)
        {
            throw new Exception($"API returned a null value to a method where null values are not allowed.");
        }
    }
}
