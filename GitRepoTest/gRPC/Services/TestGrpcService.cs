using Grpc.Core;
using static GitRepoTest.gRPC.TestService;

namespace GitRepoTest.gRPC.Services
{
    public class TestGrpcService : TestServiceBase
    {
        public override Task<TestResponse> TestMethod1(Testrequest request, ServerCallContext context)
        {
            return base.TestMethod1(request, context);
        }
    }
}
