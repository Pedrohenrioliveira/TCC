using System;
using System.Net.Http;
using System.Threading.Tasks;

public class Program {
    public static void Main() {
        var client = new HttpClient();
        var content = new MultipartFormDataContent();
        content.Add(new StringContent("00000000-0000-0000-0000-000000000000"), "perfilId");
        var bytes = new byte[100];
        var fileContent = new ByteArrayContent(bytes);
        fileContent.Headers.Add("Content-Type", "image/jpeg");
        content.Add(fileContent, "foto", "test.jpg");
        
        var response = client.PostAsync("http://localhost:5000/api/feed", content).Result;
        Console.WriteLine(response.StatusCode);
        Console.WriteLine(response.Content.ReadAsStringAsync().Result);
    }
}
