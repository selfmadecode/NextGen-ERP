namespace Shared.Utilities;

public class BaseResponse<T>
{
    public BaseResponse()
    {
    }

    public BaseResponse(T data, string responseMessage = null)
    {
        Data = data;
        Status = RequestExecution.Successful;
        ResponseMessage = responseMessage;
    }

    public BaseResponse(T data, int totalCount, string responseMessage = null)
    {
        Data = data;
        TotalCount = totalCount;
        Status = RequestExecution.Successful;
        ResponseMessage = responseMessage;
    }

    public BaseResponse(string error, List<string> errors = null)
    {
        Status = RequestExecution.Failed;
        ResponseMessage = error;
        Errors = errors;
    }

    public BaseResponse(T data, string error, List<string> errors, RequestExecution status)
    {
        Status = status;
        ResponseMessage = error;
        Errors = errors;
        Data = data;
    }

    public RequestExecution Status { get; set; }
    public T Data { get; set; }
    public string ResponseMessage { get; set; }
    public int TotalCount { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
}
