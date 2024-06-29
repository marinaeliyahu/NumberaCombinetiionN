using System.Net;

#nullable enable

namespace WebAppCoreTargilME.Common.BaseModel;

public record BaseViewModel
{
    public IList<Message> Messages { get; set; }

    public HttpStatusCode Status { get; set; }

    public BaseViewModel()
    {
        Messages = new List<Message>();
        Status = HttpStatusCode.OK;
    }
}
public record BaseViewModel<T> : BaseViewModel
{

    public T? Data { get; set; }
}

public record Message
{
    public LogLevel LogLevel { get; set; }

    public string Text { get; set; } = string.Empty;
}

public enum LogLevel
{
    Fatal,
    Error,
    Warning,
    Info
}