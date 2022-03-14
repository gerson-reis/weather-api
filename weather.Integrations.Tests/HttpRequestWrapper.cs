using Newtonsoft.Json;
using RestSharp;

public class HttpRequestWrapper
{
    private RestRequest _restRequest;
    private RestClient _restClient;

    public HttpRequestWrapper()
    {
        _restRequest = new RestRequest();
    }

    public HttpRequestWrapper SetResourse(string resource)
    {
        _restRequest.Resource = resource;
        return this;
    }

    public HttpRequestWrapper SetMethod(Method method)
    {
        _restRequest.Method = method;
        return this;
    }

    public HttpRequestWrapper AddHeaders(IDictionary<string, string> headers)
    {
        foreach (var header in headers)
        {
            _restRequest.AddParameter(header.Key, header.Value, ParameterType.HttpHeader);
        }
        return this;
    }

    public HttpRequestWrapper AddJsonContent(object data)
    {
        _restRequest.RequestFormat = DataFormat.Json;
        _restRequest.AddHeader("Content-Type", "application/json");
        _restRequest.AddBody(data);
        return this;
    }

    public HttpRequestWrapper AddEtagHeader(string value)
    {
        return this;
    }


    public HttpRequestWrapper AddParameter(string name, object value)
    {
        _restRequest.AddParameter(name, value, ParameterType.GetOrPost);
        return this;
    }

    public HttpRequestWrapper AddParameters(IDictionary<string, object> parameters)
    {
        foreach (var item in parameters)
        {
            _restRequest.AddParameter(item.Key, item.Value, ParameterType.GetOrPost);
        }
        return this;
    }

    public RestResponse Execute()
    {
        try
        {
            _restClient = new RestClient("https://localhost:7167/");
            var response = _restClient.ExecuteAsync(_restRequest).Result;
            return response;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public T Execute<T>()
    {
        _restClient = new RestClient("https://localhost:7167/");
        var response = _restClient.ExecuteAsync(_restRequest).Result;
        var data = JsonConvert.DeserializeObject<T>(response.Content);
        return data;
    }

}