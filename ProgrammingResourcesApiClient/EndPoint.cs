namespace ProgrammingResourcesApiClient;

public class EndPoint
{
    protected virtual void CheckResponse(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"API Response was not successful: {response.StatusCode}");
        }
    }

    protected virtual void ThrowIfNull<T>(T obj)
    {
        if(obj is null)
        {
            throw new Exception($"API returned a null value, in a method where null values are not allowed.");
        }
    }
}
