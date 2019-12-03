using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NSubstitute;
using WorkorderService.DomainServices;

namespace WorkorderServiceCdcTests
{
    public class WorkorderServiceConsumedByAngleClientProviderStateMiddleware
    {
        public static IClientService MockClientService { get; private set; }

        private readonly string ConsumerName = "AngleClient";
        private readonly RequestDelegate _next;
        private readonly IDictionary<string, Action> _providerStates;

        public WorkorderServiceConsumedByAngleClientProviderStateMiddleware(RequestDelegate next)
        {
            _next = next;
            _providerStates = new Dictionary<string, Action>
            {
                {
                    "A client with code DERP does not exist",
                    PrepareMockClientServiceWhereDerpDoesNotExist
                },
                {
                    "A client with code COOL exists",
                    PrepareMockClientServiceWhereCoolExists
                }
            };
        }

        private void PrepareMockClientServiceWhereCoolExists()
        {
            MockClientService = Substitute.For<IClientService>();
            MockClientService.DoesClientExist("COOL").Returns(true);
        }

        private void PrepareMockClientServiceWhereDerpDoesNotExist()
        {
            MockClientService = Substitute.For<IClientService>();
            MockClientService.DoesClientExist("DERP").Returns(false);
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value == "/provider-states")
            {
                await HandleProviderStatesRequest(context);
                await context.Response.WriteAsync(String.Empty);
            }
            else
            {
                await this._next(context);
            }
        }

        private async Task HandleProviderStatesRequest(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            if (context.Request.Method.ToUpper() == HttpMethod.Post.ToString().ToUpper() &&
                context.Request.Body != null)
            {
                string jsonRequestBody = String.Empty;
                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
                {
                    jsonRequestBody = await reader.ReadToEndAsync();
                }

                var providerState = JsonConvert.DeserializeObject<ProviderState>(jsonRequestBody);

                //A null or empty provider state key must be handled
                if (providerState != null && !String.IsNullOrEmpty(providerState.State) &&
                    providerState.Consumer == ConsumerName)
                {
                    _providerStates[providerState.State].Invoke();
                }
            }
        }
    }
}