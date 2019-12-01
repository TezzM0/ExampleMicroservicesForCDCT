using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AngleClient
{
    internal class WorkorderServiceClient
    {
        private readonly string _baseUri;

        public WorkorderServiceClient(string baseUri)
        {
            _baseUri = baseUri;
        }

        public async Task CreateWorkorder(string clientCode)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_baseUri)})
            {
                try
                {
                    await client.PostAsync($"/Workorder?ClientCode=" + clientCode, new StringContent(string.Empty, Encoding.UTF8, "application/json"));
                }
                catch (System.Exception ex)
                {
                    throw new Exception("There was a problem connecting to Provider API.", ex);
                }
            }
        }
    }
}