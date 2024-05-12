using System.Net.Http.Headers;

namespace TCKimlikNo;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


internal class Client: IClient
{
    private readonly HttpClient _httpClient;
    
    public Client(HttpClient httpClient)
    {
        httpClient.BaseAddress = new Uri("https://tckimlik.nvi.gov.tr/service/kpspublic.asmx?WSDL");
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/soap+xml"));

        _httpClient = httpClient;
    }
    
    public async Task<HttpResponseMessage> Post(TcKimlikNoDto args)
    {
        var body = GetBody(args);
        var content = new StringContent(body, Encoding.UTF8, "application/soap+xml");
        return await _httpClient.PostAsync("", content);
    }

    private string GetBody(TcKimlikNoDto args)
    {
        return $@"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
                <soap12:Body>
                    <TCKimlikNoDogrula xmlns=""http://tckimlik.nvi.gov.tr/WS"">
                        <TCKimlikNo>{args.Tc}</TCKimlikNo>
                        <Ad>{args.Name}</Ad>
                        <Soyad>{args.Surname}</Soyad>
                        <DogumYili>{args.BirthYear}</DogumYili>
                    </TCKimlikNoDogrula>
                </soap12:Body>
            </soap12:Envelope>";
    } 
}