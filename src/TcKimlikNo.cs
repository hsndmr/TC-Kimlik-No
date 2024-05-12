namespace TCKimlikNo;

public class TcKimlikNo
{
    private static readonly Lazy<TcKimlikNoManager> Instance = new (() =>
        new TcKimlikNoManager(new Client(new HttpClient())));

    private TcKimlikNo()
    {
    }

    public static async Task<bool> Validate(TcKimlikNoDto args)
    {
        return await Instance.Value.Validate(args);
    }

    public static bool Verify(string tcKimlikNo)
    {
        return Instance.Value.Verify(tcKimlikNo);
    }
}