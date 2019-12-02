using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WorkorderService.Messages;

namespace AngleClient
{
    public class WorkorderServiceClient
    {
        private readonly string _baseUri;

        public WorkorderServiceClient(string baseUri)
        {
            _baseUri = baseUri;
        }

        public async Task<WorkorderCreated> CreateWorkorder(string clientCode)
        {
            using var client = new HttpClient { BaseAddress = new Uri(_baseUri)};
            HttpResponseMessage result;
            try
            {
                result = await client.PostAsync(
                    $"/Workorder?ClientCode=" + clientCode,
                    new StringContent(string.Empty, Encoding.UTF8, "application/json"));
            }
            catch (System.Exception ex)
            {
                throw new Exception("There was a problem connecting to Provider API.", ex);
            }

            var resultContentString = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                var message = resultContentString;
                throw new Exception(message);
            }

            return JsonConvert.DeserializeObject<WorkorderCreated>(resultContentString);
        }
    }
}