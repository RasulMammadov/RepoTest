using Grpc.Core;
using static GitRepoTest.gRPC.TestService;

namespace GitRepoTest.gRPC.Services
{
    public class TestGrpcService : TestServiceBase
    {
        public override Task<TestResponse> TestMethod1(Testrequest request, ServerCallContext context)
        {
            new Test.Test2().Test3();

            return base.TestMethod1(request, context);
        }

        public static class Test
        {
            public ref struct Test2
            {
                public Test2()
        {
                    
        }
                public void Test3()
                {
        
    }
}
        }
    }
}
