using Grpc.Core;
using Ledger;

namespace LedgerMicroservice.GrpcServices;

public class LedgerService : Ledger.LedgerService.LedgerServiceBase
{
    public override Task<DoActionResponse> DoAction(DoActionRequest request, ServerCallContext context)
    {
        Console.WriteLine($"Received DoAction request: {request.Content}");
        return Task.FromResult(new DoActionResponse { Content = $"Processed Do Action!" });
    }

    public override Task<GetInfoResponse> GetInfo(GetInfoRequest request, ServerCallContext context)
    {
        Console.WriteLine($"Received GetInfo request: {request.Content}");
        return Task.FromResult(new GetInfoResponse { Content = $"The Info!" });
    }
}