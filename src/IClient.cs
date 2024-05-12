namespace TCKimlikNo;

public interface IClient
{
    Task<HttpResponseMessage> Post(TcKimlikNoDto args);
}