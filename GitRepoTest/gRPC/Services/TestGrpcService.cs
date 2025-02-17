using Grpc.Core;
using static GitRepoTest.gRPC.TestgRPC;

namespace GitRepoTest.gRPC.Services
{
    public class TestGrpcService : TestgRPCBase
    {
        public override Task<TestResponseType> TestBasics(TestRequestType request, ServerCallContext context)
        {
            return base.TestBasics(request, context);

        }

        public override Task TestStreams(IAsyncStreamReader<TestRequestType> requestStream, IServerStreamWriter<TestResponseType> responseStream, ServerCallContext context)
        {
            return Task.FromResult(23);
        }
        
    }
}
