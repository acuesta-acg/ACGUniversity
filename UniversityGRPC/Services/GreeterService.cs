using Grpc.Core;
using Microsoft.Extensions.Configuration;
using UniversityGRPC;

namespace UniversityGRPC.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GreeterService> _logger;
        /*
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        } */
        public GreeterService(ILogger<GreeterService> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}