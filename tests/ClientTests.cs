using System.Net;
using Moq;
using Moq.Protected;

namespace TCKimlikNo.tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ClientTests
{
    [TestMethod]
    public async Task Post_ItShouldReturnsTrueWithValidArgs()
    {
        // Arrange
        var responseContent = "<TCKimlikNoDogrulaResult>true</TCKimlikNoDogrulaResult>";
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        response.Content = new StringContent(responseContent);
        
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);
        
        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        
        var client = new Client(httpClient);

        var args = new TcKimlikNoDto()
        {
            Name = "Hasan",
            Surname = "Demir",
            Tc = "12345678901",
            BirthYear = 1990
        };

        // Act
        var result = await client.Post(args);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        var resultContent = await result.Content.ReadAsStringAsync();
        Assert.IsTrue(resultContent.Contains("<TCKimlikNoDogrulaResult>true</TCKimlikNoDogrulaResult>"));
    }
    
    [TestMethod]
    public async Task Post_ItShouldReturnsFalseWithoutValidArgs()
    {
        // Arrange
        var responseContent = "<TCKimlikNoDogrulaResult>false</TCKimlikNoDogrulaResult>";
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        response.Content = new StringContent(responseContent);
        
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);
        
        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        
        var client = new Client(httpClient);

        var args = new TcKimlikNoDto()
        {
            Name = "Hasan",
            Surname = "Demir",
            Tc = "12345678901",
            BirthYear = 1990
        };

        // Act
        var result = await client.Post(args);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        var resultContent = await result.Content.ReadAsStringAsync();
        Assert.IsTrue(resultContent.Contains("<TCKimlikNoDogrulaResult>false</TCKimlikNoDogrulaResult>"));
    }
 
}