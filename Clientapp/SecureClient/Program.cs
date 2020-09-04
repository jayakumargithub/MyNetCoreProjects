using System;
using Microsoft.Identity.Client;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;

namespace SecureClient
{
    class Program
    {
        /*
            https://www.youtube.com/watch?v=3PyUjOmuFic
            http://dotnetplaybook.com/secure-a-net-core-api-using-bearer-authentication/
            https://github.com/binarythistle/S03E01-Secure-.NET-Core-API
        */
        static void Main(string[] args)
        {
        //    AuthConfig config = AuthConfig.ReadJsonFromFile(@"appSettings.json");
        //    Console.WriteLine($"Authority: {config.Authority}");
        Console.WriteLine("Making the call.........");
        RunAsync().GetAwaiter().GetResult();
        }
        
        private static async Task RunAsync(){
            AuthConfig config = AuthConfig.ReadJsonFromFile(@"appSettings.json");
            
            IConfidentialClientApplication app;
            app = ConfidentialClientApplicationBuilder.Create(config.ClientId)
            .WithClientSecret(config.ClientSecret)
            .WithAuthority(new Uri(config.Authority))
            .Build();
            
            string[] ResourceIds = new string[] {config.ResourceId};
            AuthenticationResult result = null;
            try{
                result = await app.AcquireTokenForClient(ResourceIds).ExecuteAsync();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Token Aquired \n");
                Console.WriteLine(result.AccessToken);
                Console.ResetColor();
            }catch(Exception ex){
                 
                 Console.ForegroundColor = ConsoleColor.Red;
                 Console.WriteLine(ex.Message);
                 Console.ResetColor();
            }
            
            if(!string.IsNullOrEmpty(result.AccessToken)){
                var httpClient = new HttpClient();
                var defaultRequestHeaders = httpClient.DefaultRequestHeaders;
                if(defaultRequestHeaders.Accept ==null || !defaultRequestHeaders.Accept.Any(m => m.MediaType == "appliation/json")){
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
                
                defaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.AccessToken);
                
                HttpResponseMessage response = await httpClient.GetAsync(config.BaseAddress);
                
                if(response.IsSuccessStatusCode)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    string json = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(json);
                    Console.ResetColor();
                }
                else{
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Faield to call API: {response.StatusCode}");
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Content: {content}");
                }
                
            }
            Console.ResetColor();
        }
        
    }
}
