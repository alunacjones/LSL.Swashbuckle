using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace LSL.Swashbuckle.CustomData
{
    public class CustomDataHandler : DelegatingHandler
    {
        private CustomDataBuilder _configuration;

        public CustomDataHandler(CustomDataBuilder configuration)
        {
            _configuration = configuration;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<Dictionary<string, string>>(_configuration.BuildDictionary(), new JsonMediaTypeFormatter())
            });
        }
    }
}