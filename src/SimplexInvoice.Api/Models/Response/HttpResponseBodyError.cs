namespace SimplexInvoice.Api.Models.Response;

public class HttpResponseBodyError
{
    public int Code { get; set; } = 0;
    public string Message { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = new List<string>();
}