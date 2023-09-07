using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekShopping.Web.Utils
{
    public static class HttpClientExtensions
    {
        private static MediaTypeHeaderValue contentType = new MediaTypeHeaderValue("application/json");

        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode) 
                throw new ApplicationException(@$"Something went wrong: 
                                                {response.ReasonPhrase}");


            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonSerializer.Deserialize<T>(dataAsString,
                                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }


        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string url, T data)
        {
            string dataAsString = JsonSerializer.Serialize(data);

            var content = new StringContent(dataAsString);
            content.Headers.ContentType = contentType;

            return httpClient.PostAsync(url, content);
        }

        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient httpClient, string url, T data)
        {
            string dataAsString = JsonSerializer.Serialize(data);

            var content = new StringContent(dataAsString);
            content.Headers.ContentType = contentType;

            return httpClient.PutAsync(url, content);
        }
    }
}

